import http from './http.js'

const serviceUrl = window.$API_URL + 'account/';

export default {
    async login(user) {
        return http.post(serviceUrl + 'login', user);
    },

    async signUp(user) {
        return http.post(serviceUrl + 'signup', user);
    },

    async logout() {
        return http.post(serviceUrl + 'logout');
    }
}