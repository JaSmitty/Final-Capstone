<template>
  <div>
      <h1>Stocks Summary</h1>
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
export default {
    data() {
        return {
            //comment this line
            stocks: this.$store.state.currentStockMarket
            // stocks: [
            //     {id: 1, ticker: 'AAPL', o: 441.62, h: 454.72, l: 439.50, c: 454.296, pc: 440.25, performance: 0.13 + '%'}
            // ]
        }
    }
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