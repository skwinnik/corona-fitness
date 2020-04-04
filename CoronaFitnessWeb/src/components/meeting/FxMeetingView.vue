<template>
    <div v-if="currentMeeting">
        <div class="form-group">
            <label>Заголовок</label>
            <div class="form-text">
                {{currentMeeting.title}}
            </div>
        </div>

        <div class="form-group">
            <label>Описание</label>
            <div class="form-text">
                {{currentMeeting.description}}
            </div>
        </div>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/view');

    export default {
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