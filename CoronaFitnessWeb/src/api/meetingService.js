import http from './http.js'

const serviceUrl = window.$API_URL + 'meeting/';

export default {
    getMeetings() {
        return http.get(serviceUrl + 'getMeetings');
    },
}