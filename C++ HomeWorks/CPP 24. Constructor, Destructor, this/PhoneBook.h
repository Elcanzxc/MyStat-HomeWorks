#pragma once

#include <iostream>
#include <cstdio>
#include <cstring>

using namespace std;

class Contact {
private:
    char* name;
    char homePhone[20];
    char workPhone[20];
    char mobilePhone[20];
    char DopInfo[100];

public:
    Contact(const char* name, const char* homePhone, const char* workPhone, const char* mobilePhone, const char* DopInfo);
    ~Contact();
    void display() const;
    const char* getName() const;
    void saveToFile(FILE* file) const;
    static Contact* loadFromFile(FILE* file);
};

class PhoneBook {
private:
    Contact* contacts[101]{};
    int ContactCount;

public:
    PhoneBook();
    ~PhoneBook();
    void addContact(const char* name, const char* homePhone, const char* workPhone, const char* mobilePhone, const char* DopInfo);
    void removeContact(const char* name);
    void searchContact(const char* name) const;
    void displayAll() const;
    void saveToFile(const char* filename) const;
    void loadFromFile(const char* filename);
};




