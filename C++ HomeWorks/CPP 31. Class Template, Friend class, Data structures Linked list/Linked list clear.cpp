#include <iostream>
using namespace std;

// Задание не считается успешным 


// Вы не скинули в гитхаб свой код LinkedList 
// https://github.com/Zamanof/CPP_FSDE_Oct_24_8_ru/blob/master/CPP%2031.%20LinkedList/LinkedList.h
// По этому не смог посмотреть реализацию ваших методов в класее линккед листа
// пересмотрел ваш код в повторе записи в тимсе 




template <typename T>
class Node {
public:
    T data;
    Node* next;

    Node(T x) : data(x), next(nullptr) {}
};



template <typename T>
class LinkedList {

public:

    Node<T>* head;


    LinkedList() : head(nullptr) {}


   
    void Add(T value) {
        Node<T>* NoviyNode = new Node<T>(value);
        if (head == nullptr) {
            head = NoviyNode;
        }
        else { 
            Node<T>* temp = head;
            while (temp->next != nullptr) {
                temp = temp->next;
            }
            temp->next = NoviyNode;
        }
    }

  
    void Print() const {
        Node<T>* etot = head;
        while (etot != nullptr) {
            cout << etot->data << " ";
            etot = etot->next;
        }
        cout << endl;
    }

   
    void clear() {
        while (head != nullptr) {
            Node<T>* temp = head;
            head = head->next;
            delete temp;
        }
    }

    Node<T>* removeAfter(Node<T>* afterPtr) {
		if (afterPtr == nullptr) {
			return nullptr;
		}
		Node<T>* temp = afterPtr->next;
		if (temp != nullptr) {
			afterPtr->next = temp->next;
			delete temp;
		}
		return afterPtr->next;
    }

  
 
};

int main() {
    LinkedList<int> a;
    a.Add(10);
    a.Add(20);
    a.Add(30);
    a.Add(40);

    cout << "Do udaleniya: ";
    a.Print();

	Node<int>* temp = a.head;
	a.removeAfter(temp);
	cout << "Posle udaleniya: ";
	a.Print();

  
    a.clear();
    cout << "Ochistka: ";
    a.Print();

    return 0;

}
