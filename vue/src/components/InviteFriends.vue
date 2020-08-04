<template>
  <div>
    <table v-for="user in users" :key="user.userId">
      <tr>
        <td>
          <input
            type="checkbox"
            id="selectAll"
          />
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>
          <input
            type="checkbox"
            v-bind:id="user.userId"
            v-bind:value="user.userId"
          />
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
    };
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