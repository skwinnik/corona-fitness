import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        async loadMeeting(ctx, meetingId) {
            ctx.commit('updateCurrentMeeting', await meetingService.getMeetingById(meetingId));
        },
        
        async clearCurrentMeeting(ctx) {
            ctx.commit('updateCurrentMeeting', null);
        },

        async requestToAttend(ctx, meetingId) {
            const response = await meetingService.requestToAttend(meetingId);
            ctx.commit('updateMeetingRequestAttendeeStatus', response);
        }
    },
    mutations: {
        updateCurrentMeeting(state, data) {
            state.currentMeeting = data;
        },

        updateMeetingRequestAttendeeStatus(state, data) {
            state.currentMeeting.isAttendeeRequested = data;
        }
    },
    state: {
        currentMeeting: null
    },
    getters: {
        currentMeeting(state) {
            return state.currentMeeting;
        }
    }
}