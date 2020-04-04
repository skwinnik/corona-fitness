import Vue from 'vue'
import VueRouter from 'vue-router'

import notAllowed from '../pages/error401.vue'

import authLayout from '../layout/auth.vue'
import login from '../pages/auth/login.vue'
import logout from '../pages/auth/logout.vue'
import register from '../pages/auth/register.vue'

import app from '../layout/app.vue'

import meetingBase from '../pages/meeting/meetingBase.vue'
import meetingList from '../pages/meeting/meetingList.vue'
import meetingCreate from '../pages/meeting/meetingCreate.vue'
import meetingView from '../pages/meeting/meetingView.vue'
import meetingConference from '../pages/meeting/meetingConference.vue'

Vue.use(VueRouter);

const routes = [
    {
        path: '/', component: app, redirect: '/meetings',
        children: [
            {
                path: 'meetings', component: meetingBase,
                children: [
                    {
                        path: '', component: meetingList
                    },
                    {
                        path: 'view/:id', component: meetingView
                    },
                    {
                        path: 'conference/:id', component: meetingConference
                    },
                    {
                        path: 'create', component: meetingCreate
                    }
                ]
            },
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