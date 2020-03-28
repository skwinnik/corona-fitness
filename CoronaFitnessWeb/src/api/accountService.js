import http from './http.js'
const serviceUrl = window.$API_URL + 'account/';

export default {
    login(user) {
        return http.post(serviceUrl + 'login', user);
    },
    
    signUp(user) {
        return http.post(serviceUrl + 'signup', user);
    }
}