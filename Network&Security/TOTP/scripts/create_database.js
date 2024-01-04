var mysql = require('mysql');
var dbconfig = require('../config/database');

var connection = mysql.createConnection(dbconfig.connection);

connection.query('CREATE DATABASE ' + dbconfig.database);

connection.query('\
CREATE TABLE `' + dbconfig.database + '`.`' + dbconfig.users_table + '` ( \
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT, \
    `secret` VARCHAR(120) NOT NULL, \
    `username` VARCHAR(30) NOT NULL, \
    `password` VARCHAR(1024) NOT NULL, \
    `salt` VARCHAR(1024) NOT NULL, \
        PRIMARY KEY (`id`), \
    UNIQUE INDEX `id_UNIQUE` (`id` ASC) \
)');

console.log('Success: Database Created!')

connection.end();
