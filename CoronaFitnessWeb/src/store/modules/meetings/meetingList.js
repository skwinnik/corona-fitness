import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        async loadMeetings(ctx) {
            const response = await meetingService.getMeetings();
            ctx.commit('updateMeetingsList', response);
        },
        
        async requestToAttend(ctx, meetingId) {
            const response = await meetingService.requestToAttend(meetingId);
            ctx.commit('updateMeetingRequestAttendeeStatus', { meetingId, result: response });
        }
    },
    mutations: {
        updateMeetingsList(state, data) {
            state.meetings = data;
        },
        
        updateMeetingRequestAttendeeStatus(state, data) {
            state.meetings.find(m => m.id === data.meetingId).isAttendeeRequested = data.result;
        }
    },
    state: {
        meetings: []
    },
    getters: {
        meetings(state) {
            return state.meetings;
        }
    }
}