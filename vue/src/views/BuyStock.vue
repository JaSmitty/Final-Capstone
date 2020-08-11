<template>
  <div>
    <stock-details :stock="stock"/>
    
    <div class="buy">
    <form @submit.prevent="submitBuy">
      <h2>How much stock do you want to purchase?</h2>
      <h4>Please specify:</h4>
      <label for="buyDollars">Dollars:</label>
      <input
        type="number"
        id="buyDollars"
        placeholder="e.g. 100"
        min="0.00"
        :max="$store.state.currentGame.balance"
        step="0.01"
        v-model.number="amount"
        @focus="blurShares"
        @blur="showShares"
      />
      <h4>OR</h4>
      <label for="buyShares">Shares:</label>
      <input
        type="number"
        id="buyShares"
        placeholder="e.g. 1.5"
        v-model.number="stockToBuy.initialSharesPurchased"
        @focus="blurDollars"
        @blur="showDollars"
      />
      <br />
      <br />
      <input type="submit" />
    </form>
    </div>
  </div>
</template>

<script>
import stocksService from '@/services/StocksService'
import StockDetails from '@/components/StockDetails'
export default {
  components: {
    StockDetails
  },
  data() {
    return {
      stock: this.$store.state.stock,
      stockToBuy: {
        stockId: this.$store.state.stock.stockId,
        gameId: this.$store.state.currentGame.gameId,
        initialSharesPurchased: ""
      },
      amount: "",
    };
  },
  methods: {
    submitBuy() {
      if (this.stockToBuy.initialSharesPurchased === "") {
        this.stockToBuy.initialSharesPurchased = (this.amount / this.stock.c)
      }
      stocksService.submitBuy(this.stockToBuy)
    },
    blurShares() {
      document.getElementById("buyShares").disabled = true;
    },
    showShares() {
      if (this.amount === "") {
        document.getElementById("buyShares").disabled = false;
      }
    },
    blurDollars() {
      document.getElementById("buyDollars").disabled = true;
    },
    showDollars() {
      if (this.stockToBuy.initialSharesPurchased === "") {
        document.getElementById("buyDollars").disabled = false;
      }
    },
  },
};
</script>

<style>


</style>