import { createApp } from 'vue'
import { createPinia } from 'pinia'
import {createRouter, createWebHistory} from "vue-router";
import './style.css'
import App from './App.vue'
import Home from "./pages/Home.vue";

const pinia = createPinia()
const app = createApp(App)

const routes = [
    { path: '/', component: Home }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

app.use(pinia)
app.use(router)
app.mount('#app')
