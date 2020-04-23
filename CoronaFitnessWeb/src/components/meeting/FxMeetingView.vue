<template>
    <div v-if="currentMeeting">
        <FxMeetingListItem :meeting="currentMeeting" v-on:requestToAttend="onRequestToAttend" />
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