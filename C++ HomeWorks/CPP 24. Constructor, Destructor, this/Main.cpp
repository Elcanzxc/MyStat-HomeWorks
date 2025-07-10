#include "phonebook.h"
#include <iostream>
#include <locale.h>
using namespace std;

int main() {
    setlocale(LC_ALL, "Russian");

    PhoneBook phoneBook;
    const char* filename = "contacts.txt";
       int vibor;
    do {
     
        cout << endl << "1. Добавить контакт\n2. Удалить контакт\n3. Найти контакт\n4. Показать все контакты\n5. Сохранить\n6. Загрузить\n7. Выход\nВыбор: ";
        cin >> vibor;

        cin.ignore();
        
        switch (vibor) {
        case 1: {
            char name[100], home[20], work[20], mobile[20], dopInfo[100];
            cout << "ФИО: "; cin.getline(name, 100);
            cout << "Домашний: "; cin.getline(home, 20);
            cout << "Рабочий: "; cin.getline(work, 20);
            cout << "Мобильный: "; cin.getline(mobile, 20);
            cout << "Доп. информация: "; cin.getline(dopInfo, 100);
            phoneBook.addContact(name, home, work, mobile, dopInfo);
            break;
        }
        case 2: {
            char name[100];
            cout << "ФИО для удаления: ";
            cin.getline(name, 100);
            phoneBook.removeContact(name);
            break;
        }
        case 3: {
            char name[100];
            cout << "ФИО для поиска: ";
            cin.getline(name, 100);
            phoneBook.searchContact(name);
            break;
        }
        case 4:
            phoneBook.displayAll();
            break;
        case 5:
            phoneBook.saveToFile(filename);
            break;
        case 6:
            phoneBook.loadFromFile(filename);
            break;
        case 7:
            cout << "Пока." << endl;
            break;
        default:
            cout << "Упс попробуй заново." << endl;
            break;
        }
	} while (vibor != 7);

    return 0;
}
