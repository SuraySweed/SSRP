import socket

HOST = '127.0.0.1'
PORT = 8820

users_dictionary = {}
list

def main():
	
	listening_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # Bind the socket to the port
	server_address = ('localhost', 8820)
	print("\nWELCOME TO THE SSRP SERVER !!\n-----------------------")
	print ("starting up on port", server_address)
	listening_socket.bind(server_address)

    # Listen for incoming connections
	listening_socket.listen(3)

	while True:
		try:
			# Wait for a connection
			print("waiting for a connection\naccepting client...\n-----------------------")
			
			connection, client_address = listening_socket.accept()
			print('connection from', client_address)
			
			while(True):
				msg_from_client = connection.recv(2048)
				
				if msg_from_client:
					print("Received: ", msg_from_client.decode('utf-8'))
					msg_code = get_msg_code(msg_from_client)
					handle_msg_from_client(msg_code, msg_from_client, connection, client_address)
				
				else:
					print("no more data from", client_address)
					break
				
		finally:
            # Clean up the connection
			connection.close()
			listening_socket.close()
     	 

def get_msg_code(msg_from_client):
	return msg_from_client[0:3]
		 
	 
def handle_recevied_name(msg, connection, client_address):
	client_name = (msg.decode('utf-8').split("|"))[1]
	users_dictionary.update({client_name : client_address})		
	
	msg_to_client = "201|HEY " + client_name + ", WELCOME TO THE SSRP SERVER !!!"
	print("send: ", msg_to_client)
	print("-----------------------")
	connection.sendall(msg_to_client.encode())
	
	None 
	
def handle_forwarding_information_request(msg_from_client, connection, client_address):
	
	msg_to_client = "203|0" #false
	
	other_side_client_name = ((msg_from_client.decode('utf-8')).split("|"))[1]
	for key in users_dictionary.keys():
		if(other_side_client_name == key):
			msg_to_client = "203|" + users_dictionary[key][0] + "|" + str(users_dictionary[key][1])
			
	print("sent: ", msg_to_client)
	print("-----------------------")

	connection.sendall(msg_to_client.encode())
	
	None


def handle_msg_from_client(msg_code, msg_from_client, connection, client_address):
	
	if(msg_code == b'100'):
		client_name = handle_recevied_name(msg_from_client, connection, client_address)
	
	elif(msg_code == b'102'):
		handle_forwarding_information_request(msg_from_client, connection, client_address)
		
	
main()
