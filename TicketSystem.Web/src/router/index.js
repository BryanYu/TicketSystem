import { createWebHistory, createRouter } from "vue-router";
import TicketList from '@/components/Ticket/TicketList.vue'
import TicketCreate from '@/components/Ticket/TicketCreate.vue'
import TicketEdit from '@/components/Ticket/TicketEdit.vue'
import Login from '@/components/Login.vue'
import Home from '@/components/Home.vue'

const routes = [{
        path: "/Login",
        name: "Login",
        component: Login
    },
    {
        path: "",
        name: "Home",
        component: Home,
        children: [{
            path: "Ticket",
            component: TicketList
        }, {
            path: "/Ticket/Create",
            component: TicketCreate
        }, {
            name: "TicketEdit",
            path: "/Ticket/Edit/:id",
            component: TicketEdit
        }]
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;