
#include <iostream>
#include "Employer.h"
#include "Shape.h"
#include <vector>

using namespace std;

void Print(Employer& data) {
    data.Print();
}
void Print(Shape* data) {
    cout<<"area of "<<data->getName() <<": "<<data->area() << endl;
}


int main()
{
    President president;
    Manager manager;
    Worker worker;

    Print(president);
    Print(manager);
    Print(worker);


    Rectangle rect(4.0, 5.0);
    Circle circle(3.0);
    RightTriangle triangle(3.0, 4.0);
    Trapezoid trapezoid(3.0, 5.0, 4.0);

    vector<Shape*> shapes;
    shapes.push_back(&rect);
    shapes.push_back(&circle);
    shapes.push_back(&triangle);
    shapes.push_back(&trapezoid);

    Print(&rect);
    Print(&circle);
    Print(&triangle);
    Print(&trapezoid);
}
