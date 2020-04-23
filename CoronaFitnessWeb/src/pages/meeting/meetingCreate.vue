<template>
    <MeetingEditor :meeting="meeting" @saveMeeting="onSaveMeeting"/>
</template>

<script>
    import MeetingEditor from "../../components/meeting/MeetingEditor.vue";
    import {mapGetters, mapActions} from 'vuex';
    import moment from 'moment';

    export default {
        components: {MeetingEditor},
        computed: mapGetters(['currentUser']),
        data: function () {
            return {
                meeting: {
                    id: '',
                    title: '',
                    description: '',
                    startTime: moment().add({hour: 1}).set({minute: 0, second: 0}).toDate(),
                    duration: 60,
                    isPublic: true,
                }
            };
        },

        methods: {
            async onSaveMeeting(meeting) {
                try {
                    await this.$store.dispatch('meetings/manage/createMeeting', meeting);
                    this.$toastr.s('Готово!');
                    this.$router.push('/');
                }
                catch (e) {
                    this.$toastr.e('Ошибка!');
                }
            }
        },

        mounted() {
            if (!this.currentUser.canCreateMeetings) {
                this.$router.push('/');
            }
        }
    }
</script>