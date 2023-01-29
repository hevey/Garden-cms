<template>
  <div class="h-screen w-screen">
    <login-form />
  </div>
</template>

<script setup lang="ts">
import LoginForm from "../components/LoginForm.vue";
import {onMounted} from "vue";
import {useGardenStore} from "../stores/garden";
import {useRouter} from "vue-router";

const store = useGardenStore()
const router = useRouter()

onMounted(() => {

  if(store.getIsAuthenticated()) {
    router.push('/')
  }
})

</script>

<style scoped>

</style>