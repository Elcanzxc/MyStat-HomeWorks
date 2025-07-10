#include <iostream>
#include "Animals.h"
#include "Stroka.h"
using namespace std;

int main()
{
	Animals a{ "Jivotnie","Chto to delayut" };
	a.Show();

	Dog b{ "Sobaka" };
	b.Show();

	Cat c{ "Cat" };
	c.Show();

	Parrot d{ "Parrot" };
	d.Show();


	Stroka s1("Hi");
	Stroka s2(" Elcan");
	Stroka s3 = s1 + s2;

	s3.print();

	s3 += Stroka("!!!");
	s3.print();

    cout << "Len: " << s3.length() << endl;

	s3.clear();
	s3.print(); 


}

