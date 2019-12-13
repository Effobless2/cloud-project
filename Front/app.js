const express = require("express");
path = require('path');
app=express();

const PORT = 8080;

app.use(express.static(__dirname));

app.get('/', function (req, res) {
    res.sendFile(path.resolve(__dirname, 'index.html'));
})

app.listen(PORT);