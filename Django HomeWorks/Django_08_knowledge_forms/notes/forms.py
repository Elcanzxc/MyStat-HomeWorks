from django import forms

BANNED_WORDS = ['spam', 'scam', 'hack', 'idiot', 'stupid', 'fake']


class ContactForm(forms.Form):
    name = forms.CharField(
        label='Имя',
        min_length=2,
        max_length=50,
        widget=forms.TextInput(attrs={'placeholder': 'Enter your name'})
    )
    email = forms.EmailField(
        label='Почта',
        widget=forms.EmailInput(attrs={'placeholder': 'Enter your email'})
    )
    message = forms.CharField(
        label='Сообщение',
        min_length=10,
        max_length=1000,
        widget=forms.Textarea(attrs={'placeholder': 'Enter your message', 'rows': '5'})
    )

    def clean_name(self):
        name = self.cleaned_data.get('name', '').strip()
        if len(name) < 2:
            raise forms.ValidationError('Имя должно содержать минимум 2 символа.')
        if len(name) > 50:
            raise forms.ValidationError('Имя не должно превышать 50 символов.')
        return name

    def clean_message(self):
        message = self.cleaned_data.get('message', '').strip()
        if not message:
            raise forms.ValidationError('Сообщение не должно быть пустым или состоять только из пробелов.')
        if len(message) < 10:
            raise forms.ValidationError('Сообщение должно содержать минимум 10 символов.')
        if len(message) > 1000:
            raise forms.ValidationError('Сообщение не должно превышать 1000 символов.')

        message_lower = message.lower()
        for word in BANNED_WORDS:
            if word in message_lower:
                raise forms.ValidationError(f'Сообщение не должно содержать запрещенное слово: "{word}".')

        return message


class NoteForm(forms.Form):
    CATEGORY_CHOICES = [
        ('study', 'Study'),
        ('work', 'Work'),
        ('backend', "Backend"),
        ('frontend', 'Frontend'),
    ]
    title = forms.CharField(
        label="Title",
        min_length=5,
        max_length=100,
        widget=forms.TextInput(attrs={'placeholder':'Enter your title'})
    )
    content = forms.CharField(
        label="Content",
        widget=forms.Textarea(attrs={'placeholder':'Enter your content', 'rows':'6'}, ),
        min_length=5,
    )
    tags = forms.CharField(
        label="Tags",
        max_length=200,
        widget=forms.TextInput(attrs={'placeholder':'Enter your tags. Example: django python'})
    )
    category = forms.ChoiceField(
        label="Category",
        choices=CATEGORY_CHOICES,
    )

    def clean_title(self):
        title = self.cleaned_data['title'].strip()
        if title.lower().startswith('test'):
            raise forms.ValidationError("Title should not start with 'test'")
        return title

