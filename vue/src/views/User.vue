<template>
  <div>
      <games-list />

      <create-game />
      
  </div>
</template>

<script>
import GamesList from '@/components/GamesList.vue'
import CreateGame from '@/components/CreateGame.vue'

export default {
    components: {
        GamesList,
        CreateGame
    }
}
</script>

<style>

</style>