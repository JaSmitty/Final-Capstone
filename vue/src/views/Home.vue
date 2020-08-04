<template>
  <div class="home">
    <h1>Home</h1>
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
