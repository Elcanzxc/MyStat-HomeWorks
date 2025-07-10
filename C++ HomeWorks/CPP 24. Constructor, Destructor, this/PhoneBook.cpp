#include "PhoneBook.h"

Contact::Contact(const char* name, const char* home, const char* work, const char* mobile, const char* info) {
    this->name = new char[strlen(name) + 1];
    strcpy_s(this->name, strlen(name) + 1, name);
    strcpy_s(homePhone, sizeof(homePhone), home);
    strcpy_s(workPhone, sizeof(workPhone), work);
    strcpy_s(mobilePhone, sizeof(mobilePhone), mobile);
    strcpy_s(DopInfo, sizeof(DopInfo), info);
}

Contact::~Contact() {
    delete[] name;
}

void Contact::display() const {
    cout << "ФИО: " << name << endl << "Домашний: " << homePhone
        << endl << "Рабочий: " << workPhone << endl << "Мобильный: " << mobilePhone
        << endl << "Доп. информация: " << DopInfo << endl;
}

const char* Contact::getName() const {
    return name;
}

void Contact::saveToFile(FILE* file) const {
    fprintf(file, "%s\n%s\n%s\n%s\n%s\n", name, homePhone, workPhone, mobilePhone, DopInfo);
}

Contact* Contact::loadFromFile(FILE* file) {
    char name[100], homePhone[20], workPhone[20], mobilePhone[20], DopInfo[100];

    name[strcspn(name, "\n")] = '\0';
    homePhone[strcspn(homePhone, "\n")] = '\0';
    workPhone[strcspn(workPhone, "\n")] = '\0';
    mobilePhone[strcspn(mobilePhone, "\n")] = '\0';
    DopInfo[strcspn(DopInfo, "\n")] = '\0';

    return new Contact(name, homePhone, workPhone, mobilePhone, DopInfo);
}

PhoneBook::PhoneBook() : ContactCount(0) {}

PhoneBook::~PhoneBook() {
    for (int i = 0; i < ContactCount; i++) {
        delete contacts[i];
    }
}

void PhoneBook::addContact(const char* name, const char* home, const char* work, const char* mobile, const char* dopInfo) {
    if (ContactCount < 100) {
        contacts[ContactCount++] = new Contact(name, home, work, mobile, dopInfo);
    }
    else {
        cout << "Уже всё!" << endl;
    }
}

void PhoneBook::removeContact(const char* name) {
    for (int i = 0; i < ContactCount; i++) {
        if (strcmp(contacts[i]->getName(), name) == 0) {
            delete contacts[i];
            for (int j = i; j < ContactCount - 1; j++) {
                contacts[j] = contacts[j + 1];
            }
            ContactCount--;
            cout << "Удалено" << endl;
            return;
        }
    }
    cout << "Не найдено" << endl;
}

void PhoneBook::searchContact(const char* name) const {
    for (int i = 0; i < ContactCount; i++) {
        if (strcmp(contacts[i]->getName(), name) == 0) {
            contacts[i]->display();
            return;
        }
    }
    cout << "Не найдено" << endl;
}

void PhoneBook::displayAll() const {
    for (int i = 0; i < ContactCount; i++) {
        (*contacts[i]).display();
    }
}

void PhoneBook::saveToFile(const char* filename) const {
    FILE* file{ nullptr };
    fopen_s(&file, filename, "w");
    if (file == nullptr) {
        cout << "Не могу сохранить в: " << filename << endl;
        return;
    }

    for (int i = 0; i < ContactCount; i++) {
        contacts[i]->saveToFile(file);
    }
    fclose(file);
    cout << "Сохранено в: " << filename << endl;
}

void PhoneBook::loadFromFile(const char* filename) {
    FILE* file{ nullptr };
    fopen_s(&file, filename, "r");
    if (file == nullptr) {
        cout << "Не могу загрузить из: " << filename << endl;
        return;
    }
    while (ContactCount < 100) {
        Contact* newContact = Contact::loadFromFile(file);
        if (!newContact) break;
        contacts[ContactCount++] = newContact;
    }
    fclose(file);
    cout << "Загружено из: " << filename << endl;
}
