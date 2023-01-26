<template>
  <Form @submit="login">
    <Field type="email" name="email" placeholder="email" :rules="validateEmails" />
    <ErrorMessage name="email"/>
    
    <Field type="password" name="password" placeholder="password" :rules="validatePassword" />
    <ErrorMessage name="password" />
    
    <button>Login</button>
  </Form>
  <p>{{returnValue}}</p>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { Form, Field, ErrorMessage } from "vee-validate";

const returnValue = ref("")

function login(values: any) {
  axios.post(
      'https://localhost:7161/garden/identity/login',
      {
        'email': values.email,
        'password': values.password
      }
  ).then((response) => {
    if(response.status == 200) {
      returnValue.value = response.data 
    }
    else {
      returnValue.value = response.statusText
    }
  }).catch((error) => {
    console.log(error)
  })
}

function validateEmails(value: any) {
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