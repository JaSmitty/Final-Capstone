BEGIN TRANSACTION
DECLARE @gameId int
INSERT INTO game (organizer_id, name, start_date, end_date, is_complete) VALUES (5, 'WhosTheWinner', '2020-08-05', '2020-08-11', 1);
Select @gameId = @@IDENTITY
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (3, @gameId, 'approved', 78098.78);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (4, @gameId, 'approved', 112987.89);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (5, @gameId, 'approved', 107231.57);
INSERT INTO game (organizer_id, name, start_date, end_date, is_complete) VALUES (6, 'GetGood', '2020-08-01', '2020-08-07', 1);
Select @gameId = @@IDENTITY
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (3, @gameId, 'approved', 98234.74);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (4, @gameId, 'approved', 122687.89);
INSERT INTO users_game(users_id, game_id, status, balance) VALUES (5, @gameId, 'approved', 111231.67);
SELECT * FROM game
SELECT * FROM users_game
Commit TRANSACTION