<template>
  <div id="leaderboard">
    <h1>Leaderboard</h1>
    <table>
      <tr class="table-headings">
        <th>&nbsp;</th>
        <th class="balance">Balance</th>
        <th>Total Worth</th>
      </tr>
      <tr v-for="player in players" :key="player.id">
        <td>{{player.username}}</td>
        <td class="balance">${{(player.balance).toFixed(2)}}</td>
        <td>${{(player.totalWorth).toFixed(2)}}</td>
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

#leaderboard table {
  border: rgba(0, 26, 51) solid 3px;
  
  background: rgba(0, 26, 51, 0.7);
  border-radius: 8px;
  padding-top: 4px;
  padding-bottom: 4px;
  margin-left: 4px;
  margin-right: 4px;

}

#leaderboard th {
  border-bottom: #add6ff solid 2px;
}

#leaderboard td {
  border-bottom: #add6ff solid 1px;
}

.balance{
  border-left:#add6ff solid 2px;
  border-right: #add6ff solid 2px;
}

#leaderboard{
  display: flex;
  flex-direction: column;
  
}

</style>