const express = require('express');
const path = require('path');
const port = 3000;
const app = express();

app.use(express.static(path.join(__dirname, '/')));

app.listen(port, function () {
    console.log("webserver listening!");
});