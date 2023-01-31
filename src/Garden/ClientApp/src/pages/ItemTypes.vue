<template>
<h2>Items</h2>
</template>

<script setup lang="ts">
  import axios from "axios";
  import {useGardenStore} from "../stores/garden";
  import {onMounted} from "vue";
  import {ItemsResponse} from "../Models/itemsResponse";

  const gardenStore = useGardenStore()
  
  onMounted(() => {
    getItemTypes()
  })
  
  async function getItemTypes() {
    try {
      const { data, status } = await axios.get<ItemsResponse>("https://localhost:7161/garden/items",{
        headers: {
          'Authorization': `Bearer ${gardenStore.getToken()}`
        }
      });
      
      data.data.forEach((item) => {
        console.log(item.name)
        
      })
      console.log();
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.log('error message: ', error.message);
      } else {
        console.log('unexpected error: ', error);
      }
    }
  }
</script>

<style scoped>

</style>