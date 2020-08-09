<template>
  <div>
    <h1>Stock Market</h1>
    <ul>
      <li v-for="stock in sortedStocks" :key="stock.id">
        <router-link :to="{name: 'BuyStock', params: {ticker: stock.ticker}}">
          <div @click="setStockToBuy(stock)">
            <h2>{{stock.ticker}}</h2>
            <h2>{{stock.c}}</h2>
            <p :class="{'green': stock.c >= stock.o, 'red': stock.c < stock.o}">
              {{stock.c > stock.o ? "+" : ""}}
              {{(stock.c - stock.o).toFixed(3)}}
              ({{stock.c > stock.o ? "+" : ""}}
              {{((stock.c / stock.o) - 1).toFixed(3)}}%)
            </p>
          </div>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      //comment this line
      stocks: this.$store.state.currentStockMarket,
      // stocks: [
      //     {id: 1, ticker: 'AAPL', o: 441.62, h: 454.72, l: 439.50, c: 454.296, pc: 440.25, performance: 0.13 + '%'}
      // ]
    };
  },
  methods: {
    setStockToBuy(stock) {
      this.$store.commit("SET_STOCK", stock);
    },
  },
  computed: {
    sortedStocks() {
      let sortedStocks = this.stocks;
      return sortedStocks.sort(function (a, b) {
        return b.c / b.o - a.c / a.o;
      });
    },
  }
};
</script>

<style>

li {
  list-style: none;
}
</style>