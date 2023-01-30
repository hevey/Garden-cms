<template>
<nav class="grid grid-cols-2 border-b border-teal-800">
  <div class="flex justify-start space-x-4 w-full">
    <h1 class="px-4 py-2 my-3 text-2xl font-medium">Garden</h1>
  </div>
  <div class="flex justify-end space-x-1 w-full pr-3">
    <router-link to="/" class="nav-btn-primary">Home</router-link>
    <router-link to="/logout" class="nav-btn-primary text-teal-500">sign out</router-link>
  </div>
</nav>
</template>

<script setup lang="ts">

</script>

<style scoped>

</style>