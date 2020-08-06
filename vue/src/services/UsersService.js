import axios from 'axios'

export default {
    // TODO remove hard-coded value
    getUsersToInvite(gameId) {
        return axios.get(`/api/users/invite/${gameId}`)
    },
    
  }
  