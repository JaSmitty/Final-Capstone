<template>
  <div>
    <h1>{{stock.companyName}} ({{stock.ticker}})</h1>
    <h2>
      {{stock.c}}
      <span :class="{'green': stock.c >= stock.o, 'red': stock.c < stock.o}">
        {{stock.c > stock.o ? "+" : ""}}
        {{(stock.c - stock.o).toFixed(3)}}
        ({{stock.c > stock.o ? "+" : ""}}
        {{((stock.c / stock.o) - 1).toFixed(3)}}%)
      </span>
    </h2>
    <h3>Open: {{stock.o}}</h3>
    <h3>High: {{stock.h}}</h3>
    <h3>Low: {{stock.l}}</h3>
    <h3>Previous Close: {{stock.pc}}</h3>
    <form @submit.prevent="setSharesToBuy">
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
        v-model.number="stockToBuy.sharesToBuy"
        @focus="blurDollars"
        @blur="showDollars"
      />
      <br />
      <br />
      <input type="submit" />
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      stock: this.$store.state.stock,
      stockToBuy: {
        sharesToBuy: ""
      },
      amount: "",
    };
  },
  methods: {
    setSharesToBuy() {
      if (this.stockToBuy.sharesToBuy === "") {
        this.stockToBuy.sharesToBuy = this.amount * this.stock.c
      }
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
      if (this.stockToBuy.sharesToBuy === "") {
        document.getElementById("buyDollars").disabled = false;
      }
    },
  },
};
</script>

<style>
</style>