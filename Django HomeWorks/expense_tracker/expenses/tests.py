from datetime import timedelta
from decimal import Decimal
from django.contrib.auth.models import User
from django.test import TestCase
from django.urls import reverse
from django.utils import timezone

from expenses.forms import ExpenseForm
from expenses.models import Expense


class ExpenseTrackerTests(TestCase):
    def setUp(self):
        self.today = timezone.localdate()
        self.user1 = User.objects.create_user(username="testuser1", password="password123")
        self.user2 = User.objects.create_user(username="testuser2", password="password123")

        self.e1 = Expense.objects.create(
            user=self.user1,
            title="Продукты в супермаркете",
            amount=Decimal("1500.00"),
            category="food",
            payment_method="card",
            expense_date=self.today,
            comment="Еда на неделю",
        )
        self.e2 = Expense.objects.create(
            user=self.user1,
            title="Такси до вокзала",
            amount=Decimal("500.00"),
            category="transport",
            payment_method="cash",
            expense_date=self.today - timedelta(days=2),
            comment="",
        )
        self.e3 = Expense.objects.create(
            user=self.user1,
            title="Книга по программированию",
            amount=Decimal("1200.00"),
            category="education",
            payment_method="card",
            expense_date=self.today - timedelta(days=5),
            comment="Django & Python",
        )

        self.e_other = Expense.objects.create(
            user=self.user2,
            title="Расход другого пользователя",
            amount=Decimal("3000.00"),
            category="entertainment",
            payment_method="card",
            expense_date=self.today,
        )

    def test_unauthenticated_user_redirected(self):
        response = self.client.get(reverse("expenses:list"))
        self.assertEqual(response.status_code, 302)
        self.assertIn("/accounts/login/", response.url)

    def test_user_sees_only_own_expenses(self):
        self.client.login(username="testuser1", password="password123")
        response = self.client.get(reverse("expenses:list"))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Продукты в супермаркете")
        self.assertContains(response, "Такси до вокзала")
        self.assertContains(response, "Книга по программированию")
        self.assertNotContains(response, "Расход другого пользователя")
        self.assertEqual(response.context["total_count"], 3)
        self.assertEqual(response.context["total_amount"], Decimal("3200.00"))

    def test_foreign_expense_access_forbidden(self):
        self.client.login(username="testuser1", password="password123")
        response_detail = self.client.get(reverse("expenses:detail", args=[self.e_other.pk]))
        self.assertEqual(response_detail.status_code, 404)

        response_edit = self.client.get(reverse("expenses:edit", args=[self.e_other.pk]))
        self.assertEqual(response_edit.status_code, 404)

        response_delete = self.client.get(reverse("expenses:delete", args=[self.e_other.pk]))
        self.assertEqual(response_delete.status_code, 404)

    def test_expense_create_auto_assigns_owner(self):
        self.client.login(username="testuser1", password="password123")
        response = self.client.post(
            reverse("expenses:create"),
            {
                "title": "Кино и попкорн",
                "amount": "650.00",
                "category": "entertainment",
                "payment_method": "card",
                "expense_date": self.today.strftime("%Y-%m-%d"),
                "comment": "Отличный фильм",
            },
        )
        self.assertEqual(response.status_code, 302)
        new_expense = Expense.objects.get(title="Кино и попкорн")
        self.assertEqual(new_expense.user, self.user1)

    def test_validation_amount_greater_than_zero(self):
        form_zero = ExpenseForm(
            data={
                "title": "Покупка",
                "amount": "0.00",
                "category": "food",
                "payment_method": "card",
                "expense_date": self.today.strftime("%Y-%m-%d"),
            }
        )
        self.assertFalse(form_zero.is_valid())
        self.assertIn("amount", form_zero.errors)

        form_negative = ExpenseForm(
            data={
                "title": "Покупка",
                "amount": "-10.00",
                "category": "food",
                "payment_method": "card",
                "expense_date": self.today.strftime("%Y-%m-%d"),
            }
        )
        self.assertFalse(form_negative.is_valid())
        self.assertIn("amount", form_negative.errors)

    def test_validation_title_length(self):
        form = ExpenseForm(
            data={
                "title": "  ab  ",
                "amount": "100.00",
                "category": "food",
                "payment_method": "card",
                "expense_date": self.today.strftime("%Y-%m-%d"),
            }
        )
        self.assertFalse(form.is_valid())
        self.assertIn("title", form.errors)

    def test_validation_future_date(self):
        future_date = self.today + timedelta(days=2)
        form = ExpenseForm(
            data={
                "title": "Покупка",
                "amount": "100.00",
                "category": "food",
                "payment_method": "card",
                "expense_date": future_date.strftime("%Y-%m-%d"),
            }
        )
        self.assertFalse(form.is_valid())
        self.assertIn("expense_date", form.errors)

    def test_joint_filters(self):
        self.client.login(username="testuser1", password="password123")

        response = self.client.get(
            reverse("expenses:list"),
            {
                "q": "Такси",
                "category": "transport",
                "payment_method": "cash",
                "date_from": (self.today - timedelta(days=3)).strftime("%Y-%m-%d"),
                "date_to": self.today.strftime("%Y-%m-%d"),
            },
        )
        self.assertEqual(response.status_code, 200)
        self.assertEqual(response.context["total_count"], 1)
        self.assertEqual(response.context["total_amount"], Decimal("500.00"))
        self.assertContains(response, "Такси до вокзала")
        self.assertNotContains(response, "Продукты в супермаркете")

    def test_filter_no_results_summary_zero(self):
        self.client.login(username="testuser1", password="password123")
        response = self.client.get(reverse("expenses:list"), {"q": "Несуществующий расход"})
        self.assertEqual(response.status_code, 200)
        self.assertEqual(response.context["total_count"], 0)
        self.assertEqual(response.context["total_amount"], Decimal("0.00"))
        self.assertContains(response, "Расходы не найдены")
