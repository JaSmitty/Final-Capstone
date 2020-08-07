<template>
  <div id="games-list">
    <div class="active">
    <h1 class="title">Active Games</h1>
    <div class="game-card-display open">
      <active-game-details id="game" v-for="game in activeGames" :key="game.gameID" :game="game" />
    </div>
    </div>
    <div class="invitations">
    <h1 class="title">Invitations</h1>
    <div class="game-card-display invited">
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

#games-list{
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas: "active invites";
}
.active {
  grid-area: active;
  border-right: 3px solid #e7f3ff;
}

.invites{
  grid-area: invites;
  border-left: 3px solid #e7f3ff;
}

#game {
  border-left: solid #005cb8 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #5cadff,
#e7f3ff);
  border-radius: 8px;
  width: 50%;
  }

  #game:hover{
    border-left: solid #f6710b 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #fbbf92,
#feeadb);
  border-radius: 8px;
  width: 50%;
  }

  
.title {
  background: #005cb8;
  color: white;
  padding-left: 15px;
  padding-top: 20px;
  padding-bottom: 20px;
  grid-area: title;
  text-align: center;
}

.game-card-display {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>