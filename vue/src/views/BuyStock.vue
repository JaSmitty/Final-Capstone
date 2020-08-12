<template>
  <div>
    <div class="nav-back">
    <router-link class="link back" :to="{name: 'Game', params: {gameId: $route.params.gameId}}">Back To Game</router-link>
    </div>
    <stock-details :stock="stock"/>
    
    <div class="buy">
      <div class="buy-card">
    <form @submit.prevent="submitBuy">
      <h2>How much stock do you want to purchase?</h2>
      <h3>Please specify:</h3>
      <label class="dollars" for="buyDollars">Dollars:</label>
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
      <label class="shares" for="buyShares">Shares:</label>
      <input
        type="number"
        id="buyShares"
        placeholder="e.g. 1.5"
        min="0.00"
        :max="$store.state.currentGame.balance / stock.c"
        v-model.number="stockToBuy.initialSharesPurchased"
        @focus="blurDollars"
        @blur="showDollars"
      />
      <br />
      <br />
      <input class='btn' type="submit" />
    </form>
    </div>
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
      // stock: this.$store.state.stock,
      stockToBuy: {
        stockId: "",
        gameId: Number(this.$route.params.gameId),
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
      stocksService.submitBuy(this.stockToBuy).then(response => {
        if (response.status === 201) {
          this.$router.push(`/games/${this.stockToBuy.gameId}`)
        }
      })
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
  computed: {
    stock() {
      return this.$store.state.stock
    }
  },
  created() {
    stocksService.getCurrentStock(this.$route.params.ticker).then(response => {
          if (response.status === 200) {
            this.$store.commit("SET_STOCK", response.data)
            this.stockToBuy.stockId = response.data.stockId
          }
        })
  }
};
</script>

<style>
.buy{
  display: flex;
  justify-content: center;
  align-content: center;
}

.buy-card{
   background: radial-gradient(#fcd5b6, 	#f06e04);
  border-radius: 7px;
  width: 750px;
  margin-top: 70px;
  padding-bottom: 20px;
  opacity: 0.95;
}

.dollars {
  font-size: 18px;
}

.shares{
  font-size: 18px;
}

.back{
  font-size: 20px;
  color: #003366;
}

.nav-back{
  background: rgba(173, 214, 255, 0.9);
  padding-top: 7px;
  padding-bottom: 7px;
}

</style>