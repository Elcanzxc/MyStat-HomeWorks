from django.urls import path

from accounts import views

app_name = "accounts"

urlpatterns = [
    path("register/", views.register_view, name="register"),
    path("login/", views.login_view, name="login"),
    path("logout/", views.logout_view, name="logout"),
    path("profile/edit/", views.profile_edit_view, name="profile_edit"),
    path("profile/<str:username>/", views.profile_view, name="profile"),
    path("manage-users/", views.users_manage_view, name="users_manage"),
    path("manage-users/<int:user_id>/ban/", views.toggle_ban_view, name="toggle_ban"),
    path(
        "manage-users/<int:user_id>/admin/",
        views.toggle_admin_view,
        name="toggle_admin",
    ),
]
