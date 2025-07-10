#include "Stroka.h"


Stroka::Stroka() {
    data = new char[1];
    data[0] = '\0';
}

Stroka::Stroka(const char* str) {
    size_t len = strlen(str) + 1;
    data = new char[len];
    strcpy_s(data, len, str);
}


Stroka::Stroka(const Stroka& other) {
    data = new char[strlen(other.data) + 1];
    strcpy_s(data,strlen(data), other.data);
}

Stroka& Stroka::operator=(const Stroka& other) {
    if (this != &other) {
        delete[] data;
        data = new char[strlen(other.data) + 1];
        strcpy_s(data, strlen(data), other.data);
    }
    return *this;
}

Stroka::~Stroka() {
    delete[] data;
}

int Stroka::length() const {
    return strlen(data);
}

void Stroka::clear() {
    delete[] data;
    data = new char[1];
    data[0] = '\0';
}

Stroka Stroka::operator+(const Stroka& other) const {
    int len = strlen(data) + strlen(other.data) + 1;
    char* Str2 = new char[len];
    strcpy_s(Str2, len, data);
    strcat_s(Str2, len, other.data);
    Stroka result(Str2);
    delete[] Str2;
    return result;
}

Stroka& Stroka::operator+=(const Stroka& other) {
    int len = strlen(data) + strlen(other.data) + 1;
    char* newStr = new char[strlen(data) + strlen(other.data) + 1];
    strcpy_s(newStr, len, data);
    strcat_s(newStr, len, other.data);
    delete[] data;
    data = newStr;
    return *this;
}

bool Stroka::operator==(const Stroka& other) const {
    return strcmp(data, other.data) == 0;
}

bool Stroka::operator!=(const Stroka& other) const {
    return !(*this == other);
}

void Stroka::print() const {
    cout << data << endl;
}