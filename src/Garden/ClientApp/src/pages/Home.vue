<template>
  <div>
    <h2 class="text-xl">Welcome to Garden</h2>
    <p>You have logged in successfully</p>
    
  </div>
</template>

<script setup lang="ts">
import {useGardenStore} from "../stores/garden";
import {useRouter} from "vue-router";
import {onMounted} from "vue";
import axios from "axios";

const store = useGardenStore()
const router = useRouter()

onMounted(() => {
  if(store.getIsAuthenticated() && store.getToken() !== "") {
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

</script>

<style scoped>

</style>