#pragma once
#include <iostream>
#define M_PI 3.14159265358979323846
using namespace std;

class Shape {
public:
    string name;
    Shape(const string& name) : name(name) {}
    virtual double area() const = 0; 
    virtual ~Shape() {}

    string getName() const { return name; }
};

class Rectangle : public Shape {
private:
    double width, height;
public:
    Rectangle(double width, double heighth);
    double area() const override;
};

class Circle : public Shape {
private:
    double radius;
public:
    Circle(double radius);
    double area() const override;
};

class RightTriangle : public Shape {
private:
    double base, height;
public:
    RightTriangle(double base, double height);
    double area() const override;
};

class Trapezoid : public Shape {
private:
    double base1, base2, height;
public:
    Trapezoid(double base1, double base2, double height);
    double area() const override;
};
