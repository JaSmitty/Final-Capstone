<template>
  <div class="games-list">
    <h1 id="title">Active Games</h1>
    <div class="game-card-display">
    <game-details id="game" v-for="game in games" :key="game.gameID" :game="game" />
    </div>
  </div>
</template>

<script>
import GameDetails from "./GameDetails.vue";
import gamesService from "@/services/GamesService";

export default {
  name: "GamesList",
  components: {
    GameDetails
  },
  data() {
    return {
      games: [],
      userId: this.$store.state.user.userId
    };
  },
  created() {
    gamesService.getGamesByUserId(this.userId).then((response) => {
      if (response.status === 200) {
        this.games = response.data;
      }
    });
  },
};
</script>

<style scoped>
.games-list{
  font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
  background: #f0f7ff;
  
 
  
}
#game{
  border-left: solid #0059b3 7px;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 1px;
  margin-bottom: 25px;
  background: linear-gradient(to right, #66b3ff, #cce6ff);
  border-radius: 8px;
  width: 50%;
  
}
#title {
  background: #0080ff;
  color: white;
  padding-left: 15px;
  padding-top: 20px;
  padding-bottom: 20px;
  grid-area: title;
  text-align: center;
}

.game-card-display{
  
  display: flex;

  
  
  
}
</style>