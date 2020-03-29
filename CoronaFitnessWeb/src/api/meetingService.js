import http from './http.js'

const serviceUrl = window.$API_URL + 'meeting/';

export default {
    getMeetings() {
        return http.get(serviceUrl + 'getMeetings');
    },
    
    saveMeeting(meeting) {
        return http.post(serviceUrl + 'saveMeeting', meeting);
    }
}