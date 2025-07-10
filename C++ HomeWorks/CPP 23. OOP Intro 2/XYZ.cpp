#include "XYZ.h"


void XYZ::initialize(const int X, const int Y, const int Z) {
    setX(X);
    setY(Y);
    setZ(Z);

}

void XYZ::display() {
    cout << "X: " << x << endl;
    cout << "Y: " << y << endl;
    cout << "Z: " << z << endl;

}


int XYZ::getX() { return x; }
int XYZ::getY() { return y; }
int XYZ::getZ() { return z; }



void XYZ::setX(const int X) { x = X; }
void XYZ::setY(const int Y) { y = Y; }
void XYZ::setZ(const int Z) { z = Z; }


void XYZ::Save(const char* file) {
    FILE* my_file;
    fopen_s(&my_file, file, "w");
    if (my_file != nullptr) {
        fprintf(my_file, "%d %d %d\n", x, y, z);
        fclose(my_file);
        cout << "Soxranil: " << file << endl;
    } else {
        cout << "Ne udalos soxranit fayl: " << file << endl;
    }
}


void XYZ::Load(const char* file) {
    FILE* my_file;
    fopen_s(&my_file, file, "r");
    if (my_file != nullptr) {
        fscanf_s(my_file, "%d %d %d", &x, &y, &z);
        fclose(my_file);
        cout << "Zaqruzil:  " << file << endl;
    }
    else {
        cout << "Ne udalos otkryt fayl: " << file << endl;
    }
}