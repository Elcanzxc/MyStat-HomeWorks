from django import forms

CATEGORY_CHOICES = [
    ("", "-- Выберите категорию --"),
    ("Education", "Education"),
    ("Technology", "Technology"),
    ("Sport", "Sport"),
    ("Entertainment", "Entertainment"),
    ("Other", "Other"),
]


class EventForm(forms.Form):
    title = forms.CharField(
        label="Название события",
        max_length=100,
        min_length=3,
        widget=forms.TextInput(attrs={"placeholder": "Введите название события"}),
    )
    description = forms.CharField(
        label="Описание",
        min_length=10,
        widget=forms.Textarea(
            attrs={"placeholder": "Введите описание события (минимум 10 символов)", "rows": 5}
        ),
    )
    category = forms.ChoiceField(
        label="Категория",
        choices=CATEGORY_CHOICES,
        widget=forms.Select(),
    )
    location = forms.CharField(
        label="Место проведения",
        max_length=150,
        min_length=2,
        widget=forms.TextInput(attrs={"placeholder": "Введите место проведения"}),
    )

    def clean_title(self):
        title = self.cleaned_data.get("title", "").strip()
        if title.lower().startswith("test") or title.lower().startswith("тест"):
            raise forms.ValidationError("Название не должно начинаться со слова 'test' или 'тест'")
        return title
