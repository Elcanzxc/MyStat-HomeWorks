#include "Shape.h"



Rectangle::Rectangle(double width, double height) : Shape("Rectangle"),width(width), height(height) {}

Circle::Circle(double radius) : Shape("Circle"), radius(radius) {}

RightTriangle::RightTriangle(double base, double height) : Shape("RightTriangle"), base(base), height(height) {}

Trapezoid::Trapezoid(double base1, double base2, double height) : Shape("Trapezoid"), base1(base1), base2(base2), height(height) {}


double Rectangle::area() const {
    return width * height;
}

double Circle::area() const {
    return M_PI * radius * radius;
}

double RightTriangle::area() const {
    return 0.5 * base * height;
}

double Trapezoid::area() const {
    return 0.5 * (base1 + base2) * height;
}