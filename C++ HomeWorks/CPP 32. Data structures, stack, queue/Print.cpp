#include <iostream>
#include <string>
#include "Client.h"
#include "PrintRequest.h"
#include "Printer.h"

using namespace std;

int main() {
    Printer printer(3); 

    int choice;

    while (true) {
        cout << endl << "-- - MENU-- - " << endl;
        cout << "1. Dobavit Zapros" << endl;
        cout << "2. Print Zapros"<< endl;
        cout << "3. Show statistic" << endl;
        cout << "0. Exit" << endl;
        cout << "Vashe Deystvie: " << endl;
        cin >> choice;
        cin.ignore();

        if (choice == 1) {
            string name, surname, text;

            cout << "Name: ";
            getline(cin, name);
            cout << "Surname: ";
            getline(cin, surname);
            cout << "Text: ";
            getline(cin, text);

            Client client(name, surname);
            PrintRequest request(client, text);
            printer.addRequest(request);
        }
        else if (choice == 2) {
            printer.print();
        }
        else if (choice == 3) {
            printer.Show();
        }
        else if (choice == 0) {
            cout << "Poka" << endl;
            break;
        }
        else {
            cout << "Oshibka" << endl;
        }
    }

    return 0;
}
