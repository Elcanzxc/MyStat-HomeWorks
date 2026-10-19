from django.conf import settings
from django.db import models
from django.utils.text import slugify


class Category(models.Model):
    name = models.CharField(max_length=100, unique=True)
    slug = models.SlugField(max_length=100, unique=True, blank=True)

    class Meta:
        ordering = ["name"]
        verbose_name_plural = "Categories"

    def save(self, *args, **kwargs):
        if not self.slug:
            self.slug = slugify(self.name)
            if not self.slug:
                self.slug = f"cat-{self.name.lower()}"
        super().save(*args, **kwargs)

    def __str__(self):
        return self.name


class SkillOffer(models.Model):
    LEVEL_CHOICES = [
        ("Beginner", "Beginner"),
        ("Intermediate", "Intermediate"),
        ("Advanced", "Advanced"),
    ]

    FORMAT_CHOICES = [
        ("Online", "Online"),
        ("Offline", "Offline"),
    ]

    STATUS_CHOICES = [
        ("draft", "Draft"),
        ("pending", "Pending"),
        ("published", "Published"),
        ("rejected", "Rejected"),
    ]

    title = models.CharField(max_length=200)
    description = models.TextField()
    image = models.ImageField(upload_to="offers/", null=True, blank=True)
    category = models.ForeignKey(
        Category, on_delete=models.CASCADE, related_name="offers"
    )
    level = models.CharField(max_length=20, choices=LEVEL_CHOICES, default="Beginner")
    format = models.CharField(max_length=20, choices=FORMAT_CHOICES, default="Online")
    duration = models.CharField(max_length=50, default="60 минут")
    author = models.ForeignKey(
        settings.AUTH_USER_MODEL, on_delete=models.CASCADE, related_name="offers"
    )
    created_at = models.DateTimeField(auto_now_add=True)
    status = models.CharField(max_length=20, choices=STATUS_CHOICES, default="pending")

    class Meta:
        ordering = ["-created_at"]

    @property
    def average_rating(self) -> float:
        reviews = self.reviews.all()
        if not reviews.exists():
            return 0.0
        avg = reviews.aggregate(models.Avg("rating"))["rating__avg"]
        return round(avg, 1) if avg else 0.0

    @property
    def reviews_count(self) -> int:
        return self.reviews.count()

    @property
    def completed_lessons_count(self) -> int:
        return self.lesson_requests.filter(status="completed").count()

    def __str__(self):
        return self.title


class LessonRequest(models.Model):
    STATUS_CHOICES = [
        ("pending", "Pending"),
        ("accepted", "Accepted"),
        ("rejected", "Rejected"),
        ("completed", "Completed"),
        ("cancelled", "Cancelled"),
    ]

    offer = models.ForeignKey(
        SkillOffer, on_delete=models.CASCADE, related_name="lesson_requests"
    )
    student = models.ForeignKey(
        settings.AUTH_USER_MODEL,
        on_delete=models.CASCADE,
        related_name="sent_requests",
    )
    status = models.CharField(max_length=20, choices=STATUS_CHOICES, default="pending")
    message = models.TextField(blank=True, default="")
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ["-created_at"]

    def __str__(self):
        return f"{self.student.username} -> {self.offer.title} ({self.status})"


class Review(models.Model):
    offer = models.ForeignKey(
        SkillOffer, on_delete=models.CASCADE, related_name="reviews"
    )
    lesson_request = models.OneToOneField(
        LessonRequest, on_delete=models.CASCADE, related_name="review"
    )
    student = models.ForeignKey(
        settings.AUTH_USER_MODEL, on_delete=models.CASCADE, related_name="reviews"
    )
    rating = models.IntegerField(choices=[(i, str(i)) for i in range(1, 6)])
    comment = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ["-created_at"]

    def __str__(self):
        return f"{self.student.username}: {self.rating}/5 for {self.offer.title}"


class Favorite(models.Model):
    user = models.ForeignKey(
        settings.AUTH_USER_MODEL, on_delete=models.CASCADE, related_name="favorites"
    )
    offer = models.ForeignKey(
        SkillOffer, on_delete=models.CASCADE, related_name="favorited_by"
    )
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        unique_together = ("user", "offer")
        ordering = ["-created_at"]

    def __str__(self):
        return f"{self.user.username} favorited {self.offer.title}"
