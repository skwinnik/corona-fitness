import http from './http.js'

const serviceUrl = window.$API_URL + 'meeting/';

export default {
    getMeetings() {
        return http.get(serviceUrl + 'getMeetings');
    },
    
    getMeetingById(id) {
        return http.get(serviceUrl + 'getMeetingById', {id});
    },
    
    saveMeeting(meeting) {
        return http.post(serviceUrl + 'saveMeeting', meeting);
    },
    
    getToken(meetingId) {
        return http.get(serviceUrl + 'getToken', {meetingId});
    }
}