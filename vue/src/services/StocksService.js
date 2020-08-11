import axios from 'axios'

export default {
    getStocks() {
        return axios.get('api/stocks')
    },
    getInvestment(stockId, gameId) {
        return axios.get(`/api/stocks/${stockId}/${gameId}`)
    },
    // TODO
    submitBuy(stock) {
        return axios.post('api/stocks/buy', stock)
    },
    submitSell(investmentToSell) {
        return axios.post('api/stocks/sell', investmentToSell)
    }
}