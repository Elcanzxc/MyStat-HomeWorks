#include "ResultManager.h"
#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

Result::Result() : username(""), testName(""), correctAnswers(0), totalQuestions(0), score(0.0), grade("F") {}

Result::Result(const string& user, const string& test, int correct, int total)
    : username(user), testName(test), correctAnswers(correct), totalQuestions(total) {
    calculateGrade();
}

void Result::calculateGrade() {
    double percentage = (static_cast<double>(correctAnswers) / totalQuestions) * 100;
    score = (percentage / 100) * 12;

    if (percentage >= 90) grade = "A";
    else if (percentage >= 75) grade = "B";
    else if (percentage >= 60) grade = "C";
    else if (percentage >= 50) grade = "D";
    else grade = "F";
}

void Result::saveToFile(ofstream& out) const {
    out << username << ";" << testName << ";" << correctAnswers << ";" << totalQuestions << ";" << score << ";" << grade << "\n";
}

vector<Result> Result::loadFromFile() {
    vector<Result> loadedResults;
    ifstream in("dataResults.dat");

    if (!in.is_open()) {
        cout << "Fayl ne nayden, sozdayu noviy...\n";
        return loadedResults;  
    }

    string line;
    while (getline(in, line)) {
        stringstream ss(line);
        string username, testName;
        int correctAnswers, totalQuestions;
        double score;
        string grade;

        getline(ss, username, ';');
        getline(ss, testName, ';');
        ss >> correctAnswers;
        ss.ignore(1, ';');
        ss >> totalQuestions;
        ss.ignore(1, ';');
        ss >> score;
        ss.ignore(1, ';');
        getline(ss, grade, ';');

        loadedResults.push_back(Result(username, testName, correctAnswers, totalQuestions));
        loadedResults.back().grade = grade;
    }

    return loadedResults;
}

void ResultManager::saveResult(const string& username, const string& testName, int correctAnswers, int totalQuestions) {
    Result result(username, testName, correctAnswers, totalQuestions);
    results.push_back(result);

    ofstream out("dataResults.dat", ios::app);
    if (out.is_open()) {
        result.saveToFile(out);
        out.close();
    }
    else {
        cout << "Oshibka pri otkritii fayla dlya soxraneniya.\n";
    }
}

void ResultManager::showResults() const {
    for (const Result& result : results) {
        cout << result.username << " - " << result.testName << ": "
            << result.correctAnswers << "/" << result.totalQuestions
            << " | Score: " << result.score << " | Grade: " << result.grade << endl;
    }
}

void ResultManager::showUserResults(const string& username) const {
    for (const Result& result : results) {
        if (result.username == username) {
            cout << result.testName << ": "
                << result.correctAnswers << "/" << result.totalQuestions
                << " | Score: " << result.score << " | Grade: " << result.grade << endl;
        }
    }
}

void ResultManager::saveResultsToFile() const {
    ofstream out("dataResults.dat");
    for (const Result& result : results) {
        result.saveToFile(out);
    }
}

void ResultManager::loadResultsFromFile() {
    results = Result::loadFromFile();
}
