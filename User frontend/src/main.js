import './assets/base.css';
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { start } from './scripts/backend';
const app = createApp(App);


app.use(router);
app.mount("#app")
start();