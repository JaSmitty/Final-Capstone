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
import stocksService from '@/services/StocksService'
export default {
  name: "home",
  components: {
    GamesList
  },
  created() {
    stocksService.getStocks().then(response => {
            if (response.status === 200) {
                this.$store.commit("SET_CURRENT_STOCKS", response.data)
            }
        })
  }
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
  background: rgba(0, 51, 102, 0.95);
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
  border-radius: 2px;

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
  border-radius: 2px;
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
