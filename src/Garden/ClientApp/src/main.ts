import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createRouter, createWebHistory } from "vue-router";
import './style.css'
import Home from "./pages/Home.vue";
import App from "./App.vue";
import Login from "./pages/Login.vue";
import Logout from "./pages/Logout.vue";

const pinia = createPinia()
const app = createApp(App)

const routes = [
    { path: '/', component: Home },
    { path: '/login', component: Login},
    { path: '/logout', component: Logout}
]

const router = createRouter({
    history: createWebHistory(),
    routes
})


app.use(pinia)
app.use(router)
app.mount('#app')
