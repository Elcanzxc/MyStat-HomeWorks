#include "User.h"
#include "Crypto.h"
#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

void User::registerUser() {
    cout << "Vvedite FIO: ";
    getline(cin, fullName);

    cout << "Vvedite adres: ";
    getline(cin, address);

    cout << "Vvedite telefon: ";
    getline(cin, phone);


    do {
        cout << "Vvedite login: ";
        getline(cin, login);
        if (!isLoginUnique(login)) {
            cout << "Login uje sushchestvuet. Poprobuyte drugoi.\n";
        }
    } while (!isLoginUnique(login));

    string password;
    cout << "Vvedite parol': ";
    getline(cin, password);
    encryptedPassword = Crypto::xorEncryptDecrypt(password);

    saveToFile();
    cout << "Registratsiya proshla uspeshno.\n";
}

bool User::loginUser(const string& inputLogin, const string& inputPassword) {
    if (!isLoginUnique(inputLogin)) {
        User user = loadByLogin(inputLogin);
        if (user.checkPassword(inputPassword)) {
            *this = user;
            return true;
        }
    }
    return false;
}

bool User::checkPassword(const string& inputPassword) const {
    return Crypto::xorEncryptDecrypt(encryptedPassword) == inputPassword;
}

void User::saveToFile() const {
    ofstream out("dataUsers.dat", ios::app);
    out << login << ";" << encryptedPassword << ";" << fullName << ";" << address << ";" << phone << "\n";
    out.close();
}

bool User::isLoginUnique(const string& login) {
    ifstream in("dataUsers.dat");
    string line;
    while (getline(in, line)) {
        stringstream ss(line);
        string loginFromFile;
        getline(ss, loginFromFile, ';');
        if (loginFromFile == login) {
            return false;
        }
    }
    return true;
}

User User::loadByLogin(const string& login) {
    ifstream in("dataUsers.dat");
    string line;
    while (getline(in, line)) {
        stringstream ss(line);
        string loginFromFile, password, name, addr, tel;
        getline(ss, loginFromFile, ';');
        if (loginFromFile == login) {
            getline(ss, password, ';');
            getline(ss, name, ';');
            getline(ss, addr, ';');
            getline(ss, tel, ';');
            return User{ name, addr, tel, loginFromFile, password };
        }
    }
    return User{};
}

void User::showResults() const {
    cout << "Funkciya prosmotra rezul'tatov poka ne realizovana.\n";
}
