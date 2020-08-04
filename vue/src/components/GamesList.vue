<template>
  <div id="games-list">
    <h1 id="title">List of Games</h1>
    <active-game v-for="game in games" :key="game.gameID" :game="game" id="game"/>
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
#game{
  border-left: solid rgb(51, 51, 255) 6px;
  padding-left: 5px;
  background-color:rgb(51, 241, 255)
}
</style>