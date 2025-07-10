#ifndef TEST_H
#define TEST_H

#include <string>
#include <vector>
#include <iostream>

using namespace std;

class Test {
public:
    string category;
    string testName;
    vector<string> questions;
    vector<string> correctAnswers;
    vector<string> wrongAnswers;

    Test(const string& cat, const string& name);

    void addQuestion(const string& question, const string& correctAnswer, const string& wrongAnswer);
    void showTest() const;
    void saveToFile(ofstream& out) const;
    static vector<Test> loadFromFile(const string& category);




    string getCategory() const { return category; }
    string getTestName() const { return testName; }


};

#endif
