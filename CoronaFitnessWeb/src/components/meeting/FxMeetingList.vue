<template>
    <div class="meeting-list">
        <FxMeetingListItem class="meeting-list__item" 
                           v-for="meeting in meetings" :key="meeting.id"
                           v-bind:meeting="meeting"/>
    </div>
</template>

<script>
    import FxMeetingListItem from './FxMeetingListItem.vue';
    
    import { createNamespacedHelpers } from 'vuex';
    const { mapGetters, mapActions } = createNamespacedHelpers('meetings/list');

    export default {
        components: {
            FxMeetingListItem
        },

        computed: mapGetters(['meetings']),
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
            margin-bottom: 15px;
        }
    }
</style>