BEGIN TRANSACTION
DECLARE @stockId int


INSERT into buy_table (users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased) 
VALUES (1, 2, 1, 10, 10, 50, 10000);
Select @stockId = @@IDENTITY

Select *
from buy_table

DECLARE  @shares_owned float
DECLARE  @amount money

Select @shares_owned = shares_currently_owned, @amount = amount_per_share
From buy_table
Where id = @stockId

UPDATE buy_table
SET shares_currently_owned = (@shares_owned - 5)
Where id = @stockId

Select * from buy_table

INSERT INTO sell_table (stock_at_sell_id, buy_reference_id, shares_sold, amount_per_share, profit, time_sold)
VALUES (2, @stockId, 5, 60, ((40 * 5) - (@amount * 5)), 1000)

Select *
From sell_table
Rollback Transaction

