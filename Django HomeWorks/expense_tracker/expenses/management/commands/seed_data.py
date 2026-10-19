from datetime import timedelta
from decimal import Decimal
from django.contrib.auth.models import User
from django.core.management.base import BaseCommand
from django.utils import timezone

from expenses.models import Expense


class Command(BaseCommand):
    def handle(self, *args, **options):
        today = timezone.localdate()

        user1, _ = User.objects.get_or_create(username="user1")
        user1.set_password("user1pass")
        user1.save()

        user2, _ = User.objects.get_or_create(username="user2")
        user2.set_password("user2pass")
        user2.save()

        Expense.objects.filter(user__in=[user1, user2]).delete()

        user1_expenses = [
            {
                "title": "Покупка продуктов в супермаркете",
                "amount": Decimal("2450.50"),
                "category": "food",
                "payment_method": "card",
                "expense_date": today,
                "comment": "Овощи, фрукты, сыр и хлеб на неделю",
            },
            {
                "title": "Проездной на метро",
                "amount": Decimal("1100.00"),
                "category": "transport",
                "payment_method": "card",
                "expense_date": today - timedelta(days=1),
                "comment": "Пополнение карты тройка",
            },
            {
                "title": "Курс по Django",
                "amount": Decimal("5000.00"),
                "category": "education",
                "payment_method": "card",
                "expense_date": today - timedelta(days=3),
                "comment": "Месячный доступ к практическому интенсиву",
            },
            {
                "title": "Билеты в кино",
                "amount": Decimal("900.00"),
                "category": "entertainment",
                "payment_method": "cash",
                "expense_date": today - timedelta(days=4),
                "comment": "Поход в кино с друзьями на вечерний сеанс",
            },
            {
                "title": "Витамины и лекарства",
                "amount": Decimal("1350.00"),
                "category": "health",
                "payment_method": "card",
                "expense_date": today - timedelta(days=6),
                "comment": "Аптека возле дома",
            },
            {
                "title": "Утренний кофе с круассаном",
                "amount": Decimal("380.00"),
                "category": "food",
                "payment_method": "cash",
                "expense_date": today - timedelta(days=2),
                "comment": "В кофейне перед работой",
            },
        ]

        for item in user1_expenses:
            Expense.objects.create(user=user1, **item)

        user2_expenses = [
            {
                "title": "Заправка автомобиля",
                "amount": Decimal("2800.00"),
                "category": "transport",
                "payment_method": "card",
                "expense_date": today,
                "comment": "Полный бак 95 бензина",
            },
            {
                "title": "Доставка пиццы",
                "amount": Decimal("1650.00"),
                "category": "food",
                "payment_method": "card",
                "expense_date": today - timedelta(days=1),
                "comment": "Две пиццы и напитки",
            },
            {
                "title": "Абонемент в спортзал",
                "amount": Decimal("3500.00"),
                "category": "health",
                "payment_method": "card",
                "expense_date": today - timedelta(days=2),
                "comment": "Ежемесячный платеж за фитнес",
            },
            {
                "title": "Книга по Python",
                "amount": Decimal("1200.00"),
                "category": "education",
                "payment_method": "cash",
                "expense_date": today - timedelta(days=5),
                "comment": "Куплено в книжном магазине",
            },
            {
                "title": "Подписка на музыку",
                "amount": Decimal("299.00"),
                "category": "entertainment",
                "payment_method": "card",
                "expense_date": today - timedelta(days=7),
                "comment": "Месячное списание",
            },
            {
                "title": "Чехол для телефона",
                "amount": Decimal("650.00"),
                "category": "other",
                "payment_method": "cash",
                "expense_date": today - timedelta(days=8),
                "comment": "Защитное стекло и чехол",
            },
        ]

        for item in user2_expenses:
            Expense.objects.create(user=user2, **item)

        self.stdout.write(self.style.SUCCESS("Demo data successfully created."))
