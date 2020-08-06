<template>
  <div>
      <h1>{{game.name}}</h1>
      <p>Your balance: {{game.balance}}</p>
      <p>Start date: {{game.startDateAsString}}</p>
      <p>End date: {{game.endDateAsString}}</p>
      <div>
          <p>You are competing against the following users:</p>
          <ul>
              <li v-for="friend in friends" :key=friend.id>{{friend.username}}</li>
          </ul>
      </div>
      <invite-friends />
  </div>
</template>

<script>
import InviteFriends from '@/components/InviteFriends'
import gamesService from '@/services/GamesService'
export default {
    data() {
        return {
            friends: []
        }
    },
    components: {
        InviteFriends
    },
    computed: {
        game() {
            return this.$store.state.currentGame
        }
    },
    created() {
        gamesService.getPlayersInGame(this.game.gameId).then(response => {
            if (response.status === 200) {
                this.friends = response.data.filter(friend => friend.userId !== this.$store.state.user.userId);
            }
        })
    }
}
</script>

<style>

</style>