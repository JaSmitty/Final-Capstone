USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
Create table users (
	id int identity (1,1) not null,
	username varchar(50) not null,
	password_hash varchar(200) not null,
	salt varchar(200) not null,
	user_role varchar(50) not null
	Constraint PK_user Primary Key (id)
);

Create table game (
	id int identity not null,
	organizer int not null,
	name varchar(100) not null,
	-- should we have a begin date?
	end_date DateTime not null,
	Constraint pk_game_id Primary Key (id),
	Constraint fk_game_organizer_users_id Foreign Key (organizer) References users(id)
);

Create table users_game (
	users_id int not null,
	game_id int not null,
	balance money not null,
	Constraint pk_users_id_game_id Primary Key (users_id, game_id),
	Constraint fk_users_game_users_id_users_id Foreign Key (users_id) References users(id),
	Constraint fk_users_game_game_id_game_id Foreign Key (game_id) References game(id)
);

Create table company (
	ticker varchar(50) not null,
	open_price money not null,
	high_price money not null,
	low_price money not null,
	current_price money not null,
	previous_close_price money not null
	Constraint pk_ticker Primary Key (ticker)
);

Create table investment (
	id int identity not null,
	users_id int not null,
	company_ticker varchar(50) not null,
	game_id int not null,
	shares float not null,
	amount money not null,
	Constraint pk_id Primary Key (id),
	Constraint fk_investment_id_users_id Foreign Key (users_id) References users(id),
	Constraint fk_investment_company_ticker_company_ticker Foreign Key (company_ticker) References company(ticker),
	Constraint fk_investment_id_game_id Foreign Key (game_id) References game(id)
);

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO game (organizer, name, end_date) VALUES (1, 'game1', 2020-08-10);
INSERT INTO game (organizer, name, end_date) VALUES (2, 'game2', 2020-08-08);

INSERT INTO company(ticker, open_price, high_price, low_price, current_price, previous_close_price) VALUES ('AAPL', 432.80, 446.55, 431.57, 437.70, 425.04);
INSERT INTO company(ticker, open_price, high_price, low_price, current_price, previous_close_price) VALUES ('PGR', 90.70, 91.23, 90.15, 90.99, 90.34);

INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (1, 'AAPL', 1, 1, 90.70);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (1, 'PGR', 1, 0.50, 45.60);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (2, 'AAPL', 1, 2, 863.14);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (2, 'PGR', 2, 6, 545.94);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (1, 'AAPL', 2, 5.5, 2373.64);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (1, 'PGR', 2, 3.3, 1424.18);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (2, 'AAPL', 2, 4, 1726.28);
INSERT INTO investment(users_id, company_ticker, game_id, shares, amount) VALUES (2, 'PGR', 2, 3, 272.97);

INSERT INTO users_game(users_id, game_id, balance) VALUES (1, 1, 100000);
INSERT INTO users_game(users_id, game_id, balance) VALUES (1, 2, 100000);
INSERT INTO users_game(users_id, game_id, balance) VALUES (2, 1, 100000);
INSERT INTO users_game(users_id, game_id, balance) VALUES (2, 2, 100000);

GO
