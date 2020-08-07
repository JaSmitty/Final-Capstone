<template>
  <div class="home">
    <h1 id="welcome">Welcome, {{$store.state.user.username}}!</h1>
    <games-list />
    <div class="footer">
    <router-link class="create" :to="{name: 'CreateGame'}" ><button class="button">Create Game!</button></router-link>
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

.home{
  display: grid;
  height: 95vh;
  grid-template-rows: 50px 1fr 75px;
  grid-template-areas: "welcome"
                       "games"
                       "footer";
}

#welcome {
  grid-area: welcome;
  background: #003366;
  color: white;
  /* text-align: center; */
  padding-left: 10px;
  padding-bottom: 8px;
  padding-top: 8px;
  margin-top: 8px;
  margin-bottom: 0px;
  font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
  font-size: 25px;
  margin-top: 0px;

}

games-list {
  grid-area: games;
}

.footer{
  grid-area: footer;
  background: #001f3d;
  padding-bottom: 8px;
  padding-top: 8px;
  display: flex;
  justify-content: center;
  align-self: end;
}

.button {
  padding: 15px 25px;
  font-size: 24px;
  text-align: center;
  cursor: pointer;
  outline: none;
  color: #fff;
  background-color: 	#f97b04;
  border: none;
  border-radius: 15px;
  box-shadow: 0 5px #999;
}
.button:hover {
  background-color: #d96308;
  }

  .button:active {
  background-color: #d96308;
  box-shadow: 0 5px #666;
  transform: translateY(4px);
}


</style>
