<template>
  <div class="container">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view class="content"/>
  </div>
</template>

<style>
body{
  background-image: url('https://bitcoinist.com/wp-content/uploads/2018/10/shutterstock_732185581.jpg');
 font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; 
 color: white;
 text-align: center;
 
}

.container {
  width: 100%;
  /* max-width: 1150px; */
  margin: 0 auto;

}


</style> 
