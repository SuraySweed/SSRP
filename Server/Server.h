#pragma once

#include <WinSock2.h>
#include <Windows.h>
#include "Helper.h"
#include <vector>

using std::vector;

class Server
{
public:
	Server();
	~Server();
	void serve(int port);

private:

	void acceptt();
	void clientHandler(SOCKET clientSocket);
	//void messageHandler(string msg);

	vector<SOCKET> _clients;
	SOCKET _serverSocket;
	Helper _Helper;
};

