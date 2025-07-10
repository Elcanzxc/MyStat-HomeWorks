#include <iostream>
#include "Student.h"

using namespace std;

int main() {
    Student student;

    student.initialize("Ivanov Ivan Ivanovich", "01.01.2000", "+380123456789", "Kiev", "Ukraine", "KPI", "Kiev", "Ukraine", "KP-01");
    student.display();

    cout << student.getFullName();
    return 0;
}