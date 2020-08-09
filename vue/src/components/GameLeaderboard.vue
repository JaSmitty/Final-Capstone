<template>
  <div>
      <h1>Leaderboard</h1>
      <ul>
          <li v-for="friend in friends" :key="friend.id">{{friend.username}}</li>
        </ul>
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
export default {
    data() {
    return {
      friends: [],
    };
  },
  computed: {
      game() {
          return this.$store.state.currentGame;
      }
  },
  created() {
    gamesService.getPlayersInGame(this.game.gameId).then((response) => {
      if (response.status === 200) {
        this.friends = response.data.filter(
          (friend) => friend.userId !== this.$store.state.user.userId
        );
      }
    });
  },
}
</script>

<style>

</style>