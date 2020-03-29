import accountService from "../../api/accountService.js";

export default {
    actions: {
        async login(ctx, user) {
            const response = await accountService.login(user);
            ctx.commit('updateLoginState', response.success);
        },

        async logout(ctx) {
            const response = await accountService.logout();
            ctx.commit('updateLoginState', !response.success);
        },

        async register(ctx, user) {
            const response = await accountService.signUp(user);
            ctx.commit('updateLoginState', response.success);
        },
    },
    mutations: {
        updateLoginState(state, success) {
            state.isLoggedIn = success;
        }
    },
    state: {
        isLoggedIn: window.$IS_LOGGED_IN
    },
    getters: {
        isLoggedIn(state) {
            return state.isLoggedIn === true || state.isLoggedIn === 'True' || state.isLoggedIn === 'true';
        }
    }
}