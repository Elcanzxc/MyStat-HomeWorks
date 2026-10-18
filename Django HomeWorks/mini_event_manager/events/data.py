from copy import deepcopy
from typing import Any

_INITIAL_EVENTS: list[dict[str, Any]] = [
    {
        "id": 1,
        "title": "Django Workshop",
        "description": "Практический воркшоп по созданию веб-приложений на Django для начинающих.",
        "category": "Technology",
        "location": "Онлайн, Zoom",
        "status": "активное",
    },
    {
        "id": 2,
        "title": "Python Data Science Meetup",
        "description": "Встреча сообщества разработчиков и аналитиков данных, обмен опытом.",
        "category": "Technology",
        "location": "Конференц-зал Технопарка",
        "status": "активное",
    },
    {
        "id": 3,
        "title": "Городской марафон",
        "description": "Ежегодный забег на 10 км по набережной города для всех желающих.",
        "category": "Sport",
        "location": "Центральный парк",
        "status": "активное",
    },
    {
        "id": 4,
        "title": "Киновечер под открытым небом",
        "description": "Просмотр классических фильмов на большом экране на открытом воздухе.",
        "category": "Entertainment",
        "location": "Летний амфитеатр",
        "status": "закрытое",
    },
    {
        "id": 5,
        "title": "Лекция по истории искусства",
        "description": "Образовательная лекция о живописи эпохи Возрождения.",
        "category": "Education",
        "location": "Городская библиотека, зал 3",
        "status": "активное",
    },
    {
        "id": 6,
        "title": "Благотворительная ярмарка",
        "description": "Ярмарка сувениров ручной работы в поддержку местных приютов.",
        "category": "Other",
        "location": "Площадь Свободы",
        "status": "закрытое",
    },
]

_EVENTS: list[dict[str, Any]] = deepcopy(_INITIAL_EVENTS)
_next_id = 7


def reset_events():
    global _EVENTS, _next_id
    _EVENTS = deepcopy(_INITIAL_EVENTS)
    _next_id = 7


def get_events() -> list[dict[str, Any]]:
    return deepcopy(_EVENTS)


def get_event(event_id: int) -> dict[str, Any] | None:
    for item in _EVENTS:
        if item["id"] == event_id:
            return deepcopy(item)
    return None


def create_event(
    title: str, description: str, category: str, location: str
) -> dict[str, Any]:
    global _next_id
    event = {
        "id": _next_id,
        "title": title.strip(),
        "description": description.strip(),
        "category": category.strip(),
        "location": location.strip(),
        "status": "активное",
    }
    _EVENTS.append(event)
    _next_id += 1
    return deepcopy(event)


def close_event(event_id: int) -> bool:
    for event in _EVENTS:
        if event["id"] == event_id:
            event["status"] = "закрытое"
            return True
    return False


def delete_event(event_id: int) -> bool:
    global _EVENTS
    before = len(_EVENTS)
    _EVENTS = [e for e in _EVENTS if e["id"] != event_id]
    return before != len(_EVENTS)
