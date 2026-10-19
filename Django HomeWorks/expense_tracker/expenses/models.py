from django.conf import settings
from django.db import models


class Expense(models.Model):
    CATEGORY_CHOICES = [
        ("food", "Продукты"),
        ("transport", "Транспорт"),
        ("education", "Обучение"),
        ("entertainment", "Развлечения"),
        ("health", "Здоровье"),
        ("other", "Другое"),
    ]

    PAYMENT_METHOD_CHOICES = [
        ("card", "Карта"),
        ("cash", "Наличные"),
    ]

    title = models.CharField(max_length=200, verbose_name="Название расхода")
    amount = models.DecimalField(max_digits=10, decimal_places=2, verbose_name="Сумма")
    category = models.CharField(max_length=30, choices=CATEGORY_CHOICES, verbose_name="Категория")
    payment_method = models.CharField(max_length=20, choices=PAYMENT_METHOD_CHOICES, verbose_name="Способ оплаты")
    expense_date = models.DateField(verbose_name="Дата расхода")
    comment = models.TextField(blank=True, default="", verbose_name="Комментарий")
    created_at = models.DateTimeField(auto_now_add=True, verbose_name="Дата создания")
    user = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.CASCADE, related_name="expenses", verbose_name="Пользователь")

    class Meta:
        ordering = ["-expense_date", "-created_at"]
        verbose_name = "Расход"
        verbose_name_plural = "Расходы"

    def __str__(self):
        return f"{self.title} — {self.amount} ₽"
