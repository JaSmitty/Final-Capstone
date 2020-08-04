<template>
  <div>
    <table>
      <tr>
        <td>
          <input
            type="checkbox"
            id="selectAll"
            v-bind:checked="isSelectAll"
            v-on:change="selectAll"
          />
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>
          <input
            type="checkbox"
            v-bind:id="user.id"
            v-bind:value="user.id"
            v-model="selectedUserIDs"
          />
        </td>
        <td>test</td>
      </tr>
    </table>
  </div>
</template>

<script>
export default {
    
};
</script>

<style>
</style>