from flask import Flask, jsonify, request
import socket

HOST = '127.0.0.1'
PORT = 65432


s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((HOST, PORT))
s.listen()
conn, addr = s.accept()



app = Flask(__name__)


@app.route('/ook')
def ook():
	return jsonify('ook')

@app.route('/push', methods=['POST'])
def push():
	data = request.get_json()
	player = data['player']
	action = data['action']

	conn.send(bytes('%s~%s' % (action, player), 'ascii')) 

	return jsonify('ok')

app.run('localhost', 8000)

s.close()