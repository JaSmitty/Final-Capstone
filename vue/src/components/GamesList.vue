<template>
  <div class="games-list">
    <div class="active">
    <h1 class="title">Active Games</h1>
    <div class="game-card-display">
      <active-game-details id="game" v-for="game in activeGames" :key="game.gameID" :game="game" />
    </div>
    </div>
    <div class="invites">
    <h1 class="title">Invitations</h1>
    <div class="game-card-display">
      <pending-game-details id="game" v-for="game in pendingGames" :key="game.gameID" :game="game" />
    </div>
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

.games-list{
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas: "active invites";
}
.active {
  grid-area: active;
  border-right: 2px solid #e7f3ff;
}

.invites{
  grid-area: invites;
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