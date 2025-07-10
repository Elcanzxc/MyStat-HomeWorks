#include "Crypto.h"

using namespace std;

string Crypto::xorEncryptDecrypt(const string& input, char key) {
    string output = input;
    for (char& c : output) {
        c ^= key;
    }
    return output;
}
