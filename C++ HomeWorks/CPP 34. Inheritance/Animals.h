#pragma once
#include <iostream>
#include <string>
using namespace std;

class Animals
{
protected:

    string m_name;
    string m_type;


public:
    Animals(string name, string type);

    void Show();
};

class Dog : public Animals {
public:
    Dog(string name);
};

class Cat : public Animals {
public:
    Cat(string name);
};

class Parrot : public Animals {
public:
    Parrot(string name);
};