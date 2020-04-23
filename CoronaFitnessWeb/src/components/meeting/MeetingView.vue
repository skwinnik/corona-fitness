<template>
    <div v-if="currentMeeting">
        <MeetingListItem :meeting="currentMeeting" v-on:requestToAttend="onRequestToAttend" />
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    import MeetingListItem from "./MeetingListItem.vue";
    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/view');

    export default {
        components: {MeetingListItem},
        props: {
            meetingId: String
        },
        computed: mapGetters(['currentMeeting']),
        methods: {
            onRequestToAttend: function (meetingId) {
                try {
                    this.requestToAttend(meetingId);
                    this.$toastr.s('Готово!');
                }
                catch (e) {
                    this.$toastr.e('Ошибка!');
                }
            },
            ...mapActions(['loadMeeting', 'requestToAttend'])},

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