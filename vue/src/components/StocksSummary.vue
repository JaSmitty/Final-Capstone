<template>
  <div>
      <ul>
          <li v-for="stock in stocks" :key="stock.id">
              <p>{{stock.ticker}}</p>
              <p>{{stock.c}}</p>
              <p :class="{'green': stock.c - stock.o >= 0, 'red': stock.c - stock.o < 0}">{{stock.c - stock.o}}</p>
          </li>
      </ul>
  </div>
</template>

<script>
// import stocksService from '@/services/StockService'
export default {
    data() {
        return {
            // stocks: []
            stocks: [
                {id: 1, ticker: 'AAPL', o: 441.62, h: 454.72, l: 439.50, c: 454.296, pc: 440.25, performance: 0.13 + '%'}
            ]
        }
    },
    methods: {
        
    },
    // created() {
    //     stocksService.getStocks().then(response => {
    //         if (response.status === 200) {
    //             this.stocks = response.data
    //         }
    //     })
    // }
}
</script>

<style>

.green {
    color: green;
}
.red {
    color: red;
}
</style>