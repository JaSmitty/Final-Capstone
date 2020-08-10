<template>
  <div>
    
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