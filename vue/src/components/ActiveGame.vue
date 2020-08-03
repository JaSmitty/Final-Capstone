<template>
  <div>
      <h2>Organizer Name: {{game.organizer_Name}}</h2>
      <h2>Game Name: {{game.name}}</h2>
      <h2>End Date: {{game.end_Date}}</h2>
  </div>
</template>

<script>


export default {
    props: {
        game: Object
    }
}
</script>

<style>

</style>