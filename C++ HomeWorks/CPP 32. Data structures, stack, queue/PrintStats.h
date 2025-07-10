#pragma once

#include <iostream>
#include "Client.h"
using namespace std;

struct PrintStats {
    Client m_client;
    string m_time;

    PrintStats();
    PrintStats(const Client& client, const string& time);

    void Show() const;
   
};

