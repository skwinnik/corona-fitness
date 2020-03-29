import http from './http.js'
const serviceUrl = window.$API_URL + 'users/';

export default {
    getCurrentUser() {
        return http.get(serviceUrl + 'getCurrentUser');
    }
}