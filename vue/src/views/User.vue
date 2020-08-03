<template>
  <div>
      <games-list />
      <!-- add a button to create a game -->
      <!-- <router-link>
          <button>Create a new game</button>
    </router-link> -->
      
  </div>
</template>

<script>
import GamesList from '@/components/GamesList.vue'
export default {
    components: {
        GamesList
    }
}
</script>

<style>

</style>