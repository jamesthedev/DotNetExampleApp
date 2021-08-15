--mySQL root password: ySQLPassw0rd#@!

create database mydb;

use mydb;

create table student (
    id INT NOT NULL AUTO_INCREMENT,
    fName VARCHAR(100) NOT NULL,
    lName VARCHAR(100) NOT NULL,
    currGrade DECIMAL NOT NULL,
    age INT NOT NULL,
    PRIMARY KEY (id)
);

--optional insert
insert into student (fName, lName, currGrade, age) 
values ('James', 'Bell', 97.3, 25);