#ifndef CRYPTO_H
#define CRYPTO_H

#include <string>

using namespace std;

namespace Crypto {
    string xorEncryptDecrypt(const string& input, char key = '#');
}

#endif
