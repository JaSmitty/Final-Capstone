import axios from 'axios'

export default {
    // remove hard-coded value
    getGamesByUserId() {
        return axios.get('/api/games/1')
    }
  }
  