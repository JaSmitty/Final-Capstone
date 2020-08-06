<template>
  <div>
    <h1>Invite Friends</h1>
    <form @submit.prevent="inviteUsers">
      <table>
        <tr>
          <td>Search</td>
          <td>
            <input type="text" id="usernameFilter" v-model="filter.username" />
          </td>
        </tr>
        <br />
        <tr v-for="user in filteredList" :key="user.userId">
          <td>
            <input
              type="checkbox"
              v-bind:id="user"
              v-bind:value="user"
              v-model="selectedUsers"
            />
          </td>
          <td>{{user.username}}</td>
        </tr>
        <tr>
            <input type="submit" value="Invite">
        </tr>
      </table>
    </form>
    <div v-if="inviteSuccessful">
      The following users were invited: <span v-for="selectedUser in selectedUsers" :key="selectedUser.userId">{{selectedUser.username}} &nbsp;</span>
    </div>
  </div>
</template>

<script>
// import usersService from "@/services/UsersService.js";
import gamesService from "@/services/GamesService.js";

export default {
  data() {
    return {
      users: [],
      selectedUsers: [],
      usersGames: [],
      filter: {
        username: "",
      },
      userId: this.$store.state.user.userId,
      inviteSuccessful: false
    };
  },
  methods: {
      inviteUsers() {
          this.selectedUsers.forEach(user => {
            let userGame = {};
            userGame.userId = user.userId;
            userGame.gameId = this.gameId;
            this.usersGames.push(userGame);
          })
          gamesService.inviteUsers(this.usersGames).then(response => {
            if (response.status === 201) {
              this.inviteSuccessful = true;
            }
          });
          this.usersGames = [];
          // navigate to the game
      }
  },
  computed: {
    filteredList() {
      let filteredUsers = this.users;
      if (this.filter.username != "") {
        filteredUsers = filteredUsers.filter((user) =>
          user.username
            .toLowerCase()
            .includes(this.filter.username.toLowerCase())
        );
      }
      return filteredUsers;
    },
    gameId() {
      return this.$store.state.currentGame.gameId
    }
  },
  created() {
    gamesService.getUsersToInvite(this.gameId).then((response) => {
      if (response.status === 200) {
        this.users = response.data;
      }
    });
  },
};
</script>

<style>
</style>