from django.urls import path

from offers import views

app_name = "offers"

urlpatterns = [
    path("", views.explore, name="explore"),
    path("popular/", views.popular, name="popular"),
    path("categories/", views.categories_list, name="categories_list"),
    path("categories/<slug:slug>/", views.category_offers, name="category_offers"),
    path("categories-manage/", views.categories_manage, name="categories_manage"),
    path(
        "categories-manage/<int:category_id>/delete/",
        views.category_delete,
        name="category_delete",
    ),
    path("mentors/", views.mentors_list, name="mentors_list"),
    path("offer/<int:offer_id>/", views.offer_detail, name="offer_detail"),
    path("offer/create/", views.offer_create, name="offer_create"),
    path("offer/<int:offer_id>/edit/", views.offer_edit, name="offer_edit"),
    path("offer/<int:offer_id>/delete/", views.offer_delete, name="offer_delete"),
    path("offer/<int:offer_id>/request/", views.request_create, name="request_create"),
    path(
        "offer/<int:offer_id>/favorite/",
        views.favorite_toggle,
        name="favorite_toggle",
    ),
    path("my-offers/", views.my_offers, name="my_offers"),
    path("my-requests/", views.my_requests, name="my_requests"),
    path(
        "my-requests/<int:request_id>/<str:action>/",
        views.request_action,
        name="request_action",
    ),
    path("favorites/", views.favorites_list, name="favorites_list"),
    path("moderation/", views.moderation_list, name="moderation_list"),
    path(
        "moderation/<int:offer_id>/<str:action>/",
        views.moderation_action,
        name="moderation_action",
    ),
    path(
        "request/<int:request_id>/review/",
        views.review_create,
        name="review_create",
    ),
    path("review/<int:review_id>/edit/", views.review_edit, name="review_edit"),
    path(
        "review/<int:review_id>/delete/",
        views.review_delete,
        name="review_delete",
    ),
]
