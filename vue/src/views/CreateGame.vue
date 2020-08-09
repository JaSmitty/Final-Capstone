<template>
  <div>
    <h1>Create a Game</h1>
      <form @submit.prevent="createGame">
          <label for="date">Start Date: </label>
          <input type="date" id="date" v-model="game.startDate">

          <label for="date">End Date: </label>
          <input type="date" id="date" v-model="game.endDate">

          <label for="name">Game Name: </label>
          <input type="text" id="name" v-model="game.name">
          <input type="submit" value="Create Game!">
          
      </form>
      <div v-if="isCreated">
        <p>Your game {{game.name}} has been created. It will end on {{game.endDate}}</p>
        <label for=""></label>
        <invite-friends />
        <router-link :to="{name: 'Game', params: {gameId: game.gameId}}">Go to game</router-link>
      </div>
      
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
import InviteFriends from '@/components/InviteFriends.vue'

export default {
  components: {
        InviteFriends
  },
  data() {
    return {
      game: {
        balance: 100000,
        organizerId: this.$store.state.user.userId
      },
      isCreated: false,
    }
  },
  methods: {
    createGame() {
      gamesService.createGame(this.game).then(response => {
      if (response.status === 201) {
        this.$store.commit("SET_CURRENT_GAME", response.data);
        this.game.gameId = response.data.gameId;
        this.isCreated = true;
      }
    })
    }
  }
}
</script>

<style>

</style>