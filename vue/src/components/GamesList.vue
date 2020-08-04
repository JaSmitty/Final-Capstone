<template>
  <div class="games-list">
    <h1 id="title">Active Games</h1>
    <active-game id="game" v-for="game in games" :key="game.gameID" :game="game" />
  </div>
</template>

<script>
import ActiveGame from "./ActiveGame.vue";
import gamesService from "@/services/GamesService";

export default {
  name: "GamesList",
  components: {
    ActiveGame,
  },
  data() {
    return {
      games: [],
    };
  },
  created() {
    gamesService.getGamesByUserId().then((response) => {
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
  
}
</style>