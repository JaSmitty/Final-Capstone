<template>
  <div class="home">
    <h1 id="welcome">Welcome, {{$store.state.user.username}}!</h1>
    <games-list />
    <div class="footer">
    <router-link :to="{name: 'CreateGame'}" ><button class="button">Create Game!</button></router-link>
    </div>
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
  background: #004d99;
  color: white;
  /* text-align: center; */
  padding-left: 10px;
  padding-bottom: 8px;
  padding-top: 8px;
  margin-top: 8px;
  margin-bottom: -22px;
  font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
  font-size: 25px;

}

.footer{
  background: #004d99;
  padding-bottom: 8px;
  padding-top: 8px;
  display: flex;
  justify-content: center;
}


</style>
