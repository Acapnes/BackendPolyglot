const express = require("express");

const authRouter = require("./auth");
const questionRouter = require("./question");

const router = express.Router();

router.use("/users",authRouter);
router.use("/question",questionRouter);

module.exports = router;