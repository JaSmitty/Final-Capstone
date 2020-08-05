import axios from 'axios'

export default {
    // TODO remove hard-coded value
    getAllOtherUsers(userId) {
        return axios.get(`/api/users/${userId}`)
    },
    
  }
  