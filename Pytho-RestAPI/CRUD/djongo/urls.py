from django.urls import path
from .views import getAllUsers, createUser

urlpatterns = [
    path('users/', getAllUsers, name='get_all_users'),
    path('users/create/', createUser, name='create_user'),
]
