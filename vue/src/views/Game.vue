<template>
  <div>
      <h1>{{game.name}}</h1>
      <p>Your balance: {{game.balance}}</p>
      <p>Start date: {{game.startDateAsString}}</p>
      <p>End date: {{game.endDateAsString}}</p>
      <div>
          <p>You are competing against the following users:</p>
          <ul>
              <li v-for="friend in friends" :key=friend.id>{{friend.username}}</li>
          </ul>
      </div>
      <invite-friends />
      <current-investments />
      <stocks-summary />
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
import stocksService from '@/services/StocksService'
import InviteFriends from '@/components/InviteFriends'
import StocksSummary from '@/components/StocksSummary'
import CurrentInvestments from '@/components/CurrentInvestments'
export default {
    data() {
        return {
            friends: []
        }
    },
    components: {
        InviteFriends,
        StocksSummary,
        CurrentInvestments
    },
    computed: {
        game() {
            return this.$store.state.currentGame
        }
    },
    created() {
        gamesService.getPlayersInGame(this.game.gameId).then(response => {
            if (response.status === 200) {
                this.friends = response.data.filter(friend => friend.userId !== this.$store.state.user.userId);
            }
        }),
        stocksService.getStocks().then(response => {
            if (response.status === 200) {
                this.$store.commit("SET_CURRENT_STOCKS", response.data)
            }
        })
    }
}
</script>

<style>

</style>