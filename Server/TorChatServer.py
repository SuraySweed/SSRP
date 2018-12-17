import socket

HOST = '127.0.0.1'
PORT = 8820

def main():

    listening_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # Bind the socket to the port
    server_address = ('localhost', 8820)
    print ("starting up on port", server_address)
    listening_socket.bind(server_address)

    # Listen for incoming connections
    listening_socket.listen(3)

    while True:
        # Wait for a connection
        print("waiting for a connection\naccepting client...")
        connection, client_address = listening_socket.accept()
        try:
            print('connection from', client_address)
            connection.sendall(b"hello There")

            # Receive the data in small chunks and retransmit it
            while True:
                data = connection.recv(2048)
                print("received: ", data.decode('utf-8'))
                if data:
                    print('sending data back to the client')
                    connection.sendall(data)
                else:
                    print('no more data from', client_address)
                    break

        finally:
            # Clean up the connection
            connection.close()
            listening_socket.close()


   
main()
