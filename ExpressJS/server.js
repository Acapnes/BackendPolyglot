const express = require("express");
const { accessControl } = require("./middleware");
const routers = require("./routers");
const dotenv = require("dotenv");
const connectDatabase = require("./helpers/database/connectDatabase");
const customErrorHandler = require("./middlewares/errors/customErrorHandler")

const app = express();
app.use(express.json());

dotenv.config({
  path: "./config/env/config.env",
});

// Mongodb Connection
//mongodb://mongo:27017 -- for docker
connectDatabase();

/// Routers
app.use("", routers);

/// Error Handling

app.use(customErrorHandler);

// app.use(accessControl); /// Altındaki bütün requestler için çalışıyor
app.listen(process.env.PORT, () => {
  console.log("Server Started!");
});
