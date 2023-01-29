<template>
<router-view/>
</template>

<script setup lang="ts">
import { useGardenStore } from "./stores/garden";
import { ref, watch } from "vue";

const token = ref<string>("")
const store = useGardenStore()

watch(() => store.token, (t) => {
  token.value = t
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
