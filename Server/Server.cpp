#include "Server.h"
#include <exception>
#include <iostream>
#include <string>
#include <thread>

using std::cout;
using std::cin;
using std::endl;
using std::string;
using std::exception;
using std::thread;

Server::Server()
{
	_serverSocket = ::socket(AF_INET,  SOCK_STREAM,  IPPROTO_TCP); 

	if (_serverSocket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__ " - socket");

	_clients.clear();
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
	sa.sin_addr.s_addr = INADDR_ANY; // when there are few ip's for the machine. We will use always "INADDR_ANY"

	if (::bind(_serverSocket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - bind");
	
	if (::listen(_serverSocket, SOMAXCONN) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - listen");
	cout << "Listening on port " << port << endl;

	fd_set master;
	FD_ZERO(&master);

	FD_SET(_serverSocket, &master);

	while (true)
	{
		fd_set copy = master;

		int socketCount = select(0, &copy, nullptr, nullptr, nullptr);

		for (int i = 0; i < socketCount; i++)
		{
			SOCKET sock = copy.fd_array[i];

			if (sock == _serverSocket)
			{
				//accept
				SOCKET client = accept(_serverSocket, nullptr, nullptr);

				FD_SET(client, &master);

				string welcomeMsg = "Welcome!";
				send(client, welcomeMsg.c_str(), welcomeMsg.size() + 1, 0);
			}
			else
			{
				//get message
				char buff[4096];
				ZeroMemory(buff, 4096);

				int bytesIn = recv(sock, buff, 4096, 0);
				if (bytesIn <= 0)
				{
					closesocket(sock);
					FD_CLR(sock, &master);
				}
				else
				{
					
					send(sock, buff, bytesIn, 0);
				}
			}
		}
	}

	/*while (true)
	{
		cout << "Waiting for client connection request..." << endl;
		accept();
		
	}*/
}

void Server::acceptt()
{
	// this accepts the client and create a specific socket from server to this client
	SOCKET client_socket = ::accept(_serverSocket, NULL, NULL);

	if (client_socket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__);

	cout << "Client accepted. SOCKET = " << client_socket << endl;
	_clients.push_back(client_socket);

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
		byte = Helper::getIntPartFromSocket(clientSocket, 4);
		recvMsg = _Helper.getStringPartFromSocket(clientSocket, byte);
		cout << "Client's Socket: " << clientSocket << " ,say: "<< recvMsg << endl;
		_Helper.sendData(clientSocket, msg);


		// Closing the socket (in the level of the TCP protocol)
		//closesocket(clientSocket); 
	}
	catch (const std::exception& e)
	{
		closesocket(clientSocket);
	}
}

//void Server::messageHandler(string msg)
//{
//	if(msg == "")
//}

