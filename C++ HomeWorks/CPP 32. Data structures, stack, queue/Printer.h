#pragma once

#include <queue>
#include "PrintRequest.h"
#include "PrintStats.h"
#include <iostream>
#include <ctime>

using namespace std;

class Printer {
private:
    queue<PrintRequest> m_request;
    queue<PrintStats> m_stats;
    int maxSize;

    string getTime() const;

public:
    Printer(int Size = 3);

    void addRequest(const PrintRequest& request);
    void print();
    void Show() const;
};



