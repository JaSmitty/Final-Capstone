import axios from 'axios'

export default {
    getStocks() {
        return axios.get('api/stocks')
    },
    getInvestments() {
        return axios.get('api/investments')
    },
    submitSell(investmentToSell) {
        return axios.post('/', investmentToSell)
    }
}