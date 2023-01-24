from flask import Flask, jsonify, request
from flask_restful import Resource, Api

app = Flask(__name__)
api = Api(app)

users = [
  { 'id': 1, 'name': 'John Doe' },
  { 'id': 2, 'name': 'Jane Doe' }
]

class UserList(Resource):
  def get(self):
    return jsonify(users)

class User(Resource):
  def get(self, user_id):
    user = next(filter(lambda x: x['id'] == user_id, users), None)
    return {'user': user}, 200 if user else 404

  def post(self, user_id):
    data = request.get_json()
    user = {'id': user_id, 'name': data['name']}
    users.append(user)
    return user, 201

  def put(self, user_id):
    data = request.get_json()
    user = next(filter(lambda x: x['id'] == user_id, users), None)
    if user is None:
      user = {'id': user_id, 'name': data['name']}
      users.append(user)
    else:
      user.update(data)
    return user

api.add_resource(UserList, '/users')
api.add_resource(User, '/users/<int:user_id>')

if __name__ == '__main__':
  app.run()
