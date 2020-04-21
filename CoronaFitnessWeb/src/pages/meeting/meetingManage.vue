<template>
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
            <button class="btn btn-secondary" @click="archive(meetingId)">В Архив</button>
        </div>
        <BTabs>
            <BTab title="Участники">
                <FxMeetingAttendees v-if="attendees && attendees.length > 0" :meeting-id="meetingId"
                                    :attendees="attendees"/>
                <div class="text-muted text-center mt-3" v-if="!attendees || attendees.length === 0">
                    Участников нет
                </div>
            </BTab>
            <BTab title="Запросы">
                <FxMeetingRequests v-if="attendeeRequests && attendeeRequests.length > 0" :meeting-id="meetingId"
                                   :requests="attendeeRequests"/>
                <div class="text-muted text-center mt-3" v-if="!attendeeRequests || attendeeRequests.length === 0">
                    Запросов нет
                </div>
            </BTab>

            <BTab title="Редактировать" v-if="currentMeeting">
                <FxMeetingEditor :meeting="currentMeeting"/>
            </BTab>
        </BTabs>
    </div>

</template>

<script>
    import FxMeetingEditor from "../../components/meeting/FxMeetingEditor.vue";
    import FxMeetingRequests from "../../components/meeting/manage/FxMeetingRequests.vue";
    import FxMeetingAttendees from "../../components/meeting/manage/FxMeetingAttendees.vue";

    import {createNamespacedHelpers} from 'vuex';

    const {mapActions, mapGetters} = createNamespacedHelpers('meetings/manage');

    export default {
        components: {FxMeetingAttendees, FxMeetingRequests, FxMeetingEditor},
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
            ...mapActions(['loadMeeting', 'loadRequests', 'loadAttendees', 'clearCurrentMeeting', 'archive'])
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