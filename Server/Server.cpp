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
	// notice that we step out to the global namespace
	// for the resolution of the function socket
	
	// this server use TCP. that why SOCK_STREAM & IPPROTO_TCP
	// if the server use UDP we will use: SOCK_DGRAM & IPPROTO_UDP
	_serverSocket = ::socket(AF_INET,  SOCK_STREAM,  IPPROTO_TCP); 

	if (_serverSocket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__ " - socket");
}

Server::~Server()
{
	try
	{
		// the only use of the destructor should be for freeing 
		// resources that was allocated in the constructor
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

	// again stepping out to the global namespace
	// Connects between the socket and the configuration (port and etc..)
	if (::bind(_serverSocket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - bind");
	
	// Start listening for incoming requests of clients
	if (::listen(_serverSocket, SOMAXCONN) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - listen");
	cout << "Listening on port " << port << endl;

	while (true)
	{
		// the main thread is only accepting clients 
		// and add then to the list of handlers
		cout << "Waiting for client connection request" << endl;
		accept();
	}
}


void Server::accept()
{
	// this accepts the client and create a specific socket from server to this client
	SOCKET client_socket = ::accept(_serverSocket, NULL, NULL);

	if (client_socket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__);

	cout << "Client accepted !.  SOCKET = " << client_socket << endl;

	// the function that handle the conversation with the client
	clientHandler(client_socket);
}


void Server::clientHandler(SOCKET clientSocket)
{
	string msg = "ok, good";
	string recvMsg;
	int byte = 0;
	try
	{
		//_Helper.sendData(clientSocket, msg);
		//send(clientSocket, msg.c_str(), msg.size(), 0);
		while (TRUE)
		{
			byte = Helper::getIntPartFromSocket(clientSocket, 4);
			recvMsg = _Helper.getStringPartFromSocket(clientSocket, byte);
			cout << "the msg from the client is: " << recvMsg << endl;
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

