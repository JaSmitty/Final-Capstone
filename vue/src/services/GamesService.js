import axios from 'axios'

export default {
    // TODO remove hard-coded value
    getGamesByUserId(userId) {
        return axios.get(`/api/games/${userId}`)
    },
    createGame(newGame){
        return axios.post(`/api/games/${newGame.organizerId}`, newGame)
    },
    inviteUsers(usersGames, userId) {
        return axios.post(`/api/games/${userId}/invite`, usersGames)
    }
  }
  