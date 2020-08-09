<template>
  <div id="game-screen">
    <div class="current-game-stats">
      <h1>{{game.name}}</h1>
      <p>Your balance: ${{game.balance}}</p>
      <p>Start date: {{game.startDateAsString}}</p>
      <p>End date: {{game.endDateAsString}}</p>
      <!-- <div>
        <p>You are competing against the following users:</p>
        <ul>
          <li v-for="friend in friends" :key="friend.id">{{friend.username}}</li>
        </ul>
      </div> -->
    </div>
    <invite-friends class="invite" />
    <current-investments class="investments" />
    <stock-market class="available-stocks" />
    <game-leaderboard />
  </div>
</template>

<script>
// import gamesService from "@/services/GamesService";
import InviteFriends from "@/components/InviteFriends";
import StockMarket from "@/components/StockMarket";
import CurrentInvestments from "@/components/CurrentInvestments";
import GameLeaderboard from '@/components/GameLeaderboard'
export default {
//   data() {
//     return {
//       friends: [],
//     };
//   },
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
  },
//   created() {
//     gamesService.getPlayersInGame(this.game.gameId).then((response) => {
//       if (response.status === 200) {
//         this.friends = response.data.filter(
//           (friend) => friend.userId !== this.$store.state.user.userId
//         );
//       }
//     });
//   },
};
</script>

<style>
#game-screen {
  display: grid;
  grid-template-columns: 300px 1fr 300px;
}
</style>