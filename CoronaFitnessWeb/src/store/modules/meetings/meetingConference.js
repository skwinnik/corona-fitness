import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        async loadToken(ctx, meetingId) {
            const data = await meetingService.getToken(meetingId);
            ctx.commit('updateCurrentToken', data.token);
        },

        async cleanToken(ctx) {
            ctx.commit('updateCurrentToken', null);
        }
    },
    mutations: {
        updateCurrentToken(state, data) {
            state.currentToken = data;
        }
    },
    state: {
        currentToken: null
    },
    getters: {
        currentToken(state) {
            return state.currentToken;
        }
    }
}