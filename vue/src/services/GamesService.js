import axios from 'axios'

export default {
    getActiveGames() {
        return axios.get('/api/games')
    },
    getGameById(gameId) {
        return axios.get(`/api/games/${gameId}`)
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
    },
    getPendingGames() {
        return axios.get('/api/games/pending')
    },
    acceptInvitation(userGame) {
        return axios.put(`/api/games/${userGame.gameId}/accept`, userGame)
    },
    declineInvitation(userGame) {
        return axios.put(`/api/games/${userGame.gameId}/decline`, userGame)
    },
    getInvestments(gameId) {
        return axios.get(`api/games/${gameId}/investments`)
    },
  }
  