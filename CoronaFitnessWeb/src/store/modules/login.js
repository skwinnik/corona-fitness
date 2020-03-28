import accountService from "../../api/accountService.js";

export default {
    actions: {
        async initLoginToken(ctx) {
            
        },
        
        async login(ctx, user) {
            const response = await accountService.login(user);
            ctx.commit('updateLoginState', response.success);
        }
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
            return state.isLoggedIn;
        }
    }
}