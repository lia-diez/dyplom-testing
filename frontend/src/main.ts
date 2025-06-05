import { createApp } from 'vue'
import App from './App.vue'
import './style.css'

import 'primeicons/primeicons.css'

import PrimeVue from 'primevue/config';
import Aura from '@primeuix/themes/aura';
import mdiVue from 'mdi-vue/v3'
import * as mdijs from '@mdi/js'
import About from "./components/About.vue";
import {createWebHistory, createRouter} from "vue-router";
import Profile from "./components/Profile.vue";

const routes = [
    { path: '/', component: About },
    { path: '/profile', component: Profile },
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})


const app = createApp(App);
app.use(router)

app.use(mdiVue, {
    icons: mdijs
});
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            prefix: 'p',
            darkModeSelector: '.app-dark',
            cssLayer: false
        }
    }
});


app.mount('#app');

