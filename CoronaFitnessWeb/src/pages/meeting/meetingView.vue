<template>
    <div>
        <MeetingView :meeting-id="meetingId" />
    </div>
</template>

<script>
    import MeetingView from "../../components/meeting/MeetingView.vue";
    import {createNamespacedHelpers} from 'vuex';
    const {mapActions} = createNamespacedHelpers('meetings/view');
    
    export default {
        components: {MeetingView},
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