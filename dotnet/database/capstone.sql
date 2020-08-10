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
	organizer_id int not null,
	name varchar(100) not null,
	-- should we have a begin date?
	start_date Date not null,
	end_date Date not null,
	Constraint pk_game_id Primary Key (id),
	Constraint fk_game_organizer_users_id Foreign Key (organizer_id) References users(id)
);

Create table users_game (
	users_id int not null,
	game_id int not null,
	status varchar(10) default 'pending',
	balance money not null,
	Constraint pk_users_id_game_id Primary Key (users_id, game_id),
	Constraint fk_users_game_users_id_users_id Foreign Key (users_id) References users(id),
	Constraint fk_users_game_game_id_game_id Foreign Key (game_id) References game(id)
);

Create table company (
	stock_id int identity not null,
	ticker varchar(50) not null,
	company_name varchar(50),
	open_price money not null,
	high_price money not null,
	low_price money not null,
	current_price money not null,
	previous_close_price money not null,
	time_updated int --not null,
	Constraint pk_stock_id Primary Key (stock_id)
);

--Create table investment (
--	id int identity not null,
--	users_id int not null,
--	company_ticker varchar(50) not null,
--	game_id int not null,
--	shares float not null,
--	amount money not null,
--	Constraint pk_id Primary Key (id),
--	Constraint fk_investment_id_users_id Foreign Key (users_id) References users(id),
--	--Constraint fk_investment_company_ticker_company_ticker Foreign Key (company_ticker) References company(ticker),
--	Constraint fk_investment_id_game_id Foreign Key (game_id) References game(id)
--);

Create table buy_table (
	id int identity not null,
	users_id int not null,
	stock_at_buy_id int not null,
	game_id int not null,
	initial_shares_purchased float not null,
	shares_currently_owned float not null,
	amount_per_share money not null,
	time_purchased bigint not null,
	Constraint pk_buy_id Primary Key (id),
	Constraint fk_investment_id_users_id Foreign Key (users_id) References users(id),
	Constraint fk_investment_id_game_id Foreign Key (game_id) References game(id),
	Constraint fk_stock_buy_stock_id Foreign Key (stock_at_buy_id) References company(stock_id)
);

Create table sell_table (
	id int identity not null,
	stock_at_sell_id int not null,
	buy_reference_id int not null,
	shares_sold float not null,
	amount_per_share money not null,
	profit money not null,
	time_sold bigint not null,
	Constraint pk_sell_id Primary Key (id),
	Constraint fk_stock_sell_stock_id Foreign Key (stock_at_sell_id) References company(stock_id),
	Constraint fk_buy_reference_buy_table_id Foreign Key (buy_reference_id) References buy_table(id)
);

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO game (organizer_id, name, start_date, end_date) VALUES (1, 'game1', '2020-08-16', '2020-08-20');
INSERT INTO game (organizer_id, name, start_date, end_date) VALUES (2, 'game2', '2020-08-18', '2020-08-22');

INSERT INTO company(ticker, open_price, high_price, low_price, current_price, previous_close_price) VALUES ('AAPL', 432.80, 446.55, 431.57, 437.70, 425.04);
INSERT INTO company(ticker, open_price, high_price, low_price, current_price, previous_close_price) VALUES ('PGR', 90.70, 91.23, 90.15, 90.99, 90.34);

INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (1, 1, 1, 1, 1, 450, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (1, 2, 1, 0.50, 0.50, 100, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (2, 1, 1, 2, 2, 450, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (2, 2, 2, 6, 6, 100, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (1, 1, 2, 5.5, 5.5, 450, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (1, 2, 2, 3.3, 3.3, 100, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (2, 1, 2, 4, 4, 450, 637326609180000000);
INSERT INTO buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) VALUES (2, 2, 2, 3, 3, 100, 637326609180000000);

INSERT INTO users_game(users_id, game_id, status, balance) VALUES (1, 1, 'approved', 100000);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (1, 2, 'pending', 100000);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (2, 1, 'approved', 100000);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (2, 2, 'pending', 100000);

GO
