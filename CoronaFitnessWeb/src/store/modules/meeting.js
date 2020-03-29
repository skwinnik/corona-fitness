import meetingService from "../../api/meetingService.js";

export default {
    actions: {
        async loadMeetings(ctx) {
            const response = await meetingService.getMeetings();
            ctx.commit('updateMeetingsList', response);
        },
    },
    mutations: {
        updateMeetingsList(state, data) {
            state.meetings = data;
        }
    },
    state: {
        meetings: []
    },
    getters: {
        getMeetings(state) {
            return state.meetings;
        }
    }
}