import http from './http.js'

const serviceUrl = window.$API_URL + 'meeting/';

export default {
    getMeetings() {
        return http.get(serviceUrl + 'getMeetings');
    },

    getMeetingById(id) {
        return http.get(serviceUrl + 'getMeetingById', {id});
    },

    getAttendees(meetingId) {
        return http.get(serviceUrl + 'getAttendees', {meetingId});
    },

    saveMeeting(meeting) {
        return http.post(serviceUrl + 'saveMeeting', meeting);
    },

    getToken(meetingId) {
        return http.get(serviceUrl + 'getToken', {meetingId});
    },

    requestToAttend(meetingId) {
        return http.post(serviceUrl + 'requestToAttend', {meetingId});
    },

    getRequestsToAttend(meetingId) {
        return http.get(serviceUrl + 'getRequestsToAttend', {meetingId});
    },

    rejectRequestToAttend(meetingId, userId) {
        return http.post(serviceUrl + 'rejectRequestToAttend', {meetingId, userId});
    },

    approveRequestToAttend(meetingId, userId) {
        return http.post(serviceUrl + 'approveRequestToAttend', {meetingId, userId});
    },
    
    removeAttendee(meetingId, userId) {
        return http.post(serviceUrl + 'removeAttendee', {meetingId, userId})
    }
}