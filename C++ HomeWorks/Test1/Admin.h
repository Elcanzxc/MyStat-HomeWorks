#ifndef ADMIN_H
#define ADMIN_H

#include <string>
#include <vector>
#include "User.h"
#include "Test.h"

using namespace std;

class Admin {
private:
    string login;
    string encryptedPassword;

public:
    Admin();
    Admin(const string& adminLogin, const string& adminPassword);

    bool loginAdmin(const string& inputLogin, const string& inputPassword);
    void changeLogin(const string& newLogin);
    void changePassword(const string& newPassword);

    void addUser();
    void removeUser();
    void modifyUser();
    void viewStatistics();

    void saveToFile();
    static Admin loadAdmin();
    void saveAdmin();
};

#endif
