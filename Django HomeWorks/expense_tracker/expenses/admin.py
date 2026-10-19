from django.contrib import admin

from expenses.models import Expense


@admin.register(Expense)
class ExpenseAdmin(admin.ModelAdmin):
    list_display = ["title", "amount", "category", "payment_method", "expense_date", "user"]
    list_filter = ["category", "payment_method", "expense_date"]
    search_fields = ["title", "comment"]
