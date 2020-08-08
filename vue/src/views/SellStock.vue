<template>
  <div>
    <stock-details :stock="stock" />
    <h2>Current shares: {{investment.currentShares}}</h2>
    <h2>Purchase price per share: ${{investment.pricePerShare}}</h2>
    <h2>Current price per share: ${{stock.c}}</h2>
    <h2>
      Per share {{profitPerShare >= 0 ? 'profit: ' : 'loss: '}}
      <span
        :class="profitPerShare >= 0 ? 'green' : 'red'"
      >${{Math.abs(profitPerShare)}}</span>
    </h2>
    <form @submit.prevent="submitSell">
      <h2>
        <label for="sharesToSell">Enter shares to liquidate:</label>
        <input
          type="number"
          id="sharesToSell"
          min="0"
          :max="investment.currentShares"
          required
          v-model.number="investmentToSell.sharesSold"
        />
      </h2>
      <h2>
        Total {{profitPerShare >= 0 ? 'Profit: ' : 'Loss: '}}
        <span
          :class="profitPerShare >= 0 ? 'green' : 'red'"
        >${{Math.abs(totalProfit)}}</span>
      </h2>
      <input type="button" value="Sell" />
    </form>
  </div>
</template>

<script>
import StockDetails from "@/components/StockDetails";
import stocksService from "@/services/StocksService";
export default {
  components: {
    StockDetails,
  },
  data() {
    return {
      stock: this.$store.state.stock,
      investment: this.$store.state.investment,
      investmentToSell: {
        gameId: this.$store.state.currentGame.gameId,
        stockId: this.$store.state.stock.stockId,
        sharesSold: "",
      },
    };
  },
  computed: {
    profitPerShare() {
      return (this.stock.c - this.investment.pricePerShare).toFixed(2);
    },
    totalProfit() {
      return (
        this.investmentToSell.sharesSold *
        (this.stock.c - this.investment.pricePerShare)
      ).toFixed(2);
    },
  },
  methods: {
      submitSell() {
          stocksService.sellInvestment(this.investmentToSell);
      }
  }
};
</script>

<style>
</style>