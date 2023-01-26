<template>
  <Form @submit="login">
    <Field type="email" name="email" placeholder="email" :rules="validateEmail" />
    <ErrorMessage name="email"/>
    
    <Field type="password" name="password" placeholder="password" :rules="validatePassword" />
    <ErrorMessage name="password" />
    
    <button>Login</button>
  </Form>
</template>

<script setup lang="ts">
import axios from "axios";
import { Form, Field, ErrorMessage } from "vee-validate";
import { useRouter } from "vue-router";

import { useGardenStore } from "../stores/garden";
import { computed } from "vue";

const store = useGardenStore()
const router = useRouter()

let returnPath = computed(() => {
  let back = router.options.history.state.back
  return back ? back : '/'
})
    
function login(values: any) {
  axios.post(
      'https://localhost:7161/garden/identity/login',
      {
        'email': values.email,
        'password': values.password
      }
  ).then((response) => {
    if(response.status == 200) {
      store.token = response.data 
      store.isAuthenticated = true
      
      router.push(returnPath.value.toString())
    }
  }).catch((error) => {
    console.log(error)
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