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
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
