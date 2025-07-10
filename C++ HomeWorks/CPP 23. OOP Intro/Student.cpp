#include "Student.h"


void Student::initialize(const char* fullName, const char* birth, const char* number,
    const char* city, const char* country, const char* universitet,
    const char* cityUniversiteta, const char* countryUniversiteta, const char* groupNumber) {
    setFullName(fullName);
    setBirth(birth);
    setNumber(number);
    setCity(city);
    setCountry(country);
    setUniversitet(universitet);
    setCityUniversiteta(cityUniversiteta);
    setCountryUniversiteta(countryUniversiteta);
    setGroupNumber(groupNumber);
}

void Student::display() const {
    cout << endl << "--- InforMasiya O studente ---" << endl;
    cout << "FIO: " << FullName << endl;
    cout << "Data Rojdeniya: " << Birth << endl;
    cout << "Telefon: " << Number << endl;
    cout << "Qorod Projivaniya: " << City << endl;
    cout << "Strana Projivaniya: " << Country << endl;
    cout << "Univer: " << Universitet << endl;
    cout << "Qorod Univera: " << CityUniversiteta << endl;
    cout << "Strana Univera: " << CountryUniversiteta << endl;
    cout << "Nomer Qruppi: " << GroupNumber << endl;
}


char* Student::getFullName() { return FullName; }
char* Student::getBirth() { return Birth; }
char* Student::getNumber() { return Number; }
char* Student::getCity() { return City; }
char* Student::getCountry() { return Country; }
char* Student::getUniversitet() { return Universitet; }
char* Student::getCityUniversiteta() { return CityUniversiteta; }
char* Student::getCountryUniversiteta() { return CountryUniversiteta; }
char* Student::getGroupNumber() { return GroupNumber; }


void Student::setFullName(const char* fullName) { strcpy_s(FullName, 100, fullName); }
void Student::setBirth(const char* birth) { strcpy_s(Birth, 15, birth); }
void Student::setNumber(const char* number) { strcpy_s(Number, 20, number); }
void Student::setCity(const char* city) { strcpy_s(City, 50, city); }
void Student::setCountry(const char* country) { strcpy_s(Country, 50, country); }
void Student::setUniversitet(const char* universitet) { strcpy_s(Universitet, 100, universitet); }
void Student::setCityUniversiteta(const char* cityUniversiteta) { strcpy_s(CityUniversiteta, 50, cityUniversiteta); }
void Student::setCountryUniversiteta(const char* countryUniversiteta) { strcpy_s(CountryUniversiteta, 50, countryUniversiteta); }
void Student::setGroupNumber(const char* groupNumber) { strcpy_s(GroupNumber, 20, groupNumber); }
