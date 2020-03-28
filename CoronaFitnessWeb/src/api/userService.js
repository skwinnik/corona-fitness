import http from './http.js'
const serviceUrl = window.$API_URL + 'users/';

export default {
    get() {
        return http.get(serviceUrl + 'get');
    }
}