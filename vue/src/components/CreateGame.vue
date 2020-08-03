<template>
  <div>
      <form @submit.prevent="createGame">
          <label for="date">End Date: </label>
          <input type="date" id="date" v-model="game.endDate">

          <label for="name">Game Name: </label>
          <input type="text" id="name" v-model="game.name">
          <input type="submit" value="Create Game!">
          
      </form>
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
export default {
  data() {
    return {
      game: {
        // TODO remove hardcoded value
        organizer_id: 1
      }
    }
  },
  methods: {
    createGame() {
      gamesService.createGame(this.game).then(response => {
      if (response.status === 201) {
        this.game = {};
      }
    })
    }
  }
}
</script>

<style>

</style>