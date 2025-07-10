#pragma once
#include <string>

namespace InputValid {
    bool isValidLogin(const std::string& login);
    bool isValidPassword(const std::string& password);
    bool isValidMenuChoice(const std::string& input, int min, int max, int& result);
}
