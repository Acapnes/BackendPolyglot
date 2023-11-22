from djongo import models


class Users(models.Model):
    name = models.CharField(max_length=255)
    role = models.CharField(max_length=255)
    password = models.CharField(max_length=255)
