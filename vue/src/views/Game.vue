<template>
  <div>
      <invite-friends />
  </div>
</template>

<script>
import InviteFriends from '@/components/InviteFriends'
export default {
    components: {
        InviteFriends
    },
    computed: {
        game() {
            return this.$store.state.currentGame
        }
    }
}
</script>

<style>

</style>