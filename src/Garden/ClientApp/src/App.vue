<template>
  <div class="h-screen w-screen">
    <main-navigation v-if="isLoginPage === false" />
    <router-view/>
  </div>
</template>

<script setup lang="ts">
import { useGardenStore } from "./stores/garden";
import {onMounted, ref, watch} from "vue";
import MainNavigation from "./components/MainNavigation.vue";
import {useRouter} from "vue-router";

const token = ref<string>("")
const store = useGardenStore()
const router = useRouter()
const isLoginPage = ref<boolean>(false)

watch(() => store.token, (t) => {
  token.value = t
})

router.afterEach(() => {
  isLoginPage.value = router.currentRoute.value.path === "/login";
})

</script>

<style scoped>

</style>
