<template>
  <div>
      <games-list />

      <create-game />
      <invite-friends />
      
  </div>
</template>

<script>
import GamesList from '@/components/GamesList.vue'
import CreateGame from '@/components/CreateGame.vue'
import InviteFriends from '@/components/InviteFriends.vue'
export default {
    components: {
        GamesList,
        CreateGame,
        InviteFriends
    }
}
</script>

<style>

</style>