from django.contrib.auth.models import AbstractUser
from django.db import models


class User(AbstractUser):
    ROLE_CHOICES = [
        ("superadmin", "Супер админ"),
        ("admin", "Админ"),
        ("user", "Юзер"),
    ]

    role = models.CharField(max_length=20, choices=ROLE_CHOICES, default="user")
    is_banned = models.BooleanField(default=False)
    avatar = models.ImageField(upload_to="avatars/", null=True, blank=True)
    bio = models.TextField(blank=True, default="")

    def is_super_admin(self) -> bool:
        return self.is_superuser or self.role == "superadmin"

    def is_any_admin(self) -> bool:
        return self.is_superuser or self.role in ("superadmin", "admin")

    @property
    def average_rating(self) -> float:
        from offers.models import Review

        reviews = Review.objects.filter(offer__author=self)
        if not reviews.exists():
            return 0.0
        avg = reviews.aggregate(models.Avg("rating"))["rating__avg"]
        return round(avg, 1) if avg else 0.0

    @property
    def published_offers_count(self) -> int:
        return self.offers.filter(status="published").count()

    @property
    def completed_lessons_count(self) -> int:
        from offers.models import LessonRequest

        return (
            LessonRequest.objects.filter(
                (models.Q(offer__author=self) | models.Q(student=self)),
                status="completed",
            )
            .distinct()
            .count()
        )

    def __str__(self):
        return self.username
