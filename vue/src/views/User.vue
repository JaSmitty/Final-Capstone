<template>
  <div>
      <games-list />
      <!-- add a button to create a game -->
      <router-link>
          <button>Create a new game</button>
    </router-link>
      
  </div>
</template>

<script>
import GamesList from '@/components/GamesList.vue'
export default {
    GamesList
}
</script>

<style>

</style>