from django.contrib import messages
from django.contrib.auth import authenticate, get_user_model, login, logout
from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.http import HttpRequest, HttpResponse
from django.shortcuts import get_object_or_404, redirect, render

from accounts.forms import LoginForm, ProfileEditForm, RegisterForm

User = get_user_model()


def register_view(request: HttpRequest) -> HttpResponse:
    if request.user.is_authenticated:
        return redirect("offers:explore")

    if request.method == "POST":
        form = RegisterForm(request.POST, request.FILES)
        if form.is_valid():
            user = User.objects.create_user(
                username=form.cleaned_data["username"],
                email=form.cleaned_data["email"],
                password=form.cleaned_data["password"],
                bio=form.cleaned_data.get("bio", ""),
            )
            if form.cleaned_data.get("avatar"):
                user.avatar = form.cleaned_data["avatar"]
                user.save()

            if User.objects.count() == 1:
                user.role = "superadmin"
                user.is_superuser = True
                user.is_staff = True
                user.save()

            login(request, user)
            messages.success(request, f"Добро пожаловать, {user.username}!")
            return redirect("offers:explore")
    else:
        form = RegisterForm()

    return render(request, "accounts/register.html", {"form": form})


def login_view(request: HttpRequest) -> HttpResponse:
    if request.user.is_authenticated:
        return redirect("offers:explore")

    if request.method == "POST":
        form = LoginForm(request.POST)
        if form.is_valid():
            identifier = form.cleaned_data["username_or_email"].strip()
            password = form.cleaned_data["password"]

            user = authenticate(request, username=identifier, password=password)
            if user is None:
                try:
                    candidate = User.objects.get(email__iexact=identifier)
                    user = authenticate(
                        request, username=candidate.username, password=password
                    )
                except User.DoesNotExist:
                    user = None

            if user is not None:
                login(request, user)
                messages.success(request, f"Рады видеть вас снова, {user.username}!")
                next_url = request.GET.get("next") or "offers:explore"
                return redirect(next_url)
            else:
                messages.error(request, "Неверный логин или пароль.")
    else:
        form = LoginForm()

    return render(request, "accounts/login.html", {"form": form})


@login_required
def logout_view(request: HttpRequest) -> HttpResponse:
    logout(request)
    messages.info(request, "Вы успешно вышли из системы.")
    return redirect("offers:explore")


def profile_view(request: HttpRequest, username: str) -> HttpResponse:
    user_obj = get_object_or_404(User, username=username)
    from offers.models import Review, SkillOffer

    published_offers = (
        SkillOffer.objects.filter(author=user_obj, status="published")
        .select_related("category")
        .all()
    )
    reviews = (
        Review.objects.filter(offer__author=user_obj)
        .select_related("student", "offer")
        .all()
    )

    context = {
        "profile_user": user_obj,
        "published_offers": published_offers,
        "reviews": reviews,
    }
    return render(request, "accounts/profile.html", context)


@login_required
def profile_edit_view(request: HttpRequest) -> HttpResponse:
    if request.method == "POST":
        form = ProfileEditForm(request.POST, request.FILES, instance=request.user)
        if form.is_valid():
            form.save()
            messages.success(request, "Профиль успешно обновлен.")
            return redirect("accounts:profile", username=request.user.username)
    else:
        form = ProfileEditForm(instance=request.user)

    return render(request, "accounts/profile_edit.html", {"form": form})


@login_required
def users_manage_view(request: HttpRequest) -> HttpResponse:
    if not request.user.is_any_admin():
        raise PermissionDenied("Доступ запрещен.")

    users = User.objects.all().order_by("-date_joined")
    return render(request, "accounts/users_manage.html", {"users_list": users})


@login_required
def toggle_ban_view(request: HttpRequest, user_id: int) -> HttpResponse:
    if not request.user.is_any_admin():
        raise PermissionDenied("Доступ запрещен.")

    target_user = get_object_or_404(User, pk=user_id)
    if target_user.is_super_admin():
        messages.error(request, "Нельзя заблокировать супер админа.")
        return redirect("accounts:users_manage")

    if request.method == "POST":
        target_user.is_banned = not target_user.is_banned
        target_user.save()
        status_msg = "заблокирован" if target_user.is_banned else "разблокирован"
        messages.success(
            request, f"Пользователь {target_user.username} {status_msg}."
        )

    return redirect("accounts:users_manage")


@login_required
def toggle_admin_view(request: HttpRequest, user_id: int) -> HttpResponse:
    if not request.user.is_super_admin():
        raise PermissionDenied("Только супер админ может назначать администраторов.")

    target_user = get_object_or_404(User, pk=user_id)
    if target_user.is_super_admin():
        messages.error(request, "Нельзя изменить роль супер админа.")
        return redirect("accounts:users_manage")

    if request.method == "POST":
        if target_user.role == "admin":
            target_user.role = "user"
            target_user.is_staff = False
            messages.success(
                request, f"С пользователя {target_user.username} сняты права админа."
            )
        else:
            target_user.role = "admin"
            target_user.is_staff = True
            messages.success(
                request,
                f"Пользователь {target_user.username} назначен администратором.",
            )
        target_user.save()

    return redirect("accounts:users_manage")
