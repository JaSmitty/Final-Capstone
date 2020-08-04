import axios from 'axios'

export default {
    // remove hard-coded value
    getAllOtherUsers() {
        return axios.get('/api/users/1')
    }
  }
  