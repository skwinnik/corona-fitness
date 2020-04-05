<template>
    <div>
        <FxMeetingView :meeting-id="meetingId" />
    </div>
</template>

<script>
    import FxMeetingView from "../../components/meeting/FxMeetingView.vue";
    import {createNamespacedHelpers} from 'vuex';
    const {mapActions} = createNamespacedHelpers('meetings/view');
    
    export default {
        components: {FxMeetingView},
        methods: mapActions(['clearCurrentMeeting']),
        data: function () {
            return {
                meetingId: this.$route.params.id
            };
        },
        
        watch: {
            $route(to, from) {
                this.meetingId = to.params.id;
            }
        },
        
        beforeRouteLeave(to, from, next) {
            this.clearCurrentMeeting();
            next();
        }
    }
</script>

<style lang="scss">

</style>