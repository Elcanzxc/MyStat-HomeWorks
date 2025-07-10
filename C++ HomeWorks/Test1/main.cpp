#include <iostream>
#include <string>
#include "Admin.h"
#include "User.h"
#include "TestManager.h"
#include "ResultManager.h"
#include "Crypto.h"
#include "Menu.h"

using namespace std;

void showMenu() {
    cout << "\n--- Glavnoe menyu ---\n";
    cout << "1. Vojti kak administrator\n";
    cout << "2. Vojti kak pol'zovatel'\n";
    cout << "3. Zaregistrirovatsya kak pol'zovatel'\n";
    cout << "4. Vyiiti\n";
    cout << "Viberite optsiyu: ";
}

/* void adminMenu(Admin& admin, TestManager& testManager) {
    int choice;
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
        cin >> choice;
        cin.ignore();

        switch (choice) {
        case 1: {
            string newLogin;
            cout << "Vvedite novyi login: ";
            getline(cin, newLogin);
            admin.changeLogin(newLogin);
            break;
        }
        case 2: {
            string newPassword;
            cout << "Vvedite novyi parol': ";
            getline(cin, newPassword);
            admin.changePassword(newPassword);
            break;
        }
        case 3:
            admin.addUser();
            break;
        case 4:
            admin.removeUser();
            break;
        case 5:
            admin.modifyUser();
            break;
        case 6:
            admin.viewStatistics();
            break;
        case 7: {
            string category, testName;
            cout << "Vvedite kategoriyu testa: ";
            getline(cin, category);
            cout << "Vvedite nazvanie testa: ";
            getline(cin, testName);
            testManager.addTest(category, testName);
            break;
        }
        case 8: {
            string category, testName, question, correctAnswer, wrongAnswer;
            cout << "Vvedite kategoriyu testa: ";
            getline(cin, category);
            cout << "Vvedite nazvanie testa: ";
            getline(cin, testName);
            cout << "Vvedite vopros: ";
            getline(cin, question);
            cout << "Vvedite pravil'nyi otvet: ";
            getline(cin, correctAnswer);
            cout << "Vvedite nepravil'nyi otvet: ";
            getline(cin, wrongAnswer);
            testManager.addQuestionToTest(category, testName, question, correctAnswer, wrongAnswer);
            break;
        }
        case 9:
            testManager.showAllTests();
            break;
        case 10:
            testManager.saveTestsToFile(); 
            break;
        case 11:
            testManager.loadTestsFromFile(); 
            break;
        case 12:
            cout << "Vykhod iz menyu administratora.\n";
            break;
        default:
            cout << "Nevernyi vibor. Poprobuyte snova.\n";
        }
    } while (choice != 12);
} /*

  /* void userMenu(User& user, TestManager& testManager, ResultManager& resultManager) {
    int choice;
    do {
        cout << "\n--- Menu pol'zovatelya ---\n";
        cout << "1. Proyti test\n";
        cout << "2. Prosmotret' rezultaty\n";
        cout << "3. Vyiiti\n";
        cout << "Viberite optsiyu: ";
        cin >> choice;
        cin.ignore();

        switch (choice) {
        case 1: {
            string category, testName;
            cout << "Vvedite kategoriyu testa: ";
            getline(cin, category);
            cout << "Vvedite nazvanie testa: ";
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
                int correctAnswers = 0;
                int totalQuestions = selectedTest->questions.size();
                for (size_t i = 0; i < totalQuestions; ++i) {
                    cout << selectedTest->questions[i] << "\n";
                    cout << "1. " << selectedTest->correctAnswers[i] << "\n";
                    cout << "2. " << selectedTest->wrongAnswers[i] << "\n";
                    int answer;
                    cout << "Vash otvet (1/2): ";
                    cin >> answer;

                    if (answer == 1) {
                        correctAnswers++;
                    }
                }

                resultManager.saveResult(user.login, testName, correctAnswers, totalQuestions);
                cout << "Test proiden. Vashe rezul'tat: " << correctAnswers << "/" << totalQuestions << " - " << (correctAnswers * 100 / totalQuestions) << "%\n";
            }
            else {
                cout << "Test s takim nazvaniem ne nayden.\n";
            }

            break;
        }
        case 2:
            resultManager.showUserResults(user.login);
            break;
        case 3:
            cout << "Vykhod iz menyu pol'zovatelya.\n";
            break;
        default:
            cout << "Nevernyi vibor. Poprobuyte snova.\n";
        }
    } while (choice != 3);
}*/

int main() {
    string input;
    User user;
    Admin admin;
    TestManager testManager;
    ResultManager resultManager;
    int choice;

    ifstream adminFile("dataAdmin.dat");
    if (adminFile.fail()) {
        string adminLogin, adminPassword;
        cout << "Pervyi zapusk programmi. Ustanovite login i parol' dlya administratori.\n";
        cout << "Vvedite login: ";
        getline(cin, adminLogin);
        cout << "Vvedite parol': ";
        getline(cin, adminPassword);
        admin = Admin(adminLogin, adminPassword);
    }
    else {
        admin = admin.loadAdmin();
    }

    do {
        cout << "\n--- Glavnoe menyu ---\n";
        cout << "1. Vojti kak administrator\n";
        cout << "2. Vojti kak pol'zovatel'\n";
        cout << "3. Zaregistrirovatsya kak pol'zovatel'\n";
        cout << "4. Vyiiti\n";
        cout << "Viberite optsiyu: ";
        

        getline(cin, input);

        try {
            choice = stoi(input);
        }
        catch (...) {
            choice = -1;
        }


        if (choice == 1) {
            string inputLogin, inputPassword;
            cout << "Vvedite login: ";
            getline(cin, inputLogin);
            cout << "Vvedite parol': ";
            getline(cin, inputPassword);

            if (admin.loginAdmin(inputLogin, inputPassword)) {
                adminMenu(admin, testManager);
            }
            else {
                cout << "Nekorrektnyi login ili parol'.\n";
            }
        }
        else if (choice == 2) {
            string inputLogin, inputPassword;
            cout << "Vvedite login: ";
            getline(cin, inputLogin);
            cout << "Vvedite parol': ";
            getline(cin, inputPassword);

            if (user.loginUser(inputLogin, inputPassword)) {
                userMenu(user, testManager, resultManager);
            }
            else {
                cout << "Nekorrektnyi login ili parol'.\n";
            }
        }
        else if (choice == 3) {
            user.registerUser();
        }
        else if (choice == 4) {
            cout << "Vyiiti iz sistemy.\n";
        }
        else {
            cout << "Nevernyi vibor. Poprobuyte snova.\n";
        }
    } while (choice != 4);

    return 0;
}
