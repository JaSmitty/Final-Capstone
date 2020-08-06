import axios from 'axios'

export default {
    // TODO remove hard-coded value
    getAllGames() {
        return axios.get('/api/games')
    },
    createGame(newGame){
        return axios.post('/api/games', newGame)
    },
    inviteUsers(usersGames) {
        return axios.post('/api/games/invite', usersGames)
    },
    getUsersToInvite(gameId) {
        return axios.get(`/api/games/${gameId}/invite`)
    },
    getPlayersInGame(gameId) {
        return axios.get(`/api/games/${gameId}/players`)
    }
  }
  