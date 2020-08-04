<template>
  <div>
    <table>
      <tr>
        <td>&nbsp;</td>
        <td>
          <input type="text" id="usernameFilter" v-model="filter.username" />
        </td>
      </tr>
      <tr v-for="user in filteredList" :key="user.userId">
        <td>
          <input type="checkbox" v-bind:id="user.userId" v-bind:value="user.userId" v-model="selectedUserIds"/>
        </td>
        <td>{{user.username}}</td>
      </tr>
    </table>
  </div>
</template>

<script>
import usersService from "@/services/UsersService.js";
export default {
  data() {
    return {
      users: [],
      selectedUsersIds: [],
      filter: {
        username: "",
      },
    };
  },
  methods: {},
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
    usersService.getAllOtherUsers().then((response) => {
      if (response.status === 200) {
        this.users = response.data;
      }
    });
  },
};
</script>

<style>
</style>