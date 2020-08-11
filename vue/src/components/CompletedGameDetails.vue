<template>
  <div>
    <router-link class="game-card closed" :to="{name: 'Game', params: {gameId: game.gameId}}">
        <div>
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
  methods: {}
};
</script>

<style>

.game-card{
  text-decoration: none;
}




#game-name {
  font-size: 30px;
  margin-top: 10px;
  margin-bottom: 0px;
  color: white;
  text-decoration: underline;
  
}

.info {
  font-size: 20px;
  margin-top: 15px;
  color: #003366;
}
</style>