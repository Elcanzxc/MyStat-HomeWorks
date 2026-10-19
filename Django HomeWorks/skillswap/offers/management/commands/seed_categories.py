from django.core.management.base import BaseCommand
from django.utils.text import slugify

from offers.models import Category


class Command(BaseCommand):
    def handle(self, *args, **options):
        category_names = [
            "Programming",
            "Design",
            "Languages",
            "Marketing",
            "Music",
            "Photography",
            "Business",
            "Mathematics",
            "Other",
        ]
        created_count = 0
        for name in category_names:
            slug = slugify(name)
            obj, created = Category.objects.get_or_create(name=name, defaults={"slug": slug})
            if created:
                created_count += 1
        self.stdout.write(self.style.SUCCESS(f"Categories ready. Added {created_count} new categories."))
