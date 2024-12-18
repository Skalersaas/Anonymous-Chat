import { createRouter, createWebHistory } from "vue-router";
import ChatView from "@/views/ChatView.vue";
import AdminView from "@/views/AdminView.vue";

const routes = [
    {
        path: '/',
        name: 'Chat',
        component: ChatView
    },
    {
        path: '/admin',
        name: 'Admin',
        component: AdminView
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;