
#include <iostream>
#include <cstring> 

using namespace std;

class Student {
private:
    char FullName[100];
    char Birth[15];
    char Number[20];
    char City[50];
    char Country[50];
    char Universitet[100];
    char CityUniversiteta[50];
    char CountryUniversiteta[50];
    char GroupNumber[20];

public:

    void input();
    void display() const;
    void initialize(const char* fullName, const char* birth, const char* number,
        const char* city, const char* country, const char* universitet,
        const char* cityUniversiteta, const char* countryUniversiteta, const char* groupNumber);


    char* getFullName();
    char* getBirth();
    char* getNumber();
    char* getCity();
    char* getCountry();
    char* getUniversitet();
    char* getCityUniversiteta();
    char* getCountryUniversiteta();
    char* getGroupNumber();


    void setFullName(const char* fullName);
    void setBirth(const char* birth);
    void setNumber(const char* number);
    void setCity(const char* city);
    void setCountry(const char* country);
    void setUniversitet(const char* universitet);
    void setCityUniversiteta(const char* cityUniversiteta);
    void setCountryUniversiteta(const char* countryUniversiteta);
    void setGroupNumber(const char* groupNumber);
};

