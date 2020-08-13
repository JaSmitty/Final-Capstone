<template>
  <div id="games-list">
    <div class="active">
      <h1 class="title">Active Games</h1>
      <div class="game-card-display open">
        <active-game-details
          id="game"
          class="open"
          v-for="game in sortedActiveGames"
          :key="game.gameID"
          :game="game"
        />
      </div>
    </div>
    <div class="invitations">
      <h1 class="title">Invitations</h1>
      <div class="game-card-display invited">
        <pending-game-details
          id="game"
          class="inv"
          v-for="game in pendingGames"
          :key="game.gameID"
          :game="game"
        />
      </div>
      </div>
    <div>
      <div class="complete">
      <h1 class="title">Completed Games</h1>
      <div class="game-card-display completed">
        <completed-game-details 
        id="game"
        class="open"
          v-for="game in sortedCompletedGames"
          :key="game.gameID"
          :game="game" />
      </div>
      </div>
    </div>
    
  </div>
</template>

<script>
import ActiveGameDetails from "./ActiveGameDetails.vue";
import PendingGameDetails from "./PendingGameDetails.vue";
import CompletedGameDetails from "./CompletedGameDetails.vue";
import gamesService from "@/services/GamesService";

export default {
  name: "GamesList",
  components: {
    ActiveGameDetails,
    PendingGameDetails,
    CompletedGameDetails
  },
  data() {
    return {
      activeGames: [],
      pendingGames: [],
      completedGames: []
    };
  },
  computed: {
    sortedActiveGames() {
      let sortedActiveGames = this.activeGames
      return sortedActiveGames.sort(function (a, b) {
        return a.startDateAsTicks - b.startDateAsTicks
      })
    },
    sortedCompletedGames() {
      let sortedCompletedGames = this.completedGames
      return sortedCompletedGames.sort(function (a, b) {
        return b.endDateAsTicks - a.endDateAsTicks
      })
    }
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
    gamesService.getCompletedGames().then((response) => {
      if (response.status === 200) {
        this.completedGames = response.data;
      }
    });
  },
};
</script>

<style scoped>
#games-list {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas: "active-games invites completed";
}
.active {
  grid-area: active-games;
  border-right: 3px solid #e7f3ff;
}

.invitations {
  grid-area: invites;
  border-left: 3px solid #e7f3ff;
  border-right: 6px solid #e7f3ff;
}

.complete {
  grid-area: completed;
  
}

#game {
  border-left: solid #005cb8 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #5cadff, #e7f3ff);
  border-radius: 8px;
  width: 50%;
}

#game.inv{
  padding-bottom: 20px;
}

#game.open:hover {
  border-left: solid #c15803 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #f06e04, #fcd5b6);
  border-radius: 8px;
  width: 50%;
}

.title {
  background: rgba(173, 214, 255, 0.90);
  color: #003366;
  padding-left: 15px;
  padding-top: 20px;
  padding-bottom: 20px;
  grid-area: title;
  text-align: center;
  margin-top: 0px;
  border-radius: 2px;
}

.game-card-display {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>