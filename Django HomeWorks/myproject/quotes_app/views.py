from django.http import HttpResponse
import random


def random_quote(request):
    quotes = [
        "Ученье — свет, а неученье — тьма.",
        "Терпение и труд всё перетрут.",
        "Без труда не выловишь и рыбку из пруда.",
        "Семь раз отмерь, один раз отрежь.",
        "Век живи — век учись."
    ]
    return HttpResponse(random.choice(quotes))
