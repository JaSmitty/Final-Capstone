<template>
  <div>
    <h1>Create a Game</h1>
      <form @submit.prevent="createGame">
          <label for="date">End Date: </label>
          <input type="date" id="date" v-model="game.endDate">

          <label for="name">Game Name: </label>
          <input type="text" id="name" v-model="game.name">
          <input type="submit" value="Create Game!">
          
      </form>
      <div v-if="isCreated">
        <p>Your game {{game.name}} has been created. It will end on {{game.endDate}}</p>
        <label for=""></label>
        <invite-friends :gameId="gameId" />
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
        // TODO remove hardcoded value
        organizerId: this.$store.state.user.userId
      },
      gameId: 0,
      isCreated: false,
    }
  },
  methods: {
    createGame() {
      gamesService.createGame(this.game).then(response => {
      if (response.status === 201) {
        // this.game = {};
        this.gameId = response.data
        this.isCreated = true;
      }
    })
    }
  }
}
</script>

<style>

</style>