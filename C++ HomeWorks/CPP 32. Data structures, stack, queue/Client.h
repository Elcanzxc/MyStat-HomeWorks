#pragma once
#include <iostream>
using namespace std;

struct Client {
    string m_name;
    string m_surname;

    Client();

    Client(const string& name, const string& surname);

    string Show() const;
};
