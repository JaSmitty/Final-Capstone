<template>
  <div>
    <h1 class="create-title">Create a Game</h1>
    <div class="create-form">
      
      <form @submit.prevent="createGame">
          <label class="create-label" for="date">Start Date: </label>
          <input type="date" id="date" v-model="game.startDate">

          <label class="create-label" for="date">End Date: </label>
          <input type="date" id="date" v-model="game.endDate">

          <label class="create-label" for="name">Game Name: </label>
          <input type="text" id="name" v-model="game.name">
          <input class="btn" type="submit" value="Create Game!">
          
      </form>
      <div v-if="isCreated">
        <p class="success">Your game "{{game.name}}" has been created. It will end on {{game.endDate}}</p>
        <label for=""></label>
        <invite-friends />
        <router-link class="link go-to" :to="{name: 'Game', params: {gameId: game.gameId}}">Go to game</router-link>
      </div>
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

.create-form{
  background: rgba(173, 214, 255, 0.9);
  padding-top: 20px;
  padding-bottom: 20px;
  
}

.create-label{
  color: #003366;
  font-size: 20px;
}

.success{
   color: #003366;
  font-size: 20px;
}
.create-title{
  background: rgba(0, 26, 51, 0.7);
  margin-top: 0px;
  margin-bottom: 0px;
  padding-top: 10px;
  padding-bottom: 10px;
}

.link.go-to{
  font-size: 18px;
  color: #003366;
}

.link.go-to:hover{
  color: #f68e4f;
}

</style>