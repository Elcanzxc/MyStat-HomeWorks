#include "Printer.h"


Printer::Printer(int Size) : maxSize(Size) {}

void Printer::addRequest(const PrintRequest& request) {
    m_request.push(request);
    cout << "Dobavil Zapros: ";
    request.Show();
    cout << endl;

    if (m_request.size() >= maxSize) {
        cout << "Nachinayu pecatat" << endl;
        print();
    }
}

void Printer::print() {
    while (!m_request.empty()) {
        PrintRequest etot = m_request.front();
        m_request.pop();

        cout << "Print" << endl;
        etot.Show();
        cout << endl;

        string time = getTime();
        m_stats.push(PrintStats(etot.m_client, time));
    }
}

void Printer::Show() const {
    queue<PrintStats> etot = m_stats;

    if (etot.empty()) {
        cout << "Pusto" << endl;
        return;
    }

    cout << "Print Statistic";
    while (!etot.empty()) {
        etot.front().Show();
        cout << endl;
        etot.pop();
    }
}

string Printer::getTime() const {
    // Когда то очень давно я использовал библиотеку ctime
    // когда писал задание я забыл вообще как он работает
    // у чатджпт спросил 

    time_t thisTime = time(0); // берет время из 1970 до нынешней в секунды
    char Time[50]; 
    ctime_s(Time,50,&thisTime); // запоминает в нужном формате как строка


    return (string)Time;
}
