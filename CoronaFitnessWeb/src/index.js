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

//#region moment

import moment from 'moment';
import VueMoment from 'vue-moment'
moment.locale('ru');
Vue.use(VueMoment, { moment });

//#endregion

//#region
import VueToastr from "vue-toastr";
Vue.use(VueToastr, {
    defaultTimeout: 1000,
    defaultProgressBar: false
});
//#endregion

import Root from './root.vue'
Vue.component('Root', Root);

const app = new Vue({
    router,
    store,
    el: '#app',
    template: '<Root />'
});