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

      <tr v-for="investment in investments" :key="investment.id" :investment="investment">
        <td>
          <router-link :to="{name: 'SellStock', params: {stockId: investment.stockId}}">
            <div @click="setStockToSell(investment)">{{investment.companyTicker}}</div>
          </router-link>
        </td>
        <td>{{investment.currentShares}}</td>
        <td>{{investment.pricePerShare}}</td>
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
export default {
  components: {},
  data() {
    return {
      currentStockMarket: this.$store.state.currentStockMarket,
      investments: [
        {
          id: 1,
          usersId: 1,
          stockId: 35,
          companyTicker: "AAPL",
          gameId: 1,
          initialShares: 2,
          currentShares: 2,
          pricePerShare: 90,
          profit: 40,
        },
      ],
    };
  },
  methods: {
    setStockToSell(investment) {
      this.$store.commit("SET_STOCK", investment)
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
};
</script>

<style>
</style>