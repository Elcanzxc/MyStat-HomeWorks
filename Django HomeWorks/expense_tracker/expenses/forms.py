from decimal import Decimal
from django import forms
from django.utils import timezone

from expenses.models import Expense


class ExpenseForm(forms.ModelForm):
    class Meta:
        model = Expense
        fields = [
            "title",
            "amount",
            "category",
            "payment_method",
            "expense_date",
            "comment",
        ]
        widgets = {
            "title": forms.TextInput(attrs={"placeholder": "Например: Продукты в супермаркете"}),
            "amount": forms.NumberInput(attrs={"placeholder": "0.00", "step": "0.01", "min": "0.01"}),
            "category": forms.Select(),
            "payment_method": forms.Select(),
            "expense_date": forms.DateInput(attrs={"type": "date"}),
            "comment": forms.Textarea(attrs={"rows": 3, "placeholder": "Необязательный комментарий..."}),
        }

    def clean_title(self):
        title = self.cleaned_data.get("title", "").strip()
        if len(title) < 3:
            raise forms.ValidationError("Название должно содержать минимум 3 символа.")
        return title

    def clean_amount(self):
        amount = self.cleaned_data.get("amount")
        if amount is not None and amount <= Decimal("0"):
            raise forms.ValidationError("Сумма расхода должна быть больше нуля.")
        return amount

    def clean_expense_date(self):
        expense_date = self.cleaned_data.get("expense_date")
        if expense_date and expense_date > timezone.localdate():
            raise forms.ValidationError("Дата расхода не может быть в будущем.")
        return expense_date
