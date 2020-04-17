import meetingService from "../../../api/meetingService.js";
import {OpenVidu} from 'openvidu-browser';

export default {
    namespaced: true,
    actions: {
        initOv(ctx) {
            ctx.commit('setOv', new OpenVidu());
        },

        initSession(ctx, ov) {
            ctx.commit('setSession', ov.initSession());
        },

        initPublisherNoVideo(ctx, ov) {
            const publisher = ov.initPublisher(undefined, {
                publishAudio: false,
                publishVideo: false,
                videoSource: null
            });
            ctx.getters.session.publish(publisher)
            ctx.commit('setPublisher', publisher);
        },

        initPublisherVideo(ctx, ov) {
            const publisher = ov.initPublisher(undefined, {
                publishAudio: false,
                publishVideo: true
            });
            ctx.getters.session.publish(publisher)
            ctx.commit('setPublisher', publisher);
        },
        
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
        },
        
        toggleMic(ctx, enable) {
            ctx.commit('toggleMic', enable);
        },
        
        toggleVideo(ctx, enable) {
            ctx.getters.session.unpublish(ctx.getters.publisher);
            let action = enable ? 'initPublisherVideo' : 'initPublisherNoVideo';
            ctx.dispatch(action, ctx.getters.ov);
        },
        
        toggleFullscreen(ctx, enable) {
            ctx.commit('toggleFullscreen', enable);
        }
    },
    mutations: {
        setOv(state, ov) {
            state.ov = ov;    
        },
        
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
        },

        toggleMic(state, enable) {
            state.publisher.publishAudio(enable);
        },

        toggleVideo(state, enable) {
            state.publisher.publishVideo(enable);
        },
        
        toggleFullscreen(state, enable) {
            state.isFullscreen = enable;
        }
    },
    state: {
        ov: null,
        session: null,
        publisher: null,
        subscribers: [],
        focusedConnectionId: null,
        isFullscreen: false
    },
    getters: {
        ov(state) {
            return state.ov;
        },
        session(state) {
            return state.session;
        },
        publisher(state) {
            return state.publisher;
        },

        subscribers(state) {
            return state.subscribers;
        },
        videosCount(state) {
            return state.subscribers.length + 1; //(publisher)
        },

        focusedConnectionId(state) {
            return state.focusedConnectionId;
        },
        
        isFullscreen(state) {
            return state.isFullscreen;
        }
    }
}