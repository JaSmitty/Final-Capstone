<template>
  <div class="games-list">
    <h1 class="title">Active Games</h1>
    <div class="game-card-display">
      <active-game-details id="game" v-for="game in activeGames" :key="game.gameID" :game="game" />
    </div>
    <h1 class="title">Invitations</h1>
    <div class="game-card-display">
      <pending-game-details id="game" v-for="game in pendingGames" :key="game.gameID" :game="game" />
    </div>
  </div>
</template>

<script>
import ActiveGameDetails from "./ActiveGameDetails.vue";
import PendingGameDetails from "./PendingGameDetails.vue";
import gamesService from "@/services/GamesService";

export default {
  name: "GamesList",
  components: {
    ActiveGameDetails,
    PendingGameDetails
  },
  data() {
    return {
      activeGames: [],
      pendingGames: []
    };
  },
  created() {
    gamesService.getActiveGames().then((response) => {
      if (response.status === 200) {
        this.activeGames = response.data;
      }
    });
    gamesService.getPendingGames().then((response) => {
      if (response.status === 200) {
        this.pendingGames = response.data;
      }
    });
  },
};
</script>

<style scoped>
.games-list {
  /* font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; */
  /* background: #f0f7ff; */
  /* background: url('../../images/4057028.jpg') */
}
#game {
  border-left: solid #0059b3 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #66b3ff, #cce6ff);
  border-radius: 8px;
  width: 50%;
}
.title {
  background: #0080ff;
  color: white;
  padding-left: 15px;
  padding-top: 20px;
  padding-bottom: 20px;
  grid-area: title;
  text-align: center;
}

.game-card-display {
  display: flex;
}
</style>