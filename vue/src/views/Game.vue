<template>
  <div id="game-screen">
    <div class="current-game-stats">
      <div class="player-stats">
      <h1>{{game.name}}</h1>
      <div class="stats">
      <p>Your balance: ${{(game.balance).toFixed(2)}}</p>
      <p>Start date: {{game.startDateAsString}}</p>
      <p>End date: {{game.endDateAsString}}</p>
      </div>
      </div>
      <!-- <div>
        <p>You are competing against the following users:</p>
        <ul>
          <li v-for="friend in friends" :key="friend.id">{{friend.username}}</li>
        </ul>
      </div> -->
      <div class="ranking">
      <game-leaderboard class="leaderboard" />
      </div>
    </div>
    <invite-friends class="invite" />
    <div class="main-content">
    <current-investments class="investments" v-if="isActive"/>
    <stock-market class="available-stocks" v-if="isActive"/>
    </div>
    <!-- <game-leaderboard class="leaderboard" /> -->
  </div>
</template>

<script>
import gamesService from '@/services/GamesService';
// import stocksService from '@/services/StocksService';
import InviteFriends from '@/components/InviteFriends';
import StockMarket from '@/components/StockMarket';
import CurrentInvestments from '@/components/CurrentInvestments';
import GameLeaderboard from '@/components/GameLeaderboard'
export default {
  data() {
    return {
    };
  },
  components: {
    InviteFriends,
    StockMarket,
    CurrentInvestments,
    GameLeaderboard
  },
  computed: {
    game() {
      return this.$store.state.currentGame;
    },
    isActive() {
      return this.now >= this.startDate
    },
    now() {
      return Date.now()
    },
    startDate() {
      return Date.parse(this.game.startDate)
    }
  },
  created() {
//     gamesService.getPlayersInGame(this.game.gameId).then((response) => {
//       if (response.status === 200) {
//         this.friends = response.data.filter(
//           (friend) => friend.userId !== this.$store.state.user.userId
//         );
//       }
//     });
      gamesService.getGameById(this.$route.params.gameId).then(response => {
        if (response.status === 200) {
          this.$store.commit("SET_CURRENT_GAME", response.data);
        }
      });
      
  },
};
</script>

<style>

#game-screen{
    display: grid;
    grid-template-columns: 235px 1fr 240px;
    grid-template-areas: "stats title invite"
                         "stats gameplay invite";
    
}

.main-content{
  display:grid;
  grid-template-areas: "investments"
                       "stocks";
  grid-template-rows: 1fr 1fr;
  height: 95vh;
}

.investments {
  grid-area: investments;
  margin-left: 13px;
  margin-right: 0px;
}

.available-stocks{
  grid-area: stocks;
  
}

.current-game-stats{
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    grid-area: stats;
    background: rgba(173, 214, 255, 0.9);
    width: 238px;
    height: 95.4vh;
    padding-left: 5px;
    padding-right: 5px;
    
}

.stats {
  background: rgba(0, 26, 51, 0.7);
  border-radius: 8px;
  padding-top: 4px;
  padding-bottom: 4px;
  margin-left: 4px;
  margin-right: 4px;
}

.gameplay{
    grid-area: gameplay;
}

.invite{
    grid-area: invite;
    background: rgba(173, 214, 255, 0.9); 
    width: 250px;
    height: 95.4vh;
    width: 235px;
    padding-right: 5px;
    
}

.leaderboard{
  grid-area: leaderboard;
}

.player-stats > h1 {
  color: #003366;
}
</style>