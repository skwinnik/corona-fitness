import http from './http.js'

const serviceUrl = window.$API_URL + 'meetings/';

export default {
    async getMeetings() {
        const response = await http.get(serviceUrl);
        return response.json();
    },
    
    async getMeetingById(id) {
        const response = await http.get(serviceUrl + `${id}`);
        return response.json();
    },

    async getAttendees(meetingId) {
        const response = await http.get(serviceUrl + `${meetingId}/attendees`);
        return response.json();
    },

    async getRequestsToAttend(meetingId) {
        const response = await http.get(serviceUrl + `${meetingId}/attendee_requests`);
        return response.json();
    },
    
    async createMeeting(meeting) {
        return http.put(serviceUrl, meeting);
    },
    
    async saveMeeting(meetingId, meeting) {
        return http.post(serviceUrl + `${meetingId}`, meeting);
    },

    async getToken(meetingId) {
        const response = await http.get(serviceUrl + `${meetingId}/token`);
        return response.text();
    },

    async requestToAttend(meetingId) {
        const response = await http.put(serviceUrl + `${meetingId}/attendee_requests`);
        return response.ok;
    },

    async rejectRequestToAttend(meetingId, userId) {
        const response = await http.post(serviceUrl + `${meetingId}/attendee_requests/${userId}/reject`);
        return response.ok;
    },

    async approveRequestToAttend(meetingId, userId) {
        const response = await http.post(serviceUrl + `${meetingId}/attendee_requests/${userId}/approve`);
        return response.ok;
    },
    
    async removeAttendee(meetingId, userId) {
        const response = await http.delete(serviceUrl + `${meetingId}/attendees/${userId}`)
        return response.ok;
    },
    
    async archive(meetingId) {
        return http.post(serviceUrl + `${meetingId}/archive`);
    }
}