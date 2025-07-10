#include "InputValid.h"
#include <regex>
#include <sstream>

bool InputValid::isValidLogin(const std::string& login) {
    return std::regex_match(login, std::regex("^[A-Za-z][A-Za-z0-9_]{2,15}$"));
}

bool InputValid::isValidPassword(const std::string& password) {
    return password.length() >= 4;
}

bool InputValid::isValidMenuChoice(const std::string& input, int min, int max, int& result) {
    std::stringstream ss(input);
    if (ss >> result && ss.eof() && result >= min && result <= max) {
        return true;
    }
    result = -1;
    return false;
}
