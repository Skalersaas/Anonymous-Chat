import './assets/base.css'
import { start } from './scripts/backend'
import { createApp } from 'vue'
import App from './App.vue'

createApp(App).mount('#app')

start();
