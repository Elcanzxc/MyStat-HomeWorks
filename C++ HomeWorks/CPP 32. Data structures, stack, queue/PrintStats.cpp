#include "PrintStats.h"

PrintStats::PrintStats() {

}

PrintStats::PrintStats(const Client& client, const string& time)
    : m_client(client), m_time(time) {
}

void PrintStats::Show() const {
    cout << "Client: " << m_client.Show() << endl;
    cout << "Print Time: " << m_time << endl;
}
