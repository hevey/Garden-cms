<template>
  <div class="h-full w-full flex items-center justify-between bg-teal-500">
      <Form class="bg-white w-2/5 mx-auto h-96 flex flex-col pt-5" @submit="login">
        <div class="grid grid-rows-7 h-full">
          <h1 class="w-11/12 text-5xl py-3 px-5 mx-auto text-base row-start-1 h-12">Garden</h1>
          <Field class="w-11/12 row-start-2 h-12 rounded-md border border-[#E9EDF4] px-5 mx-auto text-base placeholder-[#ACB6BE] outline-none focus-visible:shadow-none focus:border-blue-500" type="email" name="email" placeholder="email" :rules="validateEmail" />
          <ErrorMessage class="w-11/12 text-red-500 row-start-5 px-5 h-fit mx-auto text-base" name="email"/>

          <Field class="w-11/12 row-start-3 h-12 rounded-md border border-[#E9EDF4] px-5 mx-auto text-base placeholder-[#ACB6BE] outline-none focus-visible:shadow-none focus:border-blue-500" type="password" name="password" placeholder="password" :rules="validatePassword" />
          <ErrorMessage class="w-11/12 text-red-500 row-start-6 px-5 h-fit mx-auto text-base" name="password" />

          <button class="w-11/12 row-start-4 h-12 rounded-md border border-primary px-5 mx-auto bg-blue-500 text-base text-white cursor-pointer hover:bg-opacity-90 transition">Login</button>
          <p class="w-11/12 row-start-7 text-red-500 px-5 mx-auto text-base">{{errorMessage}}</p>
        </div>
      </Form>
  </div>
</template>

<script setup lang="ts">
import axios from "axios";
import { Form, Field, ErrorMessage } from "vee-validate";
import { useRouter } from "vue-router";

import { useGardenStore } from "../stores/garden";
import {computed, ref} from "vue";

const store = useGardenStore()
const router = useRouter()

const errorMessage = ref<String>("")

let returnPath = computed(() => {
  let back = router.options.history.state.back
  return back ? back : '/'
})
    
function login(values: any) {
  errorMessage.value = ""
  
  axios.post(
      'https://localhost:7161/garden/identity/login',
      {
        'email': values.email,
        'password': values.password
      }
  ).then((response) => {
    if(response.status == 200) {
      store.setToken(response.data) 
      store.setIsAuthenticated(true)
      
      router.push(returnPath.value.toString())
    }
  }).catch((error) => {
    errorMessage.value = error.response.data
  })
}

function validateEmail(value: any) {
  if(!value) {
    return 'Email is required';
  }
  
  const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
  if (!regex.test(value)) {
    return 'This field must be a valid email';
  }
  
  return true;
}

function validatePassword(value: any) {
  if(!value) {
    return 'Password is required';
  }
  
  return true;
}
</script>

<style scoped>

</style>