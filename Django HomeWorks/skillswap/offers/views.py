from django.contrib import messages
from django.contrib.auth import get_user_model
from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.db.models import Avg, Count, Q
from django.http import HttpRequest, HttpResponse
from django.shortcuts import get_object_or_404, redirect, render

from offers.forms import (
    CategoryForm,
    LessonRequestForm,
    ReviewForm,
    SkillOfferForm,
)
from offers.models import Category, Favorite, LessonRequest, Review, SkillOffer

User = get_user_model()


def explore(request: HttpRequest) -> HttpResponse:
    offers = (
        SkillOffer.objects.filter(status="published")
        .select_related("category", "author")
        .prefetch_related("reviews")
        .all()
    )

    q = request.GET.get("q", "").strip()
    category_slug = request.GET.get("category", "").strip()
    level = request.GET.get("level", "").strip()
    fmt = request.GET.get("format", "").strip()
    min_rating = request.GET.get("min_rating", "").strip()

    if q:
        offers = offers.filter(
            Q(title__icontains=q) | Q(description__icontains=q)
        )

    if category_slug:
        offers = offers.filter(category__slug=category_slug)

    if level:
        offers = offers.filter(level=level)

    if fmt:
        offers = offers.filter(format=fmt)

    offers_list = list(offers)
    if min_rating:
        try:
            rating_val = float(min_rating)
            offers_list = [o for o in offers_list if o.average_rating >= rating_val]
        except ValueError:
            pass

    categories = Category.objects.all()

    context = {
        "offers": offers_list,
        "categories": categories,
        "q": q,
        "selected_category": category_slug,
        "selected_level": level,
        "selected_format": fmt,
        "selected_min_rating": min_rating,
        "title": "Explore Skills",
    }
    return render(request, "offers/explore.html", context)


def popular(request: HttpRequest) -> HttpResponse:
    published_offers = (
        SkillOffer.objects.filter(status="published")
        .select_related("category", "author")
        .prefetch_related("reviews")
        .all()
    )
    popular_offers = [o for o in published_offers if o.average_rating >= 4.0]

    context = {
        "offers": popular_offers,
        "title": "Popular Skills (4+ Stars)",
    }
    return render(request, "offers/popular.html", context)


def categories_list(request: HttpRequest) -> HttpResponse:
    categories = Category.objects.annotate(
        published_count=Count(
            "offers", filter=Q(offers__status="published")
        )
    ).all()
    return render(request, "offers/categories_list.html", {"categories": categories})


def category_offers(request: HttpRequest, slug: str) -> HttpResponse:
    category = get_object_or_404(Category, slug=slug)
    offers = (
        SkillOffer.objects.filter(category=category, status="published")
        .select_related("author", "category")
        .prefetch_related("reviews")
        .all()
    )
    return render(
        request,
        "offers/category_offers.html",
        {"category": category, "offers": offers},
    )


def mentors_list(request: HttpRequest) -> HttpResponse:
    mentors = (
        User.objects.filter(offers__status="published")
        .distinct()
        .prefetch_related("offers")
    )
    return render(request, "offers/mentors_list.html", {"mentors": mentors})


def offer_detail(request: HttpRequest, offer_id: int) -> HttpResponse:
    offer = get_object_or_404(
        SkillOffer.objects.select_related("category", "author").prefetch_related(
            "reviews__student", "reviews__lesson_request"
        ),
        pk=offer_id,
    )

    if offer.status != "published":
        can_view = (
            request.user.is_authenticated
            and (request.user == offer.author or request.user.is_any_admin())
        )
        if not can_view:
            raise PermissionDenied("Это предложение еще не опубликовано.")

    is_favorited = False
    has_active_request = False
    if request.user.is_authenticated:
        is_favorited = Favorite.objects.filter(
            user=request.user, offer=offer
        ).exists()
        has_active_request = LessonRequest.objects.filter(
            offer=offer,
            student=request.user,
            status__in=["pending", "accepted"],
        ).exists()

    context = {
        "offer": offer,
        "is_favorited": is_favorited,
        "has_active_request": has_active_request,
    }
    return render(request, "offers/offer_detail.html", context)


@login_required
def offer_create(request: HttpRequest) -> HttpResponse:
    if request.user.is_banned:
        messages.error(request, "Заблокированные пользователи не могут создавать предложения.")
        return redirect("offers:explore")

    if request.method == "POST":
        form = SkillOfferForm(request.POST, request.FILES)
        if form.is_valid():
            offer = form.save(commit=False)
            offer.author = request.user
            offer.status = "pending"
            offer.save()
            messages.success(
                request,
                f"Предложение «{offer.title}» отправлено на модерацию администраторам.",
            )
            return redirect("offers:my_offers")
    else:
        form = SkillOfferForm()

    return render(request, "offers/offer_form.html", {"form": form, "action": "Создать"})


@login_required
def offer_edit(request: HttpRequest, offer_id: int) -> HttpResponse:
    offer = get_object_or_404(SkillOffer, pk=offer_id)

    can_edit = request.user == offer.author or request.user.is_any_admin()
    if not can_edit:
        raise PermissionDenied("Вы не можете редактировать чужие предложения.")

    if request.user.is_banned and not request.user.is_any_admin():
        messages.error(request, "Заблокированные пользователи не могут редактировать предложения.")
        return redirect("offers:explore")

    if request.method == "POST":
        form = SkillOfferForm(request.POST, request.FILES, instance=offer)
        if form.is_valid():
            updated_offer = form.save(commit=False)
            if not request.user.is_any_admin():
                updated_offer.status = "pending"
                messages.info(
                    request,
                    "Изменения сохранены и отправлены на повторную модерацию.",
                )
            else:
                messages.success(request, "Предложение успешно обновлено.")
            updated_offer.save()
            return redirect("offers:offer_detail", offer_id=offer.pk)
    else:
        form = SkillOfferForm(instance=offer)

    return render(
        request,
        "offers/offer_form.html",
        {"form": form, "offer": offer, "action": "Редактировать"},
    )


@login_required
def offer_delete(request: HttpRequest, offer_id: int) -> HttpResponse:
    offer = get_object_or_404(SkillOffer, pk=offer_id)

    can_delete = request.user == offer.author or request.user.is_any_admin()
    if not can_delete:
        raise PermissionDenied("Вы не можете удалять чужие предложения.")

    if request.method == "POST":
        title = offer.title
        offer.delete()
        messages.success(request, f"Предложение «{title}» успешно удалено.")
        return redirect("offers:explore")

    return render(request, "offers/offer_confirm_delete.html", {"offer": offer})


@login_required
def my_offers(request: HttpRequest) -> HttpResponse:
    offers = (
        SkillOffer.objects.filter(author=request.user)
        .select_related("category")
        .prefetch_related("reviews")
        .all()
    )
    return render(request, "offers/my_offers.html", {"offers": offers})


@login_required
def moderation_list(request: HttpRequest) -> HttpResponse:
    if not request.user.is_any_admin():
        raise PermissionDenied("Доступ запрещен.")

    pending_offers = (
        SkillOffer.objects.filter(status="pending")
        .select_related("author", "category")
        .all()
    )
    return render(
        request, "offers/moderation_list.html", {"offers": pending_offers}
    )


@login_required
def moderation_action(request: HttpRequest, offer_id: int, action: str) -> HttpResponse:
    if not request.user.is_any_admin():
        raise PermissionDenied("Доступ запрещен.")

    offer = get_object_or_404(SkillOffer, pk=offer_id)
    if request.method == "POST":
        if action == "publish":
            offer.status = "published"
            messages.success(request, f"Предложение «{offer.title}» опубликовано.")
        elif action == "reject":
            offer.status = "rejected"
            messages.warning(request, f"Предложение «{offer.title}» отклонено.")
        offer.save()

    return redirect("offers:moderation_list")


@login_required
def request_create(request: HttpRequest, offer_id: int) -> HttpResponse:
    offer = get_object_or_404(SkillOffer, pk=offer_id, status="published")

    if request.user.is_banned:
        messages.error(request, "Заблокированные пользователи не могут отправлять запросы.")
        return redirect("offers:offer_detail", offer_id=offer_id)

    if offer.author == request.user:
        messages.error(request, "Нельзя отправить запрос на собственное предложение.")
        return redirect("offers:offer_detail", offer_id=offer_id)

    existing = LessonRequest.objects.filter(
        offer=offer, student=request.user, status__in=["pending", "accepted"]
    ).exists()
    if existing:
        messages.warning(
            request, "У вас уже есть активный запрос на это занятие."
        )
        return redirect("offers:offer_detail", offer_id=offer_id)

    if request.method == "POST":
        form = LessonRequestForm(request.POST)
        if form.is_valid():
            req_obj = form.save(commit=False)
            req_obj.offer = offer
            req_obj.student = request.user
            req_obj.status = "pending"
            req_obj.save()
            messages.success(
                request, "Запрос на занятие успешно отправлен преподавателю."
            )
            return redirect("offers:my_requests")
    else:
        form = LessonRequestForm()

    return render(
        request, "offers/request_create.html", {"form": form, "offer": offer}
    )


@login_required
def my_requests(request: HttpRequest) -> HttpResponse:
    sent_requests = (
        LessonRequest.objects.filter(student=request.user)
        .select_related("offer", "offer__author")
        .all()
    )
    received_requests = (
        LessonRequest.objects.filter(offer__author=request.user)
        .select_related("offer", "student")
        .all()
    )

    context = {
        "sent_requests": sent_requests,
        "received_requests": received_requests,
    }
    return render(request, "offers/my_requests.html", context)


@login_required
def request_action(request: HttpRequest, request_id: int, action: str) -> HttpResponse:
    req_obj = get_object_or_404(
        LessonRequest.objects.select_related("offer", "student"), pk=request_id
    )

    if request.method == "POST":
        is_author = request.user == req_obj.offer.author
        is_student = request.user == req_obj.student

        if action == "accept" and is_author and req_obj.status == "pending":
            req_obj.status = "accepted"
            messages.success(request, "Запрос принят.")
        elif action == "reject" and is_author and req_obj.status == "pending":
            req_obj.status = "rejected"
            messages.info(request, "Запрос отклонен.")
        elif action == "cancel" and is_student and req_obj.status == "pending":
            req_obj.status = "cancelled"
            messages.info(request, "Запрос отменен.")
        elif action == "complete" and (is_author or is_student) and req_obj.status == "accepted":
            req_obj.status = "completed"
            messages.success(
                request,
                "Занятие отмечено как завершенное! Теперь студент может оставить отзыв.",
            )
        else:
            messages.error(request, "Невозможно выполнить данное действие.")

        req_obj.save()

    return redirect("offers:my_requests")


@login_required
def review_create(request: HttpRequest, request_id: int) -> HttpResponse:
    lesson_req = get_object_or_404(
        LessonRequest.objects.select_related("offer", "student"),
        pk=request_id,
        status="completed",
        student=request.user,
    )

    if hasattr(lesson_req, "review"):
        messages.warning(request, "Вы уже оставили отзыв по этому занятию.")
        return redirect("offers:offer_detail", offer_id=lesson_req.offer.pk)

    if request.method == "POST":
        form = ReviewForm(request.POST)
        if form.is_valid():
            review = form.save(commit=False)
            review.offer = lesson_req.offer
            review.lesson_request = lesson_req
            review.student = request.user
            review.save()
            messages.success(request, "Спасибо! Ваш отзыв успешно опубликован.")
            return redirect("offers:offer_detail", offer_id=lesson_req.offer.pk)
    else:
        form = ReviewForm()

    return render(
        request,
        "offers/review_form.html",
        {"form": form, "lesson_req": lesson_req, "action": "Оставить отзыв"},
    )


@login_required
def review_edit(request: HttpRequest, review_id: int) -> HttpResponse:
    review = get_object_or_404(Review, pk=review_id)
    if review.student != request.user:
        raise PermissionDenied("Вы можете редактировать только свой отзыв.")

    if request.method == "POST":
        form = ReviewForm(request.POST, instance=review)
        if form.is_valid():
            form.save()
            messages.success(request, "Отзыв успешно обновлен.")
            return redirect("offers:offer_detail", offer_id=review.offer.pk)
    else:
        form = ReviewForm(instance=review)

    return render(
        request,
        "offers/review_form.html",
        {"form": form, "review": review, "action": "Редактировать отзыв"},
    )


@login_required
def review_delete(request: HttpRequest, review_id: int) -> HttpResponse:
    review = get_object_or_404(Review, pk=review_id)
    can_delete = review.student == request.user or request.user.is_any_admin()
    if not can_delete:
        raise PermissionDenied("Вы не можете удалить этот отзыв.")

    offer_id = review.offer.pk
    if request.method == "POST":
        review.delete()
        messages.success(request, "Отзыв успешно удален.")
        return redirect("offers:offer_detail", offer_id=offer_id)

    return render(request, "offers/review_confirm_delete.html", {"review": review})


@login_required
def favorites_list(request: HttpRequest) -> HttpResponse:
    favorites = (
        Favorite.objects.filter(user=request.user)
        .select_related("offer", "offer__author", "offer__category")
        .prefetch_related("offer__reviews")
        .all()
    )
    offers = [f.offer for f in favorites]
    return render(
        request,
        "offers/favorites_list.html",
        {"offers": offers, "title": "Favorites"},
    )


@login_required
def favorite_toggle(request: HttpRequest, offer_id: int) -> HttpResponse:
    offer = get_object_or_404(SkillOffer, pk=offer_id)
    if request.method == "POST":
        fav = Favorite.objects.filter(user=request.user, offer=offer).first()
        if fav:
            fav.delete()
            messages.info(request, f"«{offer.title}» удалено из избранного.")
        else:
            Favorite.objects.create(user=request.user, offer=offer)
            messages.success(request, f"«{offer.title}» добавлено в избранное.")

    return redirect("offers:offer_detail", offer_id=offer_id)


@login_required
def categories_manage(request: HttpRequest) -> HttpResponse:
    if not request.user.is_super_admin():
        raise PermissionDenied("Только супер админ может управлять категориями.")

    if request.method == "POST":
        form = CategoryForm(request.POST)
        if form.is_valid():
            category = form.save()
            messages.success(
                request, f"Категория «{category.name}» успешно создана."
            )
            return redirect("offers:categories_manage")
    else:
        form = CategoryForm()

    categories = Category.objects.annotate(offers_count=Count("offers")).all()
    return render(
        request,
        "offers/categories_manage.html",
        {"form": form, "categories": categories},
    )


@login_required
def category_delete(request: HttpRequest, category_id: int) -> HttpResponse:
    if not request.user.is_super_admin():
        raise PermissionDenied("Только супер админ может удалять категории.")

    category = get_object_or_404(Category, pk=category_id)
    if request.method == "POST":
        name = category.name
        category.delete()
        messages.success(request, f"Категория «{name}» удалена.")
        return redirect("offers:categories_manage")

    return render(
        request, "offers/category_confirm_delete.html", {"category": category}
    )
