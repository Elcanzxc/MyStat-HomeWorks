from django.test import Client, TestCase

from events.data import reset_events


class EventManagerTests(TestCase):
    def setUp(self):
        reset_events()
        self.client = Client()

    def test_events_list(self):
        response = self.client.get("/")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Django Workshop")
        self.assertContains(response, "Статистика событий")

    def test_search_and_filter(self):
        response = self.client.get("/?q=workshop")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Django Workshop")
        self.assertNotContains(response, "Городской марафон")

        response = self.client.get("/?category=Sport")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Городской марафон")
        self.assertNotContains(response, "Django Workshop")

        response = self.client.get("/?status=закрытое")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Киновечер под открытым небом")
        self.assertNotContains(response, "Django Workshop")

    def test_event_detail_and_404(self):
        response = self.client.get("/1/")
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Django Workshop")

        response_404 = self.client.get("/9999/")
        self.assertEqual(response_404.status_code, 404)

    def test_create_event_validation_and_success(self):
        bad_response = self.client.post(
            "/create/",
            {
                "title": "test event",
                "description": "Short",
                "category": "Technology",
                "location": "Online",
            },
        )
        self.assertEqual(bad_response.status_code, 200)
        self.assertContains(bad_response, "не должно начинаться со слова")

        good_response = self.client.post(
            "/create/",
            {
                "title": "Новая конференция AI",
                "description": "Конференция посвящена современным языковым моделям и их применению.",
                "category": "Technology",
                "location": "Москва, Экспоцентр",
            },
        )
        self.assertEqual(good_response.status_code, 302)

    def test_close_event(self):
        response = self.client.post("/close/1/")
        self.assertEqual(response.status_code, 302)

        detail_response = self.client.get("/1/")
        self.assertContains(detail_response, "Событие уже закрыто")

    def test_delete_event(self):
        confirm_response = self.client.get("/delete/2/")
        self.assertEqual(confirm_response.status_code, 200)
        self.assertContains(confirm_response, "Вы действительно хотите удалить")

        delete_response = self.client.post("/delete/2/")
        self.assertEqual(delete_response.status_code, 302)

        response_404 = self.client.get("/2/")
        self.assertEqual(response_404.status_code, 404)
