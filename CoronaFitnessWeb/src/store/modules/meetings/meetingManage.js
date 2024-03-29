﻿import meetingService from "../../../api/meetingService.js";

export default {
    namespaced: true,
    actions: {
        async loadMeeting(ctx, meetingId) {
            ctx.commit('updateCurrentMeeting', await meetingService.getMeetingById(meetingId));
        },

        async clearCurrentMeeting(ctx) {
            ctx.commit('updateCurrentMeeting', null);
        },

        async loadAttendees(ctx, meetingId) {
            const response = await meetingService.getAttendees(meetingId);
            ctx.commit('updateAttendees', response);
        },
        
        async loadRequests(ctx, meetingId) {
            const response = await meetingService.getRequestsToAttend(meetingId);
            ctx.commit('updateAttendeeRequests', response);
        },
        
        async approve(ctx, data) {
            await meetingService.approveRequestToAttend(data.meetingId, data.userId);
            ctx.commit('requestApproved', {userId: data.userId, result: true});
        },
        
        async reject(ctx, data) {
            await meetingService.rejectRequestToAttend(data.meetingId, data.userId);
            ctx.commit('requestRejected', {userId: data.userId, result: true});
        },
        
        async removeAttendee(ctx, data) {
            await meetingService.removeAttendee(data.meetingId, data.userId);
            ctx.commit('attendeeRemoved', data.userId);
        },
        
        async archive(ctx, meetingId) {
            await meetingService.archive(meetingId);
            ctx.commit('archive');
        },

        async saveMeeting(ctx, meeting) {
            await meetingService.saveMeeting(meeting.id, meeting);
        },
        
        async createMeeting(ctx, meeting) {
            await meetingService.createMeeting(meeting);
        }
    },
    mutations: {
        updateAttendees(state, data) {
            state.attendees = data;
        },
        
        updateAttendeeRequests(state, data) {
            state.attendeeRequests = data;
        },

        updateCurrentMeeting(state, data) {
            state.currentMeeting = data;
        },

        requestApproved(state, data) {
            if (!data.result) return;

            let inx = state.attendeeRequests.findIndex(x => x.userId === data.userId);
            if (inx > -1)
                state.attendeeRequests.splice(inx, 1);
        },

        requestRejected(state, data) {
            if (!data.result) return;

            let inx = state.attendeeRequests.findIndex(x => x.userId === data.userId);
            if (inx > -1)
                state.attendeeRequests.splice(inx, 1);
        },
        
        attendeeRemoved(state, userId) {
            if (!state.attendees || !state.attendees.length) return;
            
            let inx = state.attendees.findIndex(x => x.userId === userId);
            if (inx > -1)
                state.attendees.splice(inx, 1);
        },
        
        archive(state) {
            state.currentMeeting.isArchived = true;
        }
    },
    state: {
        currentMeeting: null,
        attendees: null,
        attendeeRequests: null
    },
    getters: {
        currentMeeting(state) {
            return state.currentMeeting;
        },

        attendeeRequests(state) {
            return state.attendeeRequests;
        },

        attendees(state) {
            return state.attendees;
        }
    }
}