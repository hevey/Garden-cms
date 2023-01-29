<template>
  <div class="grid grid-cols-1 place-content-center mx-auto w-64">
    <Form class="grid grid-cols-1 grid-rows-6 bg-teal-500 h-96 mx-auto" @submit="login">
      <Field class="bg-transparent test-white placeholder-white row-start-1" type="email" name="email" placeholder="email" :rules="validateEmail" />
      <ErrorMessage class="row-start-2" name="email"/>

      <Field class="bg-transparent text-white placeholder-white row-start-3" type="password" name="password" placeholder="password" :rules="validatePassword" />
      <ErrorMessage name="password row-start-4" />

      <button class="row-start-5">Login</button>
      <p class="row-start-6">{{errorMessage}}</p>
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