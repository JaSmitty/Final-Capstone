<template>
  <div>
      <form @submit.prevent="createGame">
          <label for="date">End Date: </label>
          <input type="date" id="date" v-model="game.endDate">

          <label for="name">Game Name: </label>
          <input type="text" id="name" v-model="game.name">
          <input type="submit" value="Create Game!">
          
      </form>
      <invite-friends :gameId="gameId" />
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
        organizer_id: 1
        
      },
      gameId: 0
    }
  },
  methods: {
    createGame() {
      gamesService.createGame(this.game).then(response => {
      if (response.status === 201) {
        this.game = {};
        this.gameId = response.data
      }
    })
    }
  }
}
</script>

<style>

</style>