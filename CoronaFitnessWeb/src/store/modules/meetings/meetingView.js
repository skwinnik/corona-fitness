import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        async loadToken(ctx, meetingId) {
            const token = await meetingService.getToken(meetingId);
            ctx.commit('updateCurrentMeetingToken');
        },

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
        },

        updateCurrentMeetingToken(state, data) {
            if (state.currentMeeting)
                state.currentMeeting.token = data;
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