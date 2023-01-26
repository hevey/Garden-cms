<script setup lang="ts">
import {useGardenStore} from "../stores/garden";
import {useRouter} from "vue-router";

const store = useGardenStore();
const router = useRouter()

if(store.isAuthenticated) {
  store.token = '';
  store.isAuthenticated = false
}

router.push('/')

</script>