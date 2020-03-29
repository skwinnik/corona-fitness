import Vue from 'vue'
import VueRouter from 'vue-router'

import notAllowed from '../pages/error401.vue'

import authLayout from '../layout/auth.vue'
import login from '../pages/auth/login.vue'
import logout from '../pages/auth/logout.vue'
import register from '../pages/auth/register.vue'

import app from '../layout/app.vue'

import meetingList from '../pages/meeting/meetingList.vue'

Vue.use(VueRouter);

const routes = [
    {
        path: '/', component: app, redirect: '/meetings',
        children: [
            { path: 'meetings', component: meetingList }
        ]
    },
    {
        path: '/auth/', component: authLayout, redirect: '/auth/login',
        children: [
            {path: 'login', component: login},
            {path: 'logout', component: logout},
            {path: 'register', component: register}
        ]
    },
    {path: '/not-allowed/', component: notAllowed},
];

export default new VueRouter({
    routes
});