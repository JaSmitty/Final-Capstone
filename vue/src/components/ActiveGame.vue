<template>
  <div>
      <h2>Organizer Name: {{game.organizerName}}</h2>
      <h2>Game Name: {{game.name}}</h2>
      <h2>End Date: {{game.endDate}}</h2>
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