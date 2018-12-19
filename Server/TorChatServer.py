import socket

HOST = '127.0.0.1'
PORT = 8820

users_dictionary = {}

def main():
	
	client_name = ""
	
	listening_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # Bind the socket to the port
	server_address = ('localhost', 8820)
	print ("starting up on port", server_address)
	listening_socket.bind(server_address)

    # Listen for incoming connections
	listening_socket.listen(3)

	while True:
		try:
			# Wait for a connection
			print("waiting for a connection\naccepting client...")
			
			connection, client_address = listening_socket.accept()
			print('connection from', client_address)

			msg_from_server = connection.recv(128)
			print("received: ", msg_from_server.decode('utf-8'))
			msg_code = get_msg_code(msg_from_server)
		
			if(msg_code == 100):
				client_name = handle_recevied_name(msg_from_server)	
			
			msg_to_client = b'("HEY ", client_name, ", WELCOME TO THE SSRP SERVER !!!")'
			connection.sendall(msg_to_client)
			
			msg_from_server = connection.recv(256)
			msg_code = get_msg_code(msg_from_server)
			if(msg_code == 102):
				msg_to_client = handle_forwarding_information_request(msg_from_server)
				connection.sendall(msg_to_client)
			
			
			users_dictionary.update({client_name : client_address})		
			for key,value in users_dictionary.items():
				print(key + ", " + value)
		


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
     	 

def get_msg_code(msg_from_server):
	return msg_from_server[0:3]
		 
	 
def handle_recevied_name(msg):
	return ((msg.split("|"))[1])
	
def handle_forwarding_information_request(msg):
	msg_to_client = ""
	
	other_side_client_name = (msg.split("|"))[1]
	for key in users_dictionary.keys():
		if(other_side_client_name == key):
			msg_to_client = "201|" + users_dictionary[key][0] + "|" + users_dictionary[key][1]
		
		else:
			msg_to_client = "201|0" # false
		
	return msg_to_client
	
		
		
main()
