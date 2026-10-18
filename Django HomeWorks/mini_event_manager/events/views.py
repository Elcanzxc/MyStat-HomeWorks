from django.contrib import messages
from django.http import Http404, HttpRequest, HttpResponse
from django.shortcuts import redirect, render

from events.data import close_event, create_event, delete_event, get_event, get_events
from events.forms import EventForm


def events_list(request: HttpRequest) -> HttpResponse:
    all_events = get_events()

    total_count = len(all_events)
    active_count = len([e for e in all_events if e.get("status") == "активное"])
    closed_count = len([e for e in all_events if e.get("status") == "закрытое"])

    q = request.GET.get("q", "").strip()
    category_filter = request.GET.get("category", "").strip()
    status_filter = request.GET.get("status", "").strip()

    events = all_events

    if q:
        events = [
            e
            for e in events
            if q.lower() in e.get("title", "").lower()
            or q.lower() in e.get("location", "").lower()
        ]

    if category_filter:
        events = [
            e
            for e in events
            if e.get("category", "").lower() == category_filter.lower()
        ]

    if status_filter:
        events = [
            e
            for e in events
            if e.get("status", "").lower() == status_filter.lower()
        ]

    categories = ["Education", "Technology", "Sport", "Entertainment", "Other"]

    context = {
        "events": events,
        "total_count": total_count,
        "active_count": active_count,
        "closed_count": closed_count,
        "q": q,
        "selected_category": category_filter,
        "selected_status": status_filter,
        "categories": categories,
    }
    return render(request, "events/events_list.html", context)


def events_detail(request: HttpRequest, event_id: int) -> HttpResponse:
    event = get_event(event_id)
    if not event:
        raise Http404("Событие не найдено")

    return render(request, "events/event_detail.html", {"event": event})


def events_create(request: HttpRequest) -> HttpResponse:
    if request.method == "POST":
        form = EventForm(request.POST)
        if form.is_valid():
            event = create_event(
                title=form.cleaned_data["title"],
                description=form.cleaned_data["description"],
                category=form.cleaned_data["category"],
                location=form.cleaned_data["location"],
            )
            messages.success(request, f"Событие «{event['title']}» успешно создано!")
            return redirect("events_detail", event_id=event["id"])
    else:
        form = EventForm()

    return render(request, "events/event_create.html", {"form": form})


def events_close(request: HttpRequest, event_id: int) -> HttpResponse:
    event = get_event(event_id)
    if not event:
        raise Http404("Событие не найдено")

    if request.method == "POST":
        if event.get("status") == "активное":
            close_event(event_id)
            messages.success(request, f"Событие «{event['title']}» успешно закрыто.")
        else:
            messages.warning(request, "Это событие уже закрыто.")
        return redirect("events_detail", event_id=event_id)

    return redirect("events_detail", event_id=event_id)


def events_delete(request: HttpRequest, event_id: int) -> HttpResponse:
    event = get_event(event_id)
    if not event:
        raise Http404("Событие не найдено")

    if request.method == "POST":
        title = event.get("title", "")
        delete_event(event_id)
        messages.success(request, f"Событие «{title}» успешно удалено.")
        return redirect("events_list")

    return render(request, "events/event_delete.html", {"event": event})
