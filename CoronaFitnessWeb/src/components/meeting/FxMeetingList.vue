<template>
    <div class="meeting-list">
        <FxMeetingListItem class="meeting-list__item" 
                           v-for="meeting in getMeetings" :key="meeting.id"
                           v-bind:meeting="meeting"/>
    </div>
</template>

<script>
    import FxMeetingListItem from './FxMeetingListItem.vue';
    import {mapActions, mapGetters} from 'vuex';

    export default {
        components: {
            FxMeetingListItem
        },

        computed: mapGetters(['getMeetings']),
        methods: mapActions(['loadMeetings']),

        mounted() {
            this.loadMeetings();
        }
    }
</script>

<style lang="scss" scoped>
    .meeting-list {
        &__item {
            width: 100%;
        }
    }
</style>