<template>
    <div v-if="currentMeeting">
        <FxMeetingListItem :meeting="currentMeeting" />
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    import FxMeetingListItem from "./FxMeetingListItem.vue";
    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/view');

    export default {
        components: {FxMeetingListItem},
        props: {
            meetingId: String
        },
        computed: mapGetters(['currentMeeting']),
        methods: mapActions(['loadMeeting']),

        mounted() {
            this.loadMeeting(this.meetingId);
        },

        watch: {
            meetingId: function () {
                this.loadMeeting(this.meetingId);
            }
        }
    }
</script>