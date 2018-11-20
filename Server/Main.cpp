#pragma comment (lib, "ws2_32.lib")

#include "WSAInitializer.h"
#include "Server.h"
#include <iostream>
#include <exception>

#define PORT 8820

using std::cout;
using std::cin;
using std::endl;
using std::exception;

int main()
{
	try
	{
		WSAInitializer wsaInit;
		Server myServer;

		myServer.serve(PORT);
	}
	catch (exception& e)
	{
		cout << "Error occured: " << e.what() << endl;
	}
	system("PAUSE");
	return 0;
}