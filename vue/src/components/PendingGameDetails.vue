<template>
  <div>
    <h2 id="game-name">{{game.name}}</h2>
    <h2 class="info">Organizer: {{game.organizerName}}</h2>
    <h2 class="info">Start Date: {{game.startDateAsString}}</h2>
    <h2 class="info">End Date: {{game.endDateAsString}}</h2>
    <button class="btn accept" @click="acceptInvitation">Accept Invitation</button>
  </div>
</template>

<script>
import gamesService from '@/services/GamesService'
export default {
  props: {
    game: Object,
  },
  methods: {
      acceptInvitation() {
        let userGame = {userId: this.$store.state.user.userId, gameId: this.game.gameId};
        gamesService.acceptInvitation(userGame);
      }
  }
};
</script>

<style>
#game-name {
  font-size: 30px;
  margin-top: 10px;
  margin-bottom: 0px;
}

.info {
  font-size: 20px;
  margin-top: 15px;
  color: rgb(43, 66, 66);
}

.btn.accept{
  padding: 5px 10px;
  font-size: 15px;
   text-align: center;
  cursor: pointer;
  outline: none;
  color: #fff;
  background-color: 	#f97b04;
  border: none;
  border-radius: 10px;
  box-shadow: 0 5px #999;
}

.btn.accept:hover{
   background-color: #d96308;
  }

.btn.accept:active {
  background-color: #d96308;
  box-shadow: 0 5px #666;
  transform: translateY(4px);
}

</style>