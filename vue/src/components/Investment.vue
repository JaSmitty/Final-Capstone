<template>
  <div>
    <td>{{investment.companyTicker}}</td>
    <td>{{investment.currentShares}}</td>
    <td>{{investment.pricePerShare}}</td>
    <td>{{investment.profit}}</td>
    <td>
      <div v-if="isSelling">
        <label for="sellStock" />
        <input type="number" id="sellStock" min="0" :max="investment.currentShares" v-model.number="investmentToSell.sharesSold" />
        <button @click="submitSell">Sell</button>
      </div>
      <div v-else>
        <button @click="isSelling = !isSelling">Sell?</button>
      </div>
    </td>
  </div>
</template>

<script>
import stocksService from '@/services/StocksService'
export default {
  props: {
    investment: Object,
  },
  data() {
    return {
      isSelling: false,
      investmentToSell: {
          stockId: this.investment.stockId,
          gameId: this.$store.state.currentGame.gameId
      },
    };
  },
  methods: {
      submitSell() {
          stocksService.submitSell(this.investmentToSell);
      }
  }
};
</script>

<style>
</style>