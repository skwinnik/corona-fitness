import accountService from "../../api/accountService.js";

export default {
    actions: {
        async login(ctx, user) {
            await accountService.login(user);
            ctx.commit('updateLoginState', true);
        },

        async logout(ctx) {
            await accountService.logout();
            ctx.commit('updateLoginState', false);
        },

        async register(ctx, user) {
            await accountService.signUp(user);
            ctx.commit('updateLoginState', true);
        },
    },
    mutations: {
        updateLoginState(state, success) {
            state.isLoggedIn = success;
        }
    },
    state: {
        isLoggedIn: window.$IS_LOGGED_IN === true || window.$IS_LOGGED_IN === 'True' || window.$IS_LOGGED_IN === 'true'
    },
    getters: {
        isLoggedIn(state) {
            return state.isLoggedIn;
        }
    }
}