#include "Admin.h"
#include "Crypto.h"
#include "User.h"
#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

Admin::Admin(const string& adminLogin, const string& adminPassword) {
    login = adminLogin;
    encryptedPassword = Crypto::xorEncryptDecrypt(adminPassword, '%');
    saveToFile();
}
Admin::Admin() {
 
}

void Admin::saveAdmin() {
    ofstream out("dataAdmin.dat");
    out << login << ";" << encryptedPassword << endl;
}

Admin Admin::loadAdmin() {
    ifstream in("dataAdmin.dat");
    string line;
    getline(in, line);
    stringstream ss(line);
    string loginFromFile, password;
    getline(ss, loginFromFile, ';');
    getline(ss, password, ';');
    return Admin(loginFromFile, password);
}

bool Admin::loginAdmin(const string& inputLogin, const string& inputPassword) {
    Admin admin = loadAdmin();
    string encryptedInputPassword = Crypto::xorEncryptDecrypt(inputPassword, '%');

    if (admin.login == inputLogin && admin.encryptedPassword == encryptedInputPassword) {
        return true;
    }
    return false;
}

void Admin::changeLogin(const string& newLogin) {
    login = newLogin;
    saveToFile();
}

void Admin::changePassword(const string& newPassword) {
    encryptedPassword = Crypto::xorEncryptDecrypt(newPassword, '%');
    saveToFile();
}

void Admin::addUser() {
    User user;
    user.registerUser();
}

void Admin::removeUser() {
    string login;
    cout << "Vvedite login pol'zovatelya dlya udaleniya: ";
    getline(cin, login);

    ifstream in("dataUsers.dat");
    ofstream temp("tempUsers.dat");

    string line;
    bool found = false;
    while (getline(in, line)) {
        stringstream ss(line);
        string loginFromFile;
        getline(ss, loginFromFile, ';');
        if (loginFromFile != login) {
            temp << line << endl;
        }
        else {
            found = true;
        }
    }

    in.close();
    temp.close();

    remove("dataUsers.dat");
    rename("tempUsers.dat", "dataUsers.dat");

    if (found) {
        cout << "Pol'zovatel' udalen.\n";
    }
    else {
        cout << "Pol'zovatel' s takim loginom ne nayden.\n";
    }
}

void Admin::modifyUser() {
    string login;
    cout << "Vvedite login pol'zovatelya dlya izmeneniya: ";
    getline(cin, login);

    ifstream in("dataUsers.dat");
    ofstream temp("tempUsers.dat");

    string line;
    bool found = false;
    while (getline(in, line)) {
        stringstream ss(line);
        string loginFromFile;
        getline(ss, loginFromFile, ';');
        if (loginFromFile == login) {
            User user = User::loadByLogin(login);
            user.registerUser();
            temp << user.login << ";" << user.encryptedPassword << ";" << user.fullName << ";" << user.address << ";" << user.phone << "\n";
            found = true;
        }
        else {
            temp << line << endl;
        }
    }

    in.close();
    temp.close();

    remove("dataUsers.dat");
    rename("tempUsers.dat", "dataUsers.dat");

    if (found) {
        cout << "Pol'zovatel' obnavlen.\n";
    }
    else {
        cout << "Pol'zovatel' s takim loginom ne nayden.\n";
    }
}

void Admin::viewStatistics() {
    ifstream usersFile("dataUsers.dat");
    string line;
    int userCount = 0;

    while (getline(usersFile, line)) {
        userCount++;
    }

    ifstream testsFile("dataTests.dat");
    int testCount = 0;

    while (getline(testsFile, line)) {
        stringstream ss(line);
        string category, testName;
        getline(ss, category, ';');
        getline(ss, testName, ';');
        testCount++;
    }

    ifstream resultsFile("dataResults.dat");
    int resultCount = 0;

    while (getline(resultsFile, line)) {
        resultCount++;
    }

    cout << "Statistika:\n";
    cout << "Kolichestvo pol'zovateley: " << userCount << endl;
    cout << "Kolichestvo testov: " << testCount << endl;
    cout << "Kolichestvo rezul'tatov: " << resultCount << endl;
}

void Admin::saveToFile() {
    ofstream out("dataAdmin.dat");
    out << login << ";" << encryptedPassword << "\n";
    out.close();
}
