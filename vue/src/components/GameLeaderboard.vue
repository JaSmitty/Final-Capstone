<template>
  <div id="leaderboard">
    <h1>Leaderboard</h1>
    <table>
      <tr class="table-headings">
        <th>&nbsp;</th>
        <th class="balance">Balance Remaining</th>
        <th>Total Worth</th>
      </tr>
      <tr v-for="player in sortedPlayers" :key="player.id" :class="{highlight: player.username === $store.state.user.username}">
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
export default {
  props: {
    players: Array
  },
  computed: {
    game() {
      return this.$store.state.currentGame;
    },
    sortedPlayers() {
      let sortedPlayers = this.players;
      return sortedPlayers.sort(function (a, b) {
        return b.totalWorth - a.totalWorth;
      })
    }
  }
};
</script>

<style>
#leaderboard > h1 {
  color: #003366;
} 

#leaderboard table {
  border: rgba(0, 26, 51) solid 3px;
  
  background: rgba(0, 26, 51, 0.7);
  /* border-radius: 8px; */
  padding-top: 4px;
  padding-bottom: 4px;
  margin-left: 0px;
  margin-right: 0px;

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

.highlight{
  background: rgba(246, 142, 79, 0.8);  /*opacity */
  color: white;
}

</style>