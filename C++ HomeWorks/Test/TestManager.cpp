#include "TestManager.h"
#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

void TestManager::addTest(const string& category, const string& testName) {
    for (const Test& test : tests) {
        if (test.category == category && test.testName == testName) {
            cout << "Test with this name already exists in the given category.\n";
            return;
        }
    }

    Test test(category, testName);
    tests.push_back(test);
    cout << "Test uspesno dobavlen.\n";
}

void TestManager::addQuestionToTest(const string& category, const string& testName, const string& question, const string& correctAnswer, const string& wrongAnswer) {
    bool testFound = false;

    for (Test& test : tests) {
        if (test.category == category && test.testName == testName) {
            test.addQuestion(question, correctAnswer, wrongAnswer);
            testFound = true;
            cout << "Vopros uspesno dobavlen v test\n";
            break;
        }
    }

    if (!testFound) {
        cout << "Test s takoy kategoriey ne nayden.\n";
    }
}

void TestManager::showTest(const string& category, const string& testName) const {
    for (const Test& test : tests) {
        if (test.category == category && test.testName == testName) {
            test.showTest();
            return;
        }
    }
    cout << "Test ne nashelsya.\n";
}

void TestManager::showAllTests() const {
    if (tests.empty()) {
        cout << "No tests available.\n";
        return;
    }

    for (const Test& test : tests) {
        test.showTest();
        cout << "--------------------------\n";
    }
}
void TestManager::saveTestsToFile() const {

    ofstream out("dataTests.dat");
    if (!out.is_open()) {
        cout << "Oshibka pri otkritii fayla dlya soxraneniya.\n";
        return;
    }

    for (const Test& test : tests) {
        test.saveToFile(out); 
    }
    cout << "Testy uspesno sohranyony v fail.\n";
}

void TestManager::loadTestsFromFile() {
    ifstream in("dataTests.dat");
    if (!in.is_open()) {
        cout << "Fail dlya zagruski testov ne nakhoditsya.\n";
        return;
    }

    string line;
    while (getline(in, line)) {
        stringstream ss(line);
        string category, testName;

        
        getline(ss, category, ';');
        getline(ss, testName, ';');

        addTest(category, testName);
    }
    cout << "Testy uspesno zagruzheny iz faila.\n";
}

vector<Test> TestManager::getTestsByCategory(const string& category) const {
    vector<Test> filteredTests;
    for (const Test& test : tests) {
        if (test.category == category) {
            filteredTests.push_back(test);
        }
    }
    return filteredTests;
}

vector<Test> TestManager::loadTestsByCategory(const string& category) {
    vector<Test> filteredTests;
    for (const Test& test : tests) {
        if (test.category == category) {
            filteredTests.push_back(test);
        }
    }
    return filteredTests;
}