using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IGameDAO
    {
        Game GetGameByUserId(int userId);
        int CreateGame(Game newGame);
        UserGame AddUserToGame(int addedUserId, int gameId);


    }
}
