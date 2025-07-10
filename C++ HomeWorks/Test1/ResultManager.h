#ifndef RESULT_MANAGER_H
#define RESULT_MANAGER_H

#include <vector>
#include <string>
#include <fstream>
#include "Test.h"

using namespace std;

class Result {
public:
    string username;
    string testName;
    int correctAnswers;
    int totalQuestions;
    double score;
    string grade;

    Result();  // Конструктор по умолчанию
    Result(const string& user, const string& test, int correct, int total);  // Конструктор с параметрами

    void calculateGrade();  // Метод для расчета оценки
    void saveToFile(ofstream& out) const;  // Метод для сохранения в файл
    static vector<Result> loadFromFile();  // Статический метод для загрузки всех результатов
};

class ResultManager {
private:
    vector<Result> results;

public:
    void saveResult(const string& username, const string& testName, int correctAnswers, int totalQuestions);  // Сохранить новый результат
    void showResults() const;  // Показать все результаты
    void showUserResults(const string& username) const;  // Показать результаты по пользователю
    void saveResultsToFile() const;  // Сохранить все результаты в файл
    void loadResultsFromFile();  // Загрузить результаты из файла
};

#endif
