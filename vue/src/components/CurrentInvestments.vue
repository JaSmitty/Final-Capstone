<template>
  <div>
    <h1 class="curr-inv-title">Current Investments</h1>
    <div id="current-investments">
    <table>
      <tr class="headers">
        <th>Company</th>
        <th>Shares Owned</th>
        <th>Purchase Price</th>
        <!-- <th>Profit</th> -->
      </tr>

      <tr v-for="investment in investments" :key="investment.buyId">
        <td>
          <router-link class="stock-link" :to="{name: 'SellStock', params: {stockId: investment.stockId}}">
            <div>{{investment.companyTicker}}</div>
          </router-link>
        </td>
        <td>{{investment.sharesCurrentlyOwned}}</td>
        <td>${{(investment.amountPerShare).toFixed(2)}}</td>
      </tr>
    </table>
    </div>
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
export default {
  components: {},
  data() {
    return {
      currentStockMarket: this.$store.state.currentStockMarket,
      investments: []
    };
  },
  methods: {
    // setInvestmentToSell(investment) {
    //   this.$store.commit("SET_INVESTMENT", investment)
    //   const stock = this.$store.state.currentStockMarket.find(s => s.ticker === investment.companyTicker)
    //   this.$store.commit("SET_STOCK", stock)
    // }
    //   getProfit(investment) {
    //       let currentStock = this.currentStockMarket.find(stock => stock.company_ticker === investment.company_ticker);
    //       return (currentStock.c * investment.current_shares) - investment.price_per_share * investment.current_shares;
    //   }
  },
  computed: {
    //   profitList() {
    //       let profitList = [];
    //       this.investments.forEach(investment => {
    //           profitList.push({ticker: investment.company_ticker, profit: this.getProfit(investment)})
    //       })
    //       return profitList
    //   }
  },
  created() {
    gamesService.getInvestments(this.$store.state.currentGame.gameId).then(response => {
      if (response.status === 200) {
        this.investments = response.data;
      }
    })
  }
};
</script>

<style>
#current-investments {
  display: flex;
  justify-content: center;
}



table {
border: #add6ff solid 3px;
border-collapse: collapse;
}

.headers > th{
  font-size: large;
  border: #add6ff solid 3px;
  padding: 4px;
  background: rgba(173, 214, 255, 0.7);
  color:  #003366;
}

#current-investments  td{
   border-right: #add6ff solid 1px;
   border-bottom: #add6ff solid 1px;
   background: rgba(0, 51, 102, 0.7);
}

.stock-link{
  color: white;
}

.stock-link:hover{
  color: #f68e4f;
}

.curr-inv-title{
  background: rgba(0, 26, 51, 0.7);
  margin-top: 0;
  padding-bottom: 5px;
  padding-top: 5px;
}
</style>