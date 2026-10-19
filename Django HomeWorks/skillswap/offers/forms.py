from django import forms

from offers.models import Category, LessonRequest, Review, SkillOffer


class SkillOfferForm(forms.ModelForm):
    class Meta:
        model = SkillOffer
        fields = [
            "title",
            "category",
            "description",
            "level",
            "format",
            "duration",
            "image",
        ]
        labels = {
            "title": "Название предложения",
            "category": "Категория",
            "description": "Описание навыка / занятия",
            "level": "Уровень подготовки",
            "format": "Формат проведения",
            "duration": "Длительность занятия",
            "image": "Изображение (Cloudinary)",
        }
        widgets = {
            "title": forms.TextInput(
                attrs={"placeholder": "Например: Обучу основам Python с нуля"}
            ),
            "description": forms.Textarea(
                attrs={"rows": 5, "placeholder": "Подробно опишите, чему вы научите..."}
            ),
            "duration": forms.TextInput(
                attrs={"placeholder": "Например: 60 минут или 1.5 часа"}
            ),
        }

    def clean_title(self):
        title = self.cleaned_data.get("title", "").strip()
        if len(title) < 5:
            raise forms.ValidationError("Название должно содержать минимум 5 символов.")
        return title

    def clean_description(self):
        desc = self.cleaned_data.get("description", "").strip()
        if len(desc) < 20:
            raise forms.ValidationError(
                "Описание должно содержать минимум 20 символов."
            )
        return desc


class CategoryForm(forms.ModelForm):
    class Meta:
        model = Category
        fields = ["name"]
        labels = {"name": "Название категории"}
        widgets = {
            "name": forms.TextInput(attrs={"placeholder": "Название новой категории"})
        }

    def clean_name(self):
        name = self.cleaned_data.get("name", "").strip()
        if not name:
            raise forms.ValidationError("Название категории не может быть пустым.")
        return name


class LessonRequestForm(forms.ModelForm):
    class Meta:
        model = LessonRequest
        fields = ["message"]
        labels = {"message": "Сообщение преподавателю"}
        widgets = {
            "message": forms.Textarea(
                attrs={
                    "rows": 3,
                    "placeholder": "Расскажите о своих целях или удобном времени для занятия...",
                }
            )
        }


class ReviewForm(forms.ModelForm):
    class Meta:
        model = Review
        fields = ["rating", "comment"]
        labels = {
            "rating": "Оценка от 1 до 5",
            "comment": "Текст отзыва",
        }
        widgets = {
            "rating": forms.Select(choices=[(i, f"{i} звезд{'ы' if 1 < i < 5 else ('а' if i == 1 else '')}") for i in range(1, 6)]),
            "comment": forms.Textarea(
                attrs={
                    "rows": 4,
                    "placeholder": "Поделитесь впечатлениями о проведенном занятии...",
                }
            ),
        }

    def clean_comment(self):
        comment = self.cleaned_data.get("comment", "").strip()
        if len(comment) < 5:
            raise forms.ValidationError("Отзыв должен содержать минимум 5 символов.")
        return comment
