<template>
  <div id="stock-market">
    <h1>Available Stocks</h1>
    
    <ul>
      <li class="stock-card" v-for="stock in sortedStocks" :key="stock.id">
        <router-link class="card" :to="{name: 'BuyStock', params: {ticker: stock.ticker}}">
          <div class="card-text" @click="setStockToBuy(stock)">
            <h2 class="stock-name">{{stock.companyName}}&nbsp;({{stock.ticker}})</h2>
            <h2>${{stock.c}}</h2>
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
import stocksService from '@/services/StocksService'
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
  },
  created() {
    stocksService.getStocks().then(response => {
            if (response.status === 200) {
                this.$store.commit("SET_CURRENT_STOCKS", response.data)
            }
        })
  }
};
</script>

<style>

li {
  list-style: none;
}

#stock-market >h1 {
 background: rgba(0, 26, 51, 0.7);
  margin-top: 0;
  padding-bottom: 5px;
  padding-top: 5px;
}

#stock-market ul{
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow:  scroll;
  height: 400px;
  overflow-x: hidden;
  padding: 0px;
}

.card-text{
  color: #003366;
  
}
.card-text p {
  font-size: 23px;
}

.card-text h2 {
  font-size: 25px;
}


.card{
  text-decoration: none;
}

.stock-name{
  text-decoration: underline;
  font-size: 30px;
}
.stock-card{
  
  border-left: solid #005cb8 7px;
  border-right: solid #005cb8 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #5cadff,#e7f3ff);
  border-radius: 8px;
  width: 40%;
  
}
</style>