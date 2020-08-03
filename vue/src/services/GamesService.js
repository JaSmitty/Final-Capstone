import axios from 'axios'

export default {
    // remove hard-coded value
    getGamesByUserId() {
        return axios.get('/api/games/1')
    },
    createGame(newGame){
        return axios.post('/api/games/1', newGame)
    }
  }
  