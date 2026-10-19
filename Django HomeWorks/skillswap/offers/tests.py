from django.contrib.auth import get_user_model
from django.test import Client, TestCase

from offers.models import Category, Favorite, LessonRequest, Review, SkillOffer

User = get_user_model()


class SkillSwapTests(TestCase):
    def setUp(self):
        self.client = Client()
        self.superadmin = User.objects.create_superuser(
            username="superadmin", email="super@mail.com", password="password123", role="superadmin"
        )
        self.admin = User.objects.create_user(
            username="adminuser", email="admin@mail.com", password="password123", role="admin", is_staff=True
        )
        self.mentor = User.objects.create_user(
            username="mentor1", email="mentor@mail.com", password="password123", role="user"
        )
        self.student = User.objects.create_user(
            username="student1", email="student@mail.com", password="password123", role="user"
        )
        self.banned_user = User.objects.create_user(
            username="banned1", email="banned@mail.com", password="password123", role="user", is_banned=True
        )
        self.category = Category.objects.create(name="Programming", slug="programming")

        self.offer = SkillOffer.objects.create(
            title="Django Advanced Workshop",
            description="Deep dive into Django internals, models, views and database optimization.",
            category=self.category,
            level="Advanced",
            format="Online",
            duration="90 минут",
            author=self.mentor,
            status="published",
        )

    def test_explore_and_popular_views(self):
        response = self.client.get("/")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Django Advanced Workshop")

        response = self.client.get("/?q=Django")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Django Advanced Workshop")

        response = self.client.get("/?category=programming")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Django Advanced Workshop")

        response = self.client.get("/popular/")
        self.assertEqual(response.status_code, 200)

    def test_offer_detail_and_favorite(self):
        response = self.client.get(f"/offer/{self.offer.id}/")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "mentor1")

        self.client.login(username="student1", password="password123")
        fav_response = self.client.post(f"/offer/{self.offer.id}/favorite/")
        self.assertEqual(fav_response.status_code, 302)
        self.assertTrue(Favorite.objects.filter(user=self.student, offer=self.offer).exists())

        fav_response2 = self.client.post(f"/offer/{self.offer.id}/favorite/")
        self.assertEqual(fav_response2.status_code, 302)
        self.assertFalse(Favorite.objects.filter(user=self.student, offer=self.offer).exists())

    def test_offer_creation_and_moderation(self):
        self.client.login(username="mentor1", password="password123")
        create_res = self.client.post(
            "/offer/create/",
            {
                "title": "Python Basics Course",
                "category": self.category.id,
                "description": "Learn python from scratch with plenty of hands on practice.",
                "level": "Beginner",
                "format": "Online",
                "duration": "60 минут",
            },
        )
        self.assertEqual(create_res.status_code, 302)
        new_offer = SkillOffer.objects.get(title="Python Basics Course")
        self.assertEqual(new_offer.status, "pending")

        self.client.login(username="adminuser", password="password123")
        mod_res = self.client.post(f"/moderation/{new_offer.id}/publish/")
        self.assertEqual(mod_res.status_code, 302)
        new_offer.refresh_from_db()
        self.assertEqual(new_offer.status, "published")

    def test_lesson_request_and_review_flow(self):
        self.client.login(username="student1", password="password123")
        req_res = self.client.post(
            f"/offer/{self.offer.id}/request/",
            {"message": "Здравствуйте! Хочу записаться на занятие."},
        )
        self.assertEqual(req_res.status_code, 302)
        lesson_req = LessonRequest.objects.get(offer=self.offer, student=self.student)
        self.assertEqual(lesson_req.status, "pending")

        self.client.login(username="mentor1", password="password123")
        accept_res = self.client.post(f"/my-requests/{lesson_req.id}/accept/")
        self.assertEqual(accept_res.status_code, 302)
        lesson_req.refresh_from_db()
        self.assertEqual(lesson_req.status, "accepted")

        complete_res = self.client.post(f"/my-requests/{lesson_req.id}/complete/")
        self.assertEqual(complete_res.status_code, 302)
        lesson_req.refresh_from_db()
        self.assertEqual(lesson_req.status, "completed")

        self.client.login(username="student1", password="password123")
        rev_res = self.client.post(
            f"/request/{lesson_req.id}/review/",
            {"rating": 5, "comment": "Отличный ментор, все понятно объяснил!"},
        )
        self.assertEqual(rev_res.status_code, 302)
        review = Review.objects.get(lesson_request=lesson_req)
        self.assertEqual(review.rating, 5)
        self.assertEqual(self.offer.average_rating, 5.0)

    def test_banned_user_restrictions(self):
        self.client.login(username="banned1", password="password123")
        create_res = self.client.post(
            "/offer/create/",
            {
                "title": "Banned user course title",
                "category": self.category.id,
                "description": "Banned course description with enough length to pass length test.",
                "level": "Beginner",
                "format": "Online",
                "duration": "60 минут",
            },
        )
        self.assertEqual(create_res.status_code, 302)
        self.assertFalse(SkillOffer.objects.filter(title="Banned user course title").exists())

        req_res = self.client.post(
            f"/offer/{self.offer.id}/request/",
            {"message": "Hello from banned user"},
        )
        self.assertEqual(req_res.status_code, 302)
        self.assertFalse(LessonRequest.objects.filter(student=self.banned_user).exists())

    def test_users_manage_admin(self):
        self.client.login(username="superadmin", password="password123")
        ban_res = self.client.post(f"/accounts/manage-users/{self.student.id}/ban/")
        self.assertEqual(ban_res.status_code, 302)
        self.student.refresh_from_db()
        self.assertTrue(self.student.is_banned)

        admin_res = self.client.post(f"/accounts/manage-users/{self.student.id}/admin/")
        self.assertEqual(admin_res.status_code, 302)
        self.student.refresh_from_db()
        self.assertEqual(self.student.role, "admin")
