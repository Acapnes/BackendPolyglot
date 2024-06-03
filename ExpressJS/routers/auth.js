const express = require("express");
const router = express.Router();
const {getAccessToRoute} = require("../middlewares/authorization/auth")

const { getAllUsers,userLogin,userRegister,testError,getUser } = require("../controllers/auth");


router.get("/", getAllUsers);
router.post("/login", userLogin);
router.get("/profile", getAccessToRoute,getUser);
router.post("/register", userRegister);
router.get("/testError", testError);

module.exports = router;
