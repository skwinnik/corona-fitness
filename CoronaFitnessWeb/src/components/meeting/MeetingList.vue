﻿<template>
    <div class="meeting-list">
        <MeetingListItem class="meeting-list__item"
                           v-on:requestToAttend="onRequestToAttend"
                           v-for="meeting in meetings" :key="meeting.id"
                           v-bind:meeting="meeting"/>
    </div>
</template>

<script>
    import MeetingListItem from './MeetingListItem.vue';
    
    import { createNamespacedHelpers } from 'vuex';
    const { mapGetters, mapActions } = createNamespacedHelpers('meetings/list');

    export default {
        components: {
            MeetingListItem
        },

        computed: mapGetters(['meetings']),
        methods: {
            onRequestToAttend: function (meetingId) {
                this.requestToAttend(meetingId);
            },
            ...mapActions(['loadMeetings', 'requestToAttend'])
        },

        mounted() {
            this.loadMeetings();
        }
    }
</script>

<style lang="scss" scoped>
    .meeting-list {
        &__item {
            width: 100%;
            margin-bottom: 15px;
        }
    }
</style>