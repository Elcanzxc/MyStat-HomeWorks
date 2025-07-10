#include <iostream>
#include "XYZ.h"

using namespace std;

int main() {
    XYZ a1;

    a1.initialize(12,13,14);
    a1.display();

    cout << a1.getX() << endl;

    a1.Save("data.txt");


    XYZ a2;
    a2.Load("data.txt");

    cout << "Saved: " << endl;
    a2.display();
    return 0;
}