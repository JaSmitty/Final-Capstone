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
              v-bind:id="user.userId"
              v-bind:value="user.userId"
              v-model="selectedUserIds"
            />
          </td>
          <td>{{user.username}}</td>
        </tr>
        <tr>
            <input type="submit" value="Invite">
        </tr>
      </table>
    </form>
  </div>
</template>

<script>
import usersService from "@/services/UsersService.js";
import gamesService from "@/services/GamesService.js";

export default {
  data() {
    return {
      users: [],
      selectedUserIds: [],
      usersGames: [],
      filter: {
        username: "",
      },
      userId: this.$store.state.user.userId
    };
  },
  props: {
      gameId: Number
  },
  methods: {
      inviteUsers() {
          this.selectedUserIds.forEach(userId => {
            let userGame = {};
            userGame.userId = userId;
            userGame.gameId = this.gameId;
            this.usersGames.push(userGame);
          })
          gamesService.inviteUsers(this.usersGames, this.userId);
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
  },
  created() {
    usersService.getAllOtherUsers(this.userId).then((response) => {
      if (response.status === 200) {
        this.users = response.data;
      }
    });
  },
};
</script>

<style>
</style>