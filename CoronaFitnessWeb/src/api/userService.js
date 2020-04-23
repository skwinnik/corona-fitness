import http from './http_old.js'
const serviceUrl = window.$API_URL + 'users/';

export default {
    getCurrentUser() {
        return http.get(serviceUrl + 'getCurrentUser');
    }
}