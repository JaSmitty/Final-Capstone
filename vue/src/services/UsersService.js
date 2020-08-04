import axios from 'axios'

export default {
    // TODO remove hard-coded value
    getAllOtherUsers() {
        return axios.get('/api/users/1')
    },
    inviteUsers(selectedUserIds) {
        return axios.post('/api/users/1/invite', selectedUserIds)
    }
  }
  