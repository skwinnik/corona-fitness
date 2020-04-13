import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        setSession(ctx, session) {
            ctx.commit('setSession', session);
        },

        setPublisher(ctx, publisher) {
            ctx.commit('setPublisher', publisher);
        },

        addSubscriber(ctx, subscriber) {
            ctx.commit('addSubscriber', subscriber);
        },
        
        removeSubscriber(ctx, id) {
            ctx.commit('removeSubscriber', id);
        },
        
        setFocusedConnectionId(ctx, id) {
            ctx.commit('setFocusedConnectionId', id)
        }
    },
    mutations: {
        setSession(state, session) {
            state.session = session;
        },

        setPublisher(state, publisher) {
            state.publisher = publisher;
        },

        addSubscriber(state, subscriber) {
            state.subscribers.push(subscriber);
        },

        removeSubscriber(state, connectionId) {
            let inx = state.subscribers.findIndex(x => x.stream.connection.connectionId === connectionId);
            if (inx > -1)
                state.subscribers.splice(inx, 1);
        },
        
        setFocusedConnectionId(state, id) {
            state.focusedConnectionId = id;
        }
    },
    state: {
        publisher: null,
        subscribers: [],
        focusedConnectionId: null,
        session: null
    },
    getters: {
        publisher(state) {
            return state.publisher;
        },

        subscribers(state) {
            return state.subscribers;
        },

        session(state) {
            return state.session;
        },

        videosCount(state) {
            return state.subscribers.length + 1; //(publisher)
        },

        focusedConnectionId(state) {
            return state.focusedConnectionId;
        }
    }
}