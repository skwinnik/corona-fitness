<template>
    <BTabs>
        <BTab title="Запросы">
            <FxMeetingRequests :meeting-id="meetingId" :requests="attendeeRequests"/>
        </BTab>

        <BTab title="Редактировать" v-if="currentMeeting">
            <FxMeetingEditor :meeting="currentMeeting" />
        </BTab>
    </BTabs>
</template>

<script>
    import FxMeetingEditor from "../../components/meeting/FxMeetingEditor.vue";
    import FxMeetingRequests from "../../components/meeting/manage/FxMeetingRequests.vue";

    import {createNamespacedHelpers} from 'vuex';

    const {mapActions, mapGetters} = createNamespacedHelpers('meetings/manage');

    export default {
        data: function () {
            return {
                meetingId: this.$route.params.id
            };
        },

        watch: {
            $route: function (from, to) {
                this.meetingId = to.params.id;
                this.loadRequests(this.meetingId);
                this.loadMeeting(this.meetingId);

            }
        },

        components: {FxMeetingRequests, FxMeetingEditor},
        computed: mapGetters(['currentMeeting', 'attendeeRequests']),
        methods: mapActions(['loadMeeting', 'loadRequests', 'clearCurrentMeeting']),

        mounted() {
            this.loadRequests(this.$route.params.id);
            this.loadMeeting(this.$route.params.id);
        },

        beforeRouteLeave(to, from, next) {
            this.clearCurrentMeeting();
            next();
        }
    }
</script>

<style lang="scss">

</style>