#include "Server.h"
#include <exception>
#include <iostream>
#include <string>

using std::cout;
using std::cin;
using std::endl;
using std::string;
using std::exception;

Server::Server()
{
	_serverSocket = ::socket(AF_INET,  SOCK_STREAM,  IPPROTO_TCP); 

	if (_serverSocket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__ " - socket");
}

Server::~Server()
{
	try
	{
		::closesocket(_serverSocket);
	}
	catch (...) {}
}

void Server::serve(int port)
{
	struct sockaddr_in sa = { 0 };
	
	sa.sin_port = htons(port); // port that server will listen for
	sa.sin_family = AF_INET;   // must be AF_INET
	sa.sin_addr.s_addr = INADDR_ANY;    // when there are few ip's for the machine. We will use always "INADDR_ANY"

	if (::bind(_serverSocket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - bind");
	
	if (::listen(_serverSocket, SOMAXCONN) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - listen");
	cout << "Listening on port " << port << endl;

	while (true)
	{
		cout << "Waiting for client connection request..." << endl;
		accept();
	}
}

void Server::accept()
{
	// this accepts the client and create a specific socket from server to this client
	SOCKET client_socket = ::accept(_serverSocket, NULL, NULL);

	if (client_socket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__);

	cout << "Client accepted. SOCKET = " << client_socket << endl;

	// the function that handle the conversation with the client
	clientHandler(client_socket);
}

void Server::clientHandler(SOCKET clientSocket)
{
	string msg = "ok, good\n";
	string recvMsg;
	int byte = 0;

	try
	{
		while (TRUE)
		{
			byte = Helper::getIntPartFromSocket(clientSocket, 4);
			recvMsg = _Helper.getStringPartFromSocket(clientSocket, byte);
			cout << "Client's Socket: " << clientSocket << " ,say: "<< recvMsg << endl;
			_Helper.sendData(clientSocket, msg);
		}


		// Closing the socket (in the level of the TCP protocol)
		closesocket(clientSocket); 
	}

	catch (const std::exception& e)
	{
		closesocket(clientSocket);
	}
}

