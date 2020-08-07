<template>
  <div class="container">
    <div id="nav">
      <router-link class="link main home" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link class="link main logout" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
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
  display: grid;
  grid-template-rows: 40px 1fr;
  grid-template-areas: "nav"
                       "content";

}

.link{
  color:#f1eff0;
}
.link:hover{
  color: #f68e4f;
}

.link.main {
  font-size: x-large;
 
  
}
#nav{
   background: #001f3d;
   grid-area: nav;

}

.content{
  grid-area: content;
}


</style> 
