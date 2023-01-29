<template>
  <h1 class="text-4xl">Welcome to Garden</h1>
  <p>You have logged in successfully</p>
  
  <button @click="Logout">Logout</button>
</template>

<script setup lang="ts">
import {useGardenStore} from "../stores/garden";
import {useRouter} from "vue-router";
import {onMounted} from "vue";

const store = useGardenStore()
const router = useRouter()

onMounted(() => {

  if(!store.getIsAuthenticated()) {
    router.push('/login')
  }
})

function Logout() {
  router.push('/logout')
}

</script>

<style scoped>

</style>