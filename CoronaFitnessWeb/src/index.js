import Vue from 'vue'
import router from './router'
import store from './store'

//#region bootstrap
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(BootstrapVue);
Vue.use(IconsPlugin);

//#endregion bootstrap

import Root from './root.vue'
Vue.component('Root', Root);

import Login from './components/login/FxLogin.vue'
Vue.component('FxLogin', Login);

import Register from './components/login/FxRegister.vue'
Vue.component('FxRegister', Register);

import Users from './components/users/users.vue'
Vue.component('FxUsers', Users);

const app = new Vue({
    router,
    store,
    el: '#app',
    template: '<Root />'
});