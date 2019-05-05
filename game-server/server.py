from flask import Flask, jsonify, request
from flask_cors import CORS
import socket

HOST = '127.0.0.1'
UNITY_PORT = 65432
OUTSIDE_PORT = 8000
game_instances = {}


s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((HOST, UNITY_PORT))
s.listen()
conn, addr = s.accept()



app = Flask(__name__)
cors = CORS(app, resources={r"*": {"origins": "*"}})
# @app.route('/create_game_instance'):
# def



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


app.run(HOST, OUTSIDE_PORT, ssl_context='adhoc')
s.close()