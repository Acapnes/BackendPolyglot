from django.shortcuts import render
from .models import Users

def getAllUsers(request):
    users = Users.objects.all()
    return render(request, 'users.html', {'users': users})

def createUser(request):
    if request.method == 'POST':
        name = request.POST['name']
        role = request.POST['role']
        password = request.POST['password']

        user = Users(name=name, role=role, password=password)
        user.save()
        return redirect('get_all_users')
    return render(request, 'create_user.html')
