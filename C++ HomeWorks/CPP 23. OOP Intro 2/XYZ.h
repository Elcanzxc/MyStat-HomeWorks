#include <iostream>
#include <cstring> 

using namespace std;

class XYZ {
private:

    int x;
    int y;
    int z;


public:

    void display();
    void initialize(const int X, const int Y, const int Z);


    int getX();
    int getY();
    int getZ();



    void setX(const int X);
    void setY(const int Y);
    void setZ(const int Z);


    void Save(const char* file);
    void Load(const char* file);
};