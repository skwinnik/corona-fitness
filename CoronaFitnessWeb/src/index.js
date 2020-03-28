import Vue from 'vue'
import router from './router'
import store from './store'
import Login from './components/login/FxLogin.vue'
import Users from './components/users/users.vue'

Vue.component('FxLogin', Login);
Vue.component('FxUsers', Users);

const app = new Vue({
    router,
    store,
    el: '#app',
    template: '<router-view />'
});