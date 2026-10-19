from django.contrib import admin

from offers.models import Category, Favorite, LessonRequest, Review, SkillOffer


@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    list_display = ["name", "slug"]
    prepopulated_fields = {"slug": ("name",)}


@admin.register(SkillOffer)
class SkillOfferAdmin(admin.ModelAdmin):
    list_display = ["title", "category", "author", "status", "level", "format", "created_at"]
    list_filter = ["status", "category", "level", "format"]
    search_fields = ["title", "description"]


@admin.register(LessonRequest)
class LessonRequestAdmin(admin.ModelAdmin):
    list_display = ["student", "offer", "status", "created_at"]
    list_filter = ["status"]


@admin.register(Review)
class ReviewAdmin(admin.ModelAdmin):
    list_display = ["student", "offer", "rating", "created_at"]
    list_filter = ["rating"]


@admin.register(Favorite)
class FavoriteAdmin(admin.ModelAdmin):
    list_display = ["user", "offer", "created_at"]
