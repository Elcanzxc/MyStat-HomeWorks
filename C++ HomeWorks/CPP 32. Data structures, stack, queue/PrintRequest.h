#pragma once
#include <iostream>
#include "Client.h"
using namespace std;

struct PrintRequest {
    Client m_client;
    string m_text;

    PrintRequest();
    PrintRequest(const Client& client, const string& text);

    void Show() const;
};