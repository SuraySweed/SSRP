import socket
import sqlite3
import subprocess

PORT = 8820
USERS_DB_NAME = "Users.db"
ServerIP = "10.40.178.162"

users_dictionary = {}

connectToDB = sqlite3.connect(USERS_DB_NAME)
cursor = connectToDB.cursor()

sql_command = "DELETE FROM users;"
cursor.execute(sql_command)

def main():

	listening_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # Bind the socket to the port
	server_address = (ServerIP, PORT)
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

	Clean_DataBase_Users()
	connectToDB.close()
	connection.close()  
	listening_socket.close()
     	 

#def get_local_IP_from_the_cmd():
#	IPv4 = ''
#	ipconfig_object = subprocess.Popen(["ipconfig"], stdout=subprocess.PIPE)
#	ipconfig_string = ipconfig_object.stdout.read().decode('utf-8')
#	ipconfig_list = ipconfig_string.split("\n")
#	IPv4 = ((ipconfig_list[42].split(":")[1]).split('\r')[0]).strip() # Remove all whitespace 
	
#	return IPv4

	
def get_msg_code(msg_from_client):
	return msg_from_client[0:3]
		 
		 
def Insert_User_To_DB(name, IP, PORT, public_key):
	insert_user_to_db_sql_command = "INSERT INTO Users(name, IP, PORT, publicKey)" +  " VALUES (""'" + name + "'" + ", '" + IP + "'" + " ,'" + PORT + "', " + "'" + public_key + "'"");"
	cursor.execute(insert_user_to_db_sql_command)
	
	connectToDB.commit()
	
	None
	
def get_recepient_client_information(name):
	get_recepient_information_sql_command = "SELECT * from users WHERE name = " + "'" + name + "';"
	cursor.execute(get_recepient_information_sql_command)
	result = cursor.fetchall() #[()()()()] list of tuples
	
	if(result):
		return (result[0][2], result[0][3], result[0][4])
	
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

def update_IP_PORT_for_user(name, IP, PORT, public_key):
	sql_command = "UPDATE users SET IP = " + "'" + IP + "'" + ", PORT = " + "'" + PORT + "', '" + public_key + "'" + " WHERE name = " + "'" + name + "';" 
	cursor.execute(sql_command)
	connectToDB.commit()

	None
	
def clients_route_by_bulding_trace(name, recpient_name):
	sql_command = "SELECT * from users where name != " + '"' + name + '" and name != ' + '"' + recpient_name + '"' + ';'
	clients_data = ""

	cursor.execute(sql_command)
	result = cursor.fetchall()
	connectToDB.commit()

	if result:
		for item in result:
			clients_data = clients_data + item[2] + "," + str(item[3]) + "," + str(item[4]) + "|"
	else:
		return 0
	return clients_data


def handle_recevied_name(msg, connection, client_address):
	client_name = (msg.decode('utf-8').split("|"))[1]
	public_key = (msg.decode('utf-8').split("|"))[2]
	check = is_User_In_The_DB(client_name)

	if check:
		update_IP_PORT_for_user(client_name, client_address[0], str(client_address[1]))
		print("the IP and PORT is updated for client name: ", client_name)
		print("-----------------------")

	else:
		Insert_User_To_DB(client_name, client_address[0], str(client_address[1]), public_key)
		print("Inserted To the DATABASE")
		print("-----------------------")
	
	msg_to_client = "201|HEY " + client_name + ", WELCOME TO THE SSRP SERVER !!!" + "|" + str(client_address[1]) + "|" + client_address[0]
	print("send: ", msg_to_client)
	print("-----------------------")
	connection.sendall(msg_to_client.encode()) 
	
	None
	
def handle_forwarding_information_request(msg_from_client, connection, client_address):
	
	msg_to_client = "203|0" #false
	#ip, port
	name = ((msg_from_client.decode('utf-8')).split("|"))[1] # request the trace 
	other_side_client_name = ((msg_from_client.decode('utf-8')).split("|"))[2]
	data = get_recepient_client_information(other_side_client_name)

	route_trace = clients_route_by_bulding_trace(name, other_side_client_name)

	#if(data != 0):
		#if(data[1] != client_address[1]):
			#msg_to_client = "203|" + data[0] + "|" + str(data[1])
			
		#elif(data[1] == client_address[1]):
			#msg_to_client = "203|1"
			#
	if(data or route_trace != 0):
		msg_to_client = "203|" + data[0] + "," + str(data[1]) + "," + data[2] + "|" + str(route_trace);


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