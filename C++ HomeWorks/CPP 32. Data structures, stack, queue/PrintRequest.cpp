#include "PrintRequest.h"

PrintRequest::PrintRequest(const Client& client, const string& text)
    : m_client(client), m_text(text) {
}

void PrintRequest::Show() const {
    cout << "Client: " << m_client.Show() << endl;
    cout << "Text for print: " << m_text << endl;
}
