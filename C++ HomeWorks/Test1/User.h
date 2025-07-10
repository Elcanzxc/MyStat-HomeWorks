#ifndef USER_H
#define USER_H

#include <string>
#include <vector>

using namespace std;

class User {
public:
    string fullName;
    string address;
    string phone;
    string login;
    string encryptedPassword;
    vector<int> passedTestIds;

    void registerUser();
    bool loginUser(const string& inputLogin, const string& inputPassword);
    bool checkPassword(const string& inputPassword) const;
    void showResults() const;

    void saveToFile() const;
    static bool isLoginUnique(const string& login);
    static User loadByLogin(const string& login);
};

#endif
