from fastapi import FastAPI, HTTPException, Query, Path
from pydantic import BaseModel
from middleware import my_middleware
import uvicorn
import pyodbc

app = FastAPI()

app.add_middleware(my_middleware)

connection_string = 'DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=exp;UID=sa;PWD=123'

try:
    cnxn = pyodbc.connect(connection_string)
    cursor = cnxn.cursor()
    print("Connection established")
except Exception as e:
    print("Error connecting to the database: ", e)


class UserIn(BaseModel):
    name: str
    email: str
    password: str


class UserOut(BaseModel):
    id: int
    name: str
    email: str
    password: str


@app.get("/users")
async def getallusers():
    try:
        cursor.execute('SELECT * FROM users')
        rows = cursor.fetchall()
        return [{"id": row[0], "name": row[1], "email": row[2], "password": row[3]} for row in rows]
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/users/{user_id}")
async def read_user(user_id: int):
    cursor.execute("SELECT * FROM users WHERE id=?", (user_id,))
    row = cursor.fetchone()
    if row:
        return UserOut(id=row.id, name=row.name, email=row.email, password=row.password)
    else:
        raise HTTPException(status_code=404, detail="User not found")


@app.post("/users")
async def create_user(user: UserIn):
    cursor.execute("INSERT INTO users (name, email, password) VALUES (?,?,?)",
                   (user.name, user.email, user.password))
    cnxn.commit()
    return {"message": "User created successfully"}


@app.put("/users/{user_id}")
async def update_user(user_id: int, user: UserIn):
    cursor.execute("SELECT * FROM users WHERE id=?", (user_id,))
    row = cursor.fetchone()
    if row:
        cursor.execute("UPDATE users SET name=?, email=?, password=? WHERE id=?",
                       (user.name, user.email, user.password, user_id))
        cnxn.commit()
        return {"message": "User updated successfully"}
    else:
        raise HTTPException(status_code=404, detail="User not found")


@app.delete("/users/{user_id}")
async def delete_user(user_id: int):
    cursor.execute("DELETE FROM users WHERE id=?", (user_id,))
    cnxn.commit()
    return {"message": "User deleted successfully"}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="127.0.0.1", port=4500)
