import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createRouter, createWebHistory } from "vue-router";
import { createPersistedState } from "pinia-plugin-persistedstate";
import './style.css'
import Home from "./pages/Home.vue";
import App from "./App.vue";
import Login from "./pages/Login.vue";
import Logout from "./pages/Logout.vue";
import Types from "./pages/ItemTypes.vue";


const pinia = createPinia()
pinia.use(createPersistedState())

const app = createApp(App)

const routes = [
    { path: '/', component: Home },
    { path: '/login', component: Login },
    { path: '/logout', component: Logout },
    { path: '/types', component: Types }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})


app.use(pinia)
app.use(router)
app.mount('#app')
