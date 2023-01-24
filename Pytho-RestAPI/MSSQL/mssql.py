from flask import Flask, request, jsonify
import pyodbc

app = Flask(__name__)

connection_string = 'DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=exp;UID=sa;PWD=123'

cnxn = pyodbc.connect(connection_string)
cursor = cnxn.cursor()

if cursor:
    print("Connection established")
else:
    print("Error connecting to the database")


@app.route('/users', methods=['GET', 'POST'])
def users():
    if request.method == 'GET':
        cursor.execute('SELECT * FROM users')
        rows = cursor.fetchall()
        return jsonify(rows)
    elif request.method == 'POST':
        data = request.get_json()
        username = data['username']
        email = data['email']
        password = data['password']
        cursor.execute(
            "INSERT INTO users (username, email, password) VALUES (?,?,?)", (username, email, password))
        cnxn.commit()
        return jsonify({'message': 'User added successfully'})

@app.route('/users/<int:id>', methods=['GET', 'PUT', 'DELETE'])
def entrys(id):
    if request.method == 'GET':
        cursor.execute("SELECT * FROM users WHERE id=?", (id,))
        row = cursor.fetchone()
        if row:
            return jsonify(row)
        else:
            return jsonify({'message': 'User not found'}), 404
    elif request.method == 'PUT':
        data = request.get_json()
        username = data['username']
        email = data['email']
        password = data['password']
        cursor.execute("UPDATE users SET username=?, email=?, password=? WHERE id=?", (username, email, password, id))
        cnxn.commit()
        return jsonify({'message': 'User updated successfully'})
    elif request.method == 'DELETE':
        cursor.execute("DELETE FROM users WHERE id=?", (id,))
        cnxn.commit()
        return jsonify({'message': 'User deleted successfully'})

if __name__ == '__main__':
    app.run(port=4500)
