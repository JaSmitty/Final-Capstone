BEGIN TRANSACTION
DECLARE  @shares_owned float
DECLARE  @amountPerShareBuy money
DECLARE  @amountPerShareSold money
DECLARE  @sellId int
Select @shares_owned = shares_currently_owned, @amountPerShareBuy = amount_per_share
From buy_table
Where id = @buyId

UPDATE buy_table
SET shares_currently_owned = (@shares_owned - @sharesPurchased)
Where id = @buyId

Select @amountPerShareSold = company.current_price
From company
where stock_id = @stockAtSellId

INSERT INTO sell_table (stock_at_sell_id, buy_reference_id, shares_sold, amount_per_share, profit, time_sold)
VALUES (@stockAtSellId, @buyId, @sharesSold, @amountPerShareSold, ((@amountPerShareSold * @sharesSold) - (@amountPerShareBuy * @sharesSold)), @timeSold)
Select @sellId = @@IDENTITY
Select *
from sell_table
where id = @sellId
Commit Transaction


-- buy transaction

Begin Transaction

DECLARE @currentbalance money

select @currentbalance = balance
from users_game
where users_id = @userId and game_id = @gameId

UPDATE users_game
SET balance = (balance - ())
Where id = @buyId


INSERT into buy_table(users_id, stock_at_buy_id, game_id, initial_shares_purchased, shares_currently_owned, amount_per_share, time_purchased)
VALUES (@userId, @stockBuyId, @gameId, @sharesPurchased, @currentlyOwned, @amountPerShare, @timePurchased); SELECT @@IDENTITY
Commit Transaction

