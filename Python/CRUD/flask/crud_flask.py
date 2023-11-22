from crud_flask import Flask, request, jsonify
import pyodbc

app = Flask(__name__)

connection_string = 'DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=exp;UID=sa;PWD=123'

try:
    cnxn = pyodbc.connect(connection_string)
    cursor = cnxn.cursor()
    print("Connection established")
except Exception as e:
    print("Error connecting to the database: ", e)


@app.route('/users', methods=['GET', 'POST'])
def users():
    if request.method == 'GET':
        cursor.execute('SELECT * FROM users')
        rows = cursor.fetchall()
        users_list = []
        for row in rows:
            user = {'id': row.id, 'name': row.name,
                    'email': row.email, 'password': row.password}
            users_list.append(user)
        return jsonify(users_list)
    elif request.method == 'POST':
        data = request.get_json()
        name = data['name']
        email = data['email']
        password = data['password']
        cursor.execute(
            "INSERT INTO users (name, email, password) VALUES (?,?,?)", (name, email, password))
        cnxn.commit()
        return jsonify({'message': 'User added successfully'})


@app.route('/users/<int:id>', methods=['GET', 'PUT', 'DELETE'])
def user(id):
    if request.method == 'GET':
        cursor.execute("SELECT * FROM users WHERE id=?", (id,))
        row = cursor.fetchone()
        if row:
            user = {'id': row.id, 'name': row.name,
                    'email': row.email, 'password': row.password}
            return jsonify(user)
        else:
            return jsonify({'message': 'User not found'}), 404
    elif request.method == 'PUT':
        data = request.get_json()
        name = data['name']
        email = data['email']
        password = data['password']
        cursor.execute("UPDATE users SET name=?, email=?, password=? WHERE id=?",
                       (name, email, password, id))
        cnxn.commit()
        return jsonify({'message': 'User updated successfully'})
    elif request.method == 'DELETE':
        cursor.execute("DELETE FROM users WHERE id=?", (id,))

if __name__ == '__main__':
    app.run(port=4500)
