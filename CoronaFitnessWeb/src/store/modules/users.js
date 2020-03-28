import userService from "../../api/userService";

export default {
    actions: {
        async get(ctx) {
            const response = await userService.get();
            ctx.commit('updateUsers', response);
        }
    },
    mutations: {
        updateUsers(state, users) {
            state.users = users;
        }
    },
    state: {
        users: []
    },
    getters: {
        getUsers(state) {
            return state.users;
        }
    }
}