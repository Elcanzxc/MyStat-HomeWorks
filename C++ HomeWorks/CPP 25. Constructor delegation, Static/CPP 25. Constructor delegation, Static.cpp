#include <iostream>
using namespace std;
// Всё в одном файле
class String {
private:
    char* stroka;
    int length;
    static int num;

public:

    String() : String(80) {}

    String(int size) : length(size) {
        stroka = new char[length + 1];
        num++;
    }

    String(const char* str) : length(strlen(str)) {
        stroka = new char[length + 1];
        strcpy_s(stroka, length + 1, str);
        num++;
    }

    ~String() {
        delete[] stroka;
        num--;
    }

    void input() {
        cout << "Vvedi Stroku: ";
        cin.getline(stroka, length + 1);
    }

    void output() const {
        cout << "Stroka: " << stroka << endl;
    }

    static int getNum() {
        return num;
    }
};

int String::num = 0;

int main() {
    String str1;
    str1.input();
    str1.output();

    String str2(41);

    String str3("Salam Aleykum");
    str3.output();

    cout << "Number: " << String::getNum() << endl;
    return 0;
}
