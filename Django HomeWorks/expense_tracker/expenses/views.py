from decimal import Decimal
from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.db.models import Sum
from django.http import HttpRequest, HttpResponse
from django.shortcuts import get_object_or_404, redirect, render

from expenses.forms import ExpenseForm
from expenses.models import Expense


@login_required
def expense_list(request: HttpRequest) -> HttpResponse:
    expenses = Expense.objects.filter(user=request.user)

    q = request.GET.get("q", "").strip()
    category = request.GET.get("category", "").strip()
    payment_method = request.GET.get("payment_method", "").strip()
    date_from = request.GET.get("date_from", "").strip()
    date_to = request.GET.get("date_to", "").strip()

    if q:
        expenses = expenses.filter(title__icontains=q)

    if category:
        expenses = expenses.filter(category=category)

    if payment_method:
        expenses = expenses.filter(payment_method=payment_method)

    if date_from:
        expenses = expenses.filter(expense_date__gte=date_from)

    if date_to:
        expenses = expenses.filter(expense_date__lte=date_to)

    total_count = expenses.count()
    total_amount_agg = expenses.aggregate(total=Sum("amount"))["total"]
    total_amount = total_amount_agg if total_amount_agg is not None else Decimal("0.00")

    context = {
        "expenses": expenses,
        "total_count": total_count,
        "total_amount": total_amount,
        "q": q,
        "selected_category": category,
        "selected_payment_method": payment_method,
        "date_from": date_from,
        "date_to": date_to,
        "categories": Expense.CATEGORY_CHOICES,
        "payment_methods": Expense.PAYMENT_METHOD_CHOICES,
    }
    return render(request, "expenses/expense_list.html", context)


@login_required
def expense_detail(request: HttpRequest, pk: int) -> HttpResponse:
    expense = get_object_or_404(Expense, pk=pk, user=request.user)
    return render(request, "expenses/expense_detail.html", {"expense": expense})


@login_required
def expense_create(request: HttpRequest) -> HttpResponse:
    if request.method == "POST":
        form = ExpenseForm(request.POST)
        if form.is_valid():
            expense = form.save(commit=False)
            expense.user = request.user
            expense.save()
            messages.success(request, f"Расход «{expense.title}» успешно добавлен.")
            return redirect("expenses:detail", pk=expense.pk)
    else:
        form = ExpenseForm()

    return render(request, "expenses/expense_form.html", {"form": form, "action": "Добавить расход"})


@login_required
def expense_edit(request: HttpRequest, pk: int) -> HttpResponse:
    expense = get_object_or_404(Expense, pk=pk, user=request.user)

    if request.method == "POST":
        form = ExpenseForm(request.POST, instance=expense)
        if form.is_valid():
            form.save()
            messages.success(request, f"Расход «{expense.title}» успешно обновлен.")
            return redirect("expenses:detail", pk=expense.pk)
    else:
        form = ExpenseForm(instance=expense)

    return render(
        request,
        "expenses/expense_form.html",
        {"form": form, "expense": expense, "action": "Редактировать расход"},
    )


@login_required
def expense_delete(request: HttpRequest, pk: int) -> HttpResponse:
    expense = get_object_or_404(Expense, pk=pk, user=request.user)

    if request.method == "POST":
        title = expense.title
        expense.delete()
        messages.success(request, f"Расход «{title}» успешно удален.")
        return redirect("expenses:list")

    return render(request, "expenses/expense_confirm_delete.html", {"expense": expense})
