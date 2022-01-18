create database company;
go
use company;
go
create table users (
	id int primary key not null,
	nom varchar(255) not null,
	prenom varchar(255) not null
);

insert into users values (20, 'Hamza', 'Elansari');
insert into users values (21, 'John', 'kire');
insert into users values (22, 'Lite', 'Yagami');
insert into users values (23, 'Neo', 'Doe');
