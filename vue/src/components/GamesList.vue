<template>
  <div>
    <h1>List of Games</h1>
    <active-game v-for="game in games" :key="game.gameID" :game="game" />
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

<style>
</style>