from django.contrib.auth import get_user_model
from django.core.management.base import BaseCommand
from django.utils.text import slugify

from offers.models import Category, Favorite, LessonRequest, Review, SkillOffer

User = get_user_model()


class Command(BaseCommand):
    def handle(self, *args, **options):
        categories_data = [
            "Programming",
            "Design",
            "Languages",
            "Marketing",
            "Music",
            "Photography",
            "Business",
            "Mathematics",
            "Other",
        ]
        categories_map = {}
        for name in categories_data:
            cat, _ = Category.objects.get_or_create(
                name=name, defaults={"slug": slugify(name)}
            )
            categories_map[name] = cat

        superadmin, _ = User.objects.get_or_create(
            username="superadmin",
            defaults={
                "email": "superadmin@skillswap.local",
                "role": "superadmin",
                "is_superuser": True,
                "is_staff": True,
                "bio": "Главный администратор платформы SkillSwap.",
            },
        )
        superadmin.set_password("password123")
        superadmin.is_superuser = True
        superadmin.is_staff = True
        superadmin.role = "superadmin"
        superadmin.save()

        adminuser, _ = User.objects.get_or_create(
            username="adminuser",
            defaults={
                "email": "admin@skillswap.local",
                "role": "admin",
                "is_staff": True,
                "bio": "Модератор контента и куратор курсов.",
            },
        )
        adminuser.set_password("password123")
        adminuser.role = "admin"
        adminuser.is_staff = True
        adminuser.save()

        mentor1, _ = User.objects.get_or_create(
            username="mentor1",
            defaults={
                "email": "mentor1@skillswap.local",
                "role": "user",
                "bio": "Senior Python & Django разработчик с опытом более 6 лет.",
            },
        )
        mentor1.set_password("password123")
        mentor1.save()

        mentor2, _ = User.objects.get_or_create(
            username="mentor2",
            defaults={
                "email": "mentor2@skillswap.local",
                "role": "user",
                "bio": "Product Designer, ментор по Figma, UI/UX и дизайн-системам.",
            },
        )
        mentor2.set_password("password123")
        mentor2.save()

        student1, _ = User.objects.get_or_create(
            username="student1",
            defaults={
                "email": "student1@skillswap.local",
                "role": "user",
                "bio": "Начинающий разработчик, изучаю веб-технологии.",
            },
        )
        student1.set_password("password123")
        student1.save()

        banned_user, _ = User.objects.get_or_create(
            username="banned1",
            defaults={
                "email": "banned@skillswap.local",
                "role": "user",
                "is_banned": True,
                "bio": "Заблокированный пользователь за нарушение правил.",
            },
        )
        banned_user.set_password("password123")
        banned_user.is_banned = True
        banned_user.save()

        offer1, _ = SkillOffer.objects.get_or_create(
            title="Django Advanced Workshop",
            author=mentor1,
            defaults={
                "category": categories_map["Programming"],
                "description": "Глубокое погружение во внутреннее устройство Django, оптимизацию ORM-запросов (select_related, prefetch_related), архитектуру FBV/CBV и безопасность веб-приложений.",
                "level": "Advanced",
                "format": "Online",
                "duration": "90 минут",
                "status": "published",
            },
        )

        offer2, _ = SkillOffer.objects.get_or_create(
            title="Основы Python и структуры данных",
            author=mentor1,
            defaults={
                "category": categories_map["Programming"],
                "description": "Практический интенсив по языку Python для начинающих. Разберем циклы, функции, словари, ООП и решим практические задачи.",
                "level": "Beginner",
                "format": "Online",
                "duration": "60 минут",
                "status": "published",
            },
        )

        offer3, _ = SkillOffer.objects.get_or_create(
            title="Дизайн интерфейсов в Figma с нуля",
            author=mentor2,
            defaults={
                "category": categories_map["Design"],
                "description": "Научитесь проектировать современные веб-сайты и мобильные приложения в Figma: компоненты, auto-layout, адаптивная верстка и интерактивные прототипы.",
                "level": "Intermediate",
                "format": "Online",
                "duration": "60 минут",
                "status": "published",
            },
        )

        offer4, _ = SkillOffer.objects.get_or_create(
            title="Разговорный английский B2 для IT-специалистов",
            author=mentor2,
            defaults={
                "category": categories_map["Languages"],
                "description": "Преодоление языкового барьера, подготовка к техническим собеседованиям на английском языке и прохождение созвонов с иностранными клиентами.",
                "level": "Intermediate",
                "format": "Online",
                "duration": "45 минут",
                "status": "pending",
            },
        )

        req_completed, _ = LessonRequest.objects.get_or_create(
            offer=offer1,
            student=student1,
            defaults={
                "status": "completed",
                "message": "Здравствуйте! Хочу разобраться с продвинутыми запросами в Django.",
            },
        )

        Review.objects.get_or_create(
            offer=offer1,
            lesson_request=req_completed,
            student=student1,
            defaults={
                "rating": 5,
                "comment": "Потрясающее занятие! Ментор разложил все по полочкам, показал реальные кейсы оптимизации запросов. Очень рекомендую!",
            },
        )

        LessonRequest.objects.get_or_create(
            offer=offer2,
            student=student1,
            defaults={
                "status": "pending",
                "message": "Привет! Хочу подтянуть темы со словарями и списками.",
            },
        )

        Favorite.objects.get_or_create(user=student1, offer=offer1)
        Favorite.objects.get_or_create(user=student1, offer=offer3)

        self.stdout.write(
            self.style.SUCCESS("Тестовые данные успешно загружены в базу данных!")
        )
