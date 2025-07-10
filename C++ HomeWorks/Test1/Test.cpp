#include "Test.h"
#include <fstream>
#include <sstream>

using namespace std;

Test::Test(const string& cat, const string& name) : category(cat), testName(name) {}

void Test::addQuestion(const string& question, const string& correctAnswer, const string& wrongAnswer) {
    questions.push_back(question);
    correctAnswers.push_back(correctAnswer);
    wrongAnswers.push_back(wrongAnswer);
}

void Test::showTest() const {
    cout << "Kategoriya: " << category << endl;
    cout << "Test: " << testName << endl;
    for (size_t i = 0; i < questions.size(); ++i) {
        cout << i + 1 << ". " << questions[i] << endl;
        cout << "Pravilniy otvet: " << correctAnswers[i] << endl;
        cout << "Nepravilniy otvet: " << wrongAnswers[i] << endl;
    }
}

void Test::saveToFile(ofstream& out) const {
    out << category << ";" << testName << "\n";
    for (size_t i = 0; i < questions.size(); ++i) {
        out << questions[i] << ";" << correctAnswers[i] << ";" << wrongAnswers[i] << "\n";
    }
}

vector<Test> Test::loadFromFile(const string& category) {
    vector<Test> tests;
    ifstream in("dataTests.dat");
    string line;
    while (getline(in, line)) {
        stringstream ss(line);
        string cat, testName;
        getline(ss, cat, ';');
        getline(ss, testName, ';');

      
        if (cat == category) {
            Test test(cat, testName);

            
            while (getline(in, line)) {
                stringstream ss2(line);
                string question, correctAnswer, wrongAnswer;
                getline(ss2, question, ';');
                getline(ss2, correctAnswer, ';');
                getline(ss2, wrongAnswer, ';');

                if (!question.empty()) {
                    test.addQuestion(question, correctAnswer, wrongAnswer);
                }
                else {
                 
                    break;
                }
            }
            tests.push_back(test);
        }
    }
    return tests;
}
