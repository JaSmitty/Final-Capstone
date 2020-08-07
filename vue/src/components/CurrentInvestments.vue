<template>
  <div>
    <h1>Current investments</h1>
    <table>
      <tr>
        <td>Company</td>
        <td>Current Shares Owned</td>
        <td>Purchase Price</td>
        <td>Current Price per Share</td>
        <td>Sell</td>
      </tr>
    </table>
    <investment v-for="investment in investments" :key="investment.id" :investment="investment" />
  </div>
</template>

<script>
import Investment from "./Investment";
export default {
  components: {
    Investment,
  },
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