<template>
  <div id="leaderboard">
    <h1>Leaderboard</h1>
    <table>
      <tr>
        <th>&nbsp;</th>
        <th>Balance</th>
        <th>Total Worth</th>
      </tr>
      <tr v-for="player in players" :key="player.id">
        <td>{{player.username}}</td>
        <td>${{player.balance}}</td>
        <td>${{player.totalWorth}}</td>
      </tr>
    </table>
    <ul>
      <li></li>
    </ul>
  </div>
</template>

<script>
import gamesService from "@/services/GamesService";
export default {
  data() {
    return {
      players: [],
    };
  },
  computed: {
    game() {
      return this.$store.state.currentGame;
    },
  },
  created() {
    gamesService.getPlayersInGame(this.game.gameId).then((response) => {
      if (response.status === 200) {
        this.players = response.data;
      }
    });
  },
};
</script>

<style>
#leaderboard > h1 {
  color: #003366;
} 

</style>