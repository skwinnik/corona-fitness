import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        async loadMeeting(ctx, meetingId) {
            ctx.commit('updateCurrentMeeting', await meetingService.getMeetingById(meetingId));
        },
        
        async clearCurrentMeeting(ctx) {
            ctx.commit('updateCurrentMeeting', null);
        }
    },
    mutations: {
        updateCurrentMeeting(state, data) {
            state.currentMeeting = data;
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