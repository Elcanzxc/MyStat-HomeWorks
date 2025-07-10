#include "Menu.h"
#include <iostream>

using namespace std;

void adminMenu(Admin& admin, TestManager& testManager) {
    int choice;
    string input;
    do {
        cout << "\n--- Menu administratora ---\n";
        cout << "1. Izmenit' login\n";
        cout << "2. Izmenit' parol'\n";
        cout << "3. Dobavit' pol'zovatelya\n";
        cout << "4. Udalit' pol'zovatelya\n";
        cout << "5. Izmenit' pol'zovatelya\n";
        cout << "6. Prosmotr statistiki\n";
        cout << "7. Dobavit' test\n";
        cout << "8. Dobavit' vopros k testu\n";
        cout << "9. Prosmotret' vse testi\n";
        cout << "10. Sohranit' testi v fail\n";
        cout << "11. Zaqruzit testi iz faila\n";
        cout << "12. Vyiiti\n";
        cout << "Viberite optsiyu: ";
        getline(cin, input);

        try { choice = stoi(input); }
        catch (...) { choice = -1; }

        switch (choice) {
        case 1: {
            string newLogin;
            cout << "Novyi login: ";
            getline(cin, newLogin);
            admin.changeLogin(newLogin);
            break;
        }
        case 2: {
            string newPassword;
            cout << "Novyi parol': ";
            getline(cin, newPassword);
            admin.changePassword(newPassword);
            break;
        }
        case 3: admin.addUser(); break;
        case 4: admin.removeUser(); break;
        case 5: admin.modifyUser(); break;
        case 6: admin.viewStatistics(); break;
        case 7: {
            string category, testName;
            cout << "Kategoria: ";
            getline(cin, category);
            cout << "Nazvanie testa: ";
            getline(cin, testName);
            testManager.addTest(category, testName);
            break;
        }
        case 8: {
            string category, testName, question, correct, wrong;
            cout << "Kategoria: ";
            getline(cin, category);
            cout << "Nazvanie testa: ";
            getline(cin, testName);
            cout << "Vopros: ";
            getline(cin, question);
            cout << "Pravil'nyi otvet: ";
            getline(cin, correct);
            cout << "Nepravil'nyi otvet: ";
            getline(cin, wrong);
            testManager.addQuestionToTest(category, testName, question, correct, wrong);
            break;
        }
        case 9: testManager.showAllTests(); break;
        case 10: testManager.saveTestsToFile(); break;
        case 11: testManager.loadTestsFromFile(); break;
        case 12: cout << "Vykhod iz menyu administratora.\n"; break;
        default: cout << "Nevernyi vibor. Poprobuyte snova.\n";
        }
    } while (choice != 12);
}

void userMenu(User& user, TestManager& testManager, ResultManager& resultManager) {
    int choice;
    string input;
    do {
        cout << "\n--- Menu pol'zovatelya ---\n";
        cout << "1. Proyti test\n";
        cout << "2. Prosmotret' rezultaty\n";
        cout << "3. Vyiiti\n";
        cout << "Viberite optsiyu: ";
        getline(cin, input);

        try { choice = stoi(input); }
        catch (...) { choice = -1; }

        switch (choice) {
        case 1: {
            string category, testName;
            cout << "Kategoria: ";
            getline(cin, category);
            cout << "Nazvanie testa: ";
            getline(cin, testName);

            vector<Test> tests = testManager.getTestsByCategory(category);
            Test* selectedTest = nullptr;
            for (auto& test : tests) {
                if (test.category == category && test.testName == testName) {
                    selectedTest = &test;
                    break;
                }
            }

            if (selectedTest) {
                int correct = 0;
                int total = selectedTest->questions.size();
                for (size_t i = 0; i < total; ++i) {
                    cout << selectedTest->questions[i] << "\n";
                    cout << "1. " << selectedTest->correctAnswers[i] << "\n";
                    cout << "2. " << selectedTest->wrongAnswers[i] << "\n";
                    cout << "Vash otvet (1/2): ";
                    getline(cin, input);
                    if (input == "1") correct++;
                }
                resultManager.saveResult(user.login, testName, correct, total);
                cout << "Test zavershen. Rezultat: " << correct << "/" << total << " (" << (correct * 100 / total) << "%)\n";
            }
            else {
                cout << "Test ne nayden.\n";
            }
            break;
        }
        case 2: resultManager.showUserResults(user.login); break;
        case 3: cout << "Vykhod iz menyu pol'zovatelya.\n"; break;
        default: cout << "Nevernyi vibor. Poprobuyte snova.\n";
        }
    } while (choice != 3);
}
