import userService from "../../api/userService";

export default {
    actions: {
        async loadCurrentUser(ctx) {
            ctx.commit('updateCurrentUser', await userService.getCurrentUser());
        }
    },
    mutations: {
        updateCurrentUser(state, currentUser) {
            state.currentUser = currentUser;
        }
    },
    state: {
        currentUser: null
    },
    getters: {
        currentUser(state) {
            return state.currentUser;
        }
    }
}