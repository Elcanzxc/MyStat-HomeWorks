#include "Client.h"

Client::Client()
{}

Client::Client(const string& name, const string& surname)
        : m_name(name), m_surname(surname) {
    }

string Client::Show() const {
        return m_name + " " + m_surname;
    }