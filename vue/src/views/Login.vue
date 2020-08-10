<template>

<div id="main-page">
  <div id="description">
    <h1>Virtual Stock Market</h1>
    
      <h2>Invest virtual money in the stock market and compete against your friends! The player with the highest net-worth at the end of the game, wins!</h2>
      </div>
    
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <label for="username" class="sr-only">Username: </label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <label for="password" class="sr-only">Password: </label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <router-link class="link signup" :to="{ name: 'register' }">Need an account?</router-link>
      <button type="submit">Sign in</button>
    </form>
  </div>
  </div>
  
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style scoped>

.image{
  height: 300px;
  width: 50%;
}

#main-page {
  display: grid;
  grid-template-columns: 300px 1fr;
  grid-template-rows: 200px 1fr;
  grid-template-areas: "login description"
                      "login description"

}

#login {
  grid-area: login;
  background: rgba(173, 214, 255, 0.9);
  color: #003366;
  width: 260px;
  height: 95.4vh;
  padding-left: 5px;
  padding-right: 5px;
  border-radius: 3px;
  
}



#description{
  grid-area: description;
  background: radial-gradient(#fcd5b6, 	#f06e04);
  border-radius: 7px;
  width: 1000px;
  height: 250px;
  justify-self: center;
  align-self: center;
  position: relative;
  padding: 20px 20px;
}

#description > h1 {
  font-size: 50px;
}

#description > h2 {
  padding-top: 20px;
}

.link.signup {
  color: #003366;
}

.link.signup:hover{
  color: #f68e4f;
}

</style>
