import socket
import sqlite3

HOST = '127.0.0.1'
PORT = 8820
USERS_DB_NAME = "Users.db"

users_dictionary = {}

connectToDB = sqlite3.connect(USERS_DB_NAME)
cursor = connectToDB.cursor()

def main():

	listening_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # Bind the socket to the port
	server_address = (HOST, PORT)
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
					print("-----------------------")
					break
		except:
			print("Error")
	
	connectToDB.close()
	connection.close()  
	listening_socket.close()
     	 

def get_msg_code(msg_from_client):
	return msg_from_client[0:3]
		 
		 
def Insert_User_To_DB(name, IP, PORT):
	insert_user_to_db_sql_command = "INSERT INTO Users(name, IP, PORT)" +  " VALUES (" + "'" + name + "'" + ", '" + IP + "'" + " ,'" + PORT + "'" +");"
	cursor.execute(insert_user_to_db_sql_command)
	
	connectToDB.commit()
	
	None
	
def get_recepient_client_information(name):
	get_recepient_information_sql_command = "SELECT * from users WHERE name = " + "'" + name + "';"
	cursor.execute(get_recepient_information_sql_command)
	result = cursor.fetchall() #[()()()()] list of tuples
	
	if(result):
		return result[0][2], result[0][3]
	
	else:
		return 0	

def is_User_In_The_DB(name):
	sql_command = "SELECT * from users WHERE name = " + "'" + name + "';"
	cursor.execute(sql_command)
	result = cursor.fetchall()
	connectToDB.commit()
	
	if result:
		return True
		
	else:
		return False

def update_IP_PORT_for_user(name, IP, PORT):
	sql_command = "UPDATE users SET IP = " + "'" + IP + "'" + ", PORT = " + "'" + PORT + "'" + " WHERE name = " + "'" + name + "';" 
	cursor.execute(sql_command)
	connectToDB.commit()

	None
	
def handle_recevied_name(msg, connection, client_address):
	client_name = (msg.decode('utf-8').split("|"))[1]
	check = is_User_In_The_DB(client_name)

	if check:
		update_IP_PORT_for_user(client_name, client_address[0], str(client_address[1]))
		print("the IP and PORT is updated for client name: ", client_name)
		print("-----------------------")

	else:
		Insert_User_To_DB(client_name, client_address[0], str(client_address[1]))
		print("Inserted To the DATABASE")
		print("-----------------------")
	
	msg_to_client = "201|HEY " + client_name + ", WELCOME TO THE SSRP SERVER !!!" + "|" + str(client_address[1])
	print("send: ", msg_to_client)
	print("-----------------------")
	connection.sendall(msg_to_client.encode()) 
	
	None
	
def handle_forwarding_information_request(msg_from_client, connection, client_address):
	
	msg_to_client = "203|0" #false
	#ip, port
	other_side_client_name = ((msg_from_client.decode('utf-8')).split("|"))[1]
	
	data = get_recepient_client_information(other_side_client_name)
	
	if(data != 0):
		msg_to_client = "203|" + str(data[0]) + "|" + str(data[1])
		
	
	print("sent: ", msg_to_client)
	print("-----------------------")

	connection.sendall(msg_to_client.encode())
	
	None


def handle_msg_from_client(msg_code, msg_from_client, connection, client_address):
	
	if(msg_code == b'100'):
		handle_recevied_name(msg_from_client, connection, client_address)
	
	elif(msg_code == b'102'):
		handle_forwarding_information_request(msg_from_client, connection, client_address)
	
	None
	
	
main()