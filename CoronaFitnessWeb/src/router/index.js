import Vue from 'vue'
import VueRouter from 'vue-router'
import index from '../pages/index.vue'
import login from '../pages/login.vue'
import register from '../pages/register.vue'

Vue.use(VueRouter);

const routes = [
    { path: '/', component: index },
    { path: '/login', component: login },
    { path: '/register', component: register }];

export default new VueRouter({
    routes
});