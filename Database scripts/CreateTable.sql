CREATE TABLE accounts (
    id text PRIMARY KEY,
    email varchar(30),
    password varchar(100),
    refreshtoken varchar(100),
    refreshtokenvalidto timestamp,
    passwordresettoken varchar(100),
    passwordresettokenvalidto timestamp,
    isblocked boolean
);

CREATE TABLE employees (
    id text PRIMARY KEY,
    name varchar(20),
    surname varchar(20),
    daysoff integer,
    role int,
    accountId text REFERENCES accounts
);

CREATE TABLE holidayrequests (
    id text PRIMARY KEY,
    start date,
    "end" date,
    status int,
	senderId text REFERENCES employees
);

CREATE TABLE holidayrequestapprovers (
    id text PRIMARY KEY,
	approverId text REFERENCES employees,
	requestId text REFERENCES holidayrequests,
	status int
);

CREATE TABLE clients (
    id text PRIMARY KEY,
	name varchar(20),
	country varchar(30)
);

CREATE TABLE projects (
    id text PRIMARY KEY,
	title varchar(30),
	description varchar(200),
	teamleadid text REFERENCES employees,
	clientid text REFERENCES clients
);

CREATE TABLE employeeprojects (
    id text PRIMARY KEY,
	employeeid text REFERENCES employees,
	projectid text REFERENCES projects
);