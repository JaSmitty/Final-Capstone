<template>
  <div class="home">
    <h1 id="welcome">Welcome, {{$store.state.user.username}}!</h1>
    <games-list />
    <router-link :to="{name: 'CreateGame'}">Create Game!</router-link>
  </div>
</template>

<script>
import GamesList from "@/components/GamesList.vue";
export default {
  name: "home",
  components: {
    GamesList
  },
};
</script>

<style scoped>

#welcome {
  background: #004080;
  color: white;
  padding-left: 15px;
  padding-bottom: 5px;
  padding-top: 5px;
  margin-top: 8px;
  margin-bottom: -22px;
  font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;

}
</style>
