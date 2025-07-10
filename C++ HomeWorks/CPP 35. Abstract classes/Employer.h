#pragma once
#include <iostream>
using namespace std;


class Employer {
public:
    virtual void Print() = 0;
    virtual ~Employer() {}    
};

class President : public Employer {
public:
    void Print() override;
};

class Manager : public Employer {
public:
    void Print() override;
};

class Worker : public Employer {
public:
    void Print() override;
};