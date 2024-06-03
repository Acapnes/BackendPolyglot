const User = require("../models/User");
const CustomError = require("../helpers/error/CustomError");
const asyncErrorWrapper = require("express-async-handler");
const { sendJwtToClient } = require("../helpers/authorization/tokenHelpers");
const { validateUserInput,comparePassword } = require("../helpers/authorization/input/inputHelpers")

const getAllUsers = (req, res, next) => {
  User.find({}, function (err, users) {
    if (err) return next(err);
    res.json({
      success: true,
      data: users,
    });
  });
};

const userLogin = async (req, res, next) => {
  try {
    const { email, password } = req.body;

    // Check if email and password are provided
    if (!email || !password) {
      throw new CustomError("Please provide email and password", 400);
    }

    // Find user by email
    const user = await User.findOne({ email }).select("+password");

    // Check if user exists and password is correct
    if (!user || !comparePassword(password, user.password)) {
      throw new CustomError("Invalid email or password", 401);
    }

    // Send JWT token to client
    sendJwtToClient(user, res);
  } catch (error) {
    next(error);
  }
};


const userRegister = asyncErrorWrapper(async (req, res, next) => {
  const { name, email, password, role } = req.body;

  const user = await User.create({
    name,
    email,
    password,
    role,
  });

  sendJwtToClient(user, res);
});

const getUser = ((req, res, next) => {

  res.json({
    success: true,
    data: {
      id: req.user.id,
      name: req.user.name,
    }
  })
})

const testError = (req, res, next) => {
  return next(new SyntaxError("Custom error Message"));
};

module.exports = {
  getAllUsers,
  userLogin,
  userRegister,
  testError,
  getUser,
};
