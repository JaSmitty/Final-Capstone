<template>
  <div>
    <h1>Current investments</h1>
    <table>
      <tr>
        <th>Company</th>
        <th>Shares Owned</th>
        <th>Purchase Price</th>
        <th>Profit</th>
      </tr>

      <tr v-for="investment in investments" :key="investment.buyId" :investment="investment">
        <td>
          <router-link :to="{name: 'SellStock', params: {stockId: investment.stockId}}">
            <div @click="setInvestmentToSell(investment)">{{investment.companyTicker}}</div>
          </router-link>
        </td>
        <td>{{investment.initialSharesPurchased}}</td>
        <td>{{investment.amountPerShare}}</td>
        <td>{{investment.profit}}</td>

        <!-- <td>
      <div v-if="isSelling">
        <label for="sellStock" />
        <input type="number" id="sellStock" min="0" :max="investment.currentShares" v-model.number="investmentToSell.sharesSold" />
        <button @click="submitSell">Sell</button>
      </div>
      <div v-else>
        <button @click="isSelling = !isSelling">Sell?</button>
      </div>
        </td>-->
      </tr>
    </table>
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
export default {
  components: {},
  data() {
    return {
      currentStockMarket: this.$store.state.currentStockMarket,
      // investments: [
      //   {
      //     id: 1,
      //     usersId: 1,
      //     stockId: 35,
      //     companyTicker: "AAPL",
      //     gameId: 1,
      //     initialSharesPurchased: 2,
      //     sharesCurrentlyOwned: 2,
      //     amountPerShare: 460,
      //     buyTime: "",
      //     profit: 40,
      //   },
      // ],
      investments: []
    };
  },
  methods: {
    setInvestmentToSell(investment) {
      this.$store.commit("SET_INVESTMENT", investment)
      const stock = this.$store.state.currentStockMarket.find(s => s.ticker === investment.companyTicker)
      this.$store.commit("SET_STOCK", stock)
    }
    //   focus() {
    //       this.$refs.sellStock.focus()
    //  }
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
</style>