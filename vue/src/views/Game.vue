<template>
  <div id="game-screen">
      <h1 id="game-title">{{game.name}}</h1>
      <div class="current-game-stats">
      <p>Your balance: {{game.balance}}</p>
      <p>Start date: {{game.startDateAsString}}</p>
      <p>End date: {{game.endDateAsString}}</p>
      <div>
          <p>You are competing against the following users:</p>
          <ul>
              <li v-for="friend in friends" :key=friend.id>{{friend.username}}</li>
          </ul>
          </div>
      </div>
      <invite-friends class="invite"/>
      <div class="gameplay">
      <current-investments class="investments"/>
      <stocks-summary class="available-stocks"/>
      </div>
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

#game-screen{
    display: grid;
    grid-template-columns: 235px 1fr 235px;
    grid-template-areas: "stats title invite"
                         "stats gameplay invite";
}

.current-game-stats{
    grid-area: stats;
    background: #add6ff;
    width: 235px;
    height: 95.4vh;
    padding-left: 5px;
    padding-right: 5px;
    
}

.gameplay{
    grid-area: gameplay;
}

.invite{
    grid-area: invite;
    background: #add6ff; width: 250px;
    height: 95.4vh;
    width: 235px;
    
}


</style>