from django.urls import path

from events import views

urlpatterns = [
    path("", views.events_list, name="events_list"),
    path("<int:event_id>/", views.events_detail, name="events_detail"),
    path("create/", views.events_create, name="events_create"),
    path("close/<int:event_id>/", views.events_close, name="events_close"),
    path("delete/<int:event_id>/", views.events_delete, name="events_delete"),
]
