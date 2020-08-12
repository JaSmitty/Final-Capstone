<template>
  <div>
    <div class="nav-back">
    <router-link class="link back" :to="{name: 'Game', params: {gameId: $route.params.gameId}}">Back To Game</router-link>
    </div>
    <stock-details :stock="stock" />
    <div class="sell">
      <div class="sell-card">
        <h2>Current shares: {{investment.sharesCurrentlyOwned}}</h2>
        <h2>Purchase price per share: ${{investment.amountPerShare}}</h2>
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
              :max="investment.sharesCurrentlyOwned"
              step="0.01"
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
          <input class="btn" type="submit" value="Sell" />
        </form>
      </div>
    </div>
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
      // stock: this.$store.state.stock,
      // investment: this.$store.state.investment,
      investmentToSell: {
        buyReferenceId: "",
        stockAtSellId: "",
        sharesSold: "",
      },
    };
  },
  computed: {
    profitPerShare() {
      return (this.stock.c - this.investment.amountPerShare).toFixed(2);
    },
    totalProfit() {
      return (
        this.investmentToSell.sharesSold *
        (this.stock.c - this.investment.amountPerShare)
      ).toFixed(2);
    },
    investment() {
      return this.$store.state.investment
    },
    stock() {
      return this.$store.state.stock
    },
  },
  methods: {
    submitSell() {
      stocksService.submitSell(this.investmentToSell).then((response) => {
        if (response.status === 201) {
          this.$router.push(`/games/${this.$route.params.gameId}`);
        }
      });
    },
  },
  created() {
    stocksService.getInvestment(this.$route.params.stockId, this.$route.params.gameId).then(response => {
      if (response.status === 200) {
        this.$store.commit("SET_INVESTMENT", response.data);
        this.investmentToSell.buyReferenceId = response.data.buyId;
        this.investmentToSell.stockAtSellId = response.data.stockId;
        stocksService.getCurrentStock(response.data.companyTicker).then(response => {
          if (response.status === 200) {
            this.$store.commit("SET_STOCK", response.data)
          }
        })
      }
    })
  }
};
</script>

<style>
.sell {
  display: flex;
  justify-content: center;
}

.sell-card {
  background: radial-gradient(#fcd5b6, #f06e04);
  border-radius: 7px;
  width: 750px;
  margin-top: 70px;
  padding-bottom: 20px;
  opacity: 0.95;
}
</style>