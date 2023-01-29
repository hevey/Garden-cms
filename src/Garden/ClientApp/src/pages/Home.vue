<template>
  <h1 class="text-4xl">Welcome to Garden</h1>
  <p>You have logged in successfully</p>
  
  <button @click="Logout">Logout</button>
</template>

<script setup lang="ts">
import {useGardenStore} from "../stores/garden";
import {useRouter} from "vue-router";
import {onMounted} from "vue";
import axios from "axios";

const store = useGardenStore()
const router = useRouter()

onMounted(() => {
  if(store.getIsAuthenticated()) {
    axios.post('https://localhost:7161/garden/identity/validate',
        {
          'token': store.getToken()
        }
    ).then((response) => {
      if(response.status == 200) {
        router.push('/') 
      }
    }).catch((error) => {
      store.setToken("")
      store.setIsAuthenticated(false)
      
      router.push('/login')
    })
  } else {
    router.push('/login')
  }
})

function Logout() {
  router.push('/logout')
}

</script>

<style scoped>

</style>