<template>
  <div>
    <router-link :to="{name: 'Game', params: {gameId: game.gameId}}">
        <div @click="setGameObject">
            <h2 id="game-name">{{game.name}}</h2>
      <h2 class="info">Organizer: {{game.organizerName}}</h2>
      <h2 class="info">Start Date: {{game.startDateAsString}}</h2>
      <h2 class="info">End Date: {{game.endDateAsString}}</h2>
        </div>
    </router-link>
  </div>
</template>

<script>
export default {
  props: {
    game: Object,
  },
  methods: {
      setGameObject() {
          this.$store.commit("SET_CURRENT_GAME", this.game)
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
</style>