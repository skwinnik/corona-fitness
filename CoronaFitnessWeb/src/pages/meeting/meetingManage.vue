﻿<template>
    <div>
        <div class="form-group">
            <label for="copy">Ссылка для вступления</label>
            <div class="input-group">
                <input ref="copyElement" id="copy" name="copy" class="form-control" type="text" readonly
                       v-model="viewUrl"/>
                <div class="input-group-append">
                    <button class="btn btn-secondary" @click="copyViewUrl()">
                        <BIconClipboard/>
                    </button>
                </div>
            </div>
        </div>
        <div class="form-group">
            <button class="btn btn-secondary" @click="onArchive(meetingId)">В Архив</button>
        </div>
        <BTabs>
            <BTab title="Участники">
                <MeetingAttendees v-if="attendees && attendees.length > 0" :meeting-id="meetingId"
                                    :attendees="attendees"/>
                <div class="text-muted text-center mt-3" v-if="!attendees || attendees.length === 0">
                    Участников нет
                </div>
            </BTab>
            <BTab title="Запросы">
                <MeetingRequests v-if="attendeeRequests && attendeeRequests.length > 0" :meeting-id="meetingId"
                                   :requests="attendeeRequests"/>
                <div class="text-muted text-center mt-3" v-if="!attendeeRequests || attendeeRequests.length === 0">
                    Запросов нет
                </div>
            </BTab>

            <BTab title="Редактировать" v-if="currentMeeting">
                <MeetingEditor :meeting="currentMeeting" @saveMeeting="onSaveMeeting"/>
            </BTab>
        </BTabs>
    </div>

</template>

<script>
    import MeetingEditor from "../../components/meeting/MeetingEditor.vue";
    import MeetingRequests from "../../components/meeting/manage/MeetingRequests.vue";
    import MeetingAttendees from "../../components/meeting/manage/MeetingAttendees.vue";

    import {createNamespacedHelpers} from 'vuex';

    const {mapActions, mapGetters} = createNamespacedHelpers('meetings/manage');

    export default {
        components: {MeetingAttendees, MeetingRequests, MeetingEditor},
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
                this.loadAttendees(this.meetingId);
            }
        },
        computed: {
            viewUrl: function () {
                return window.location.protocol + '//' + window.location.host + '/#/meetings/view/' + this.meetingId;
            },
            ...mapGetters(['currentMeeting', 'attendeeRequests', 'attendees'])
        },
        methods: {
            copyViewUrl() {
                this.$refs.copyElement.select();
                document.execCommand('copy');
                this.$toastr.s('Скопировано');
            },

            async onSaveMeeting(meeting) {
                try {
                    await this.saveMeeting(meeting);
                    this.$toastr.s('Готово');
                } catch (e) {
                    this.$toastr.e('Ошибка!');
                }
            },

            async onArchive(meetingId) {
                try {
                    await this.archive(meetingId);
                    this.$toastr.s('Готово!');
                } catch (e) {
                    this.$toastr.e('Ошибка!');
                }
            },

            ...mapActions(['loadMeeting', 'loadRequests', 'loadAttendees', 'clearCurrentMeeting', 'archive', 'saveMeeting'])
        },
        mounted() {
            this.loadRequests(this.meetingId);
            this.loadMeeting(this.meetingId);
            this.loadAttendees(this.meetingId);
        },
        beforeRouteLeave(to, from, next) {
            this.clearCurrentMeeting();
            next();
        }
    }
</script>

<style lang="scss">

</style>