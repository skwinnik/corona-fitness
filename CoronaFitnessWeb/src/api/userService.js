import http from './http.js'
const serviceUrl = window.$API_URL + 'users/';

export default {
    async getCurrentUser() {
        const response = await http.get(serviceUrl + 'getCurrentUser');
        return response.json();
    }
}