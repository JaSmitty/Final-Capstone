import axios from 'axios'

export default {
    getStocks() {
        return axios.get('api/stocks')
    }
}