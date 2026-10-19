from django.contrib import admin
from django.contrib.auth.admin import UserAdmin

from accounts.models import User


@admin.register(User)
class CustomUserAdmin(UserAdmin):
    fieldsets = UserAdmin.fieldsets + (
        ("Дополнительно", {"fields": ("role", "is_banned", "avatar", "bio")}),
    )
    list_display = ["username", "email", "role", "is_banned", "is_staff"]
    list_filter = ["role", "is_banned", "is_staff"]
