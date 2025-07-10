#include "Animals.h"




Animals::Animals(string name, string type) : m_name(name), m_type(type) {}

 void Animals::Show() {
        cout << "Name: " << m_name << ", Chto delaet: " << m_type << endl;
    }

 Dog::Dog(string name) : Animals(name, "Laet") {}

 Cat::Cat(string name) : Animals(name, "Myaukaet") {}

 Parrot::Parrot(string name) : Animals(name, "Povtoryaet slova") {}