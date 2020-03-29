import meetingService from "../../api/meetingService.js";

export default {
    actions: {
        async loadMeetings(ctx) {
            const response = await meetingService.getMeetings();
            ctx.commit('updateMeetingsList', response);
        },
        
        async saveMeeting(ctx, meeting) {
            await meetingService.saveMeeting(meeting);
            await ctx.dispatch('loadMeetings');
        }
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
        meetings(state) {
            return state.meetings;
        }
    }
}