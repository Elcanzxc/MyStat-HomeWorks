#ifndef TEST_MANAGER_H
#define TEST_MANAGER_H

#include <vector>
#include <string>
#include "Test.h"

using namespace std;

class TestManager {
private:
    vector<Test> tests;

public:
    void addTest(const string& category, const string& testName);
    void addQuestionToTest(const string& category, const string& testName, const string& question, const string& correctAnswer, const string& wrongAnswer);
    void showTest(const string& category, const string& testName) const;
    void showAllTests() const;
    void saveTestsToFile() const;
    void loadTestsFromFile();

    vector<Test> getTestsByCategory(const string& category) const;
    vector<Test> loadTestsByCategory(const string& category);
};

#endif
