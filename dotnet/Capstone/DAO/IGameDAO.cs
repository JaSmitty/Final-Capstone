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
        Game CreateGame(int creatorId, string gameName, DateTime endDate);
        UserGame AddUserToGame(int addedUserId, int gameId);


    }
}
