#pragma once
#include <iostream>

using namespace std;

class Stroka {
private:
    char* data;

public:
    Stroka();                              
    Stroka(const char* str);         
    Stroka(const Stroka& other);          
    Stroka& operator=(const Stroka& other);
    ~Stroka();                            

    int length() const;               
    void clear();                          

    Stroka operator+(const Stroka& other) const; 
    Stroka& operator+=(const Stroka& other);    

    bool operator==(const Stroka& other) const;  
    bool operator!=(const Stroka& other) const; 

    void print() const;                
};