<template>
    <FxMeetingEditor :meeting="meeting" />
</template>

<script>
    import FxMeetingEditor from "../../components/meeting/FxMeetingEditor.vue";
    import {mapGetters} from 'vuex';
    
    export default {
        components: {FxMeetingEditor},
        computed: mapGetters(['currentUser']),
        data: function(){
            return {
                meeting: {
                    title: '',
                    description: '',
                    startTime: new Date(),
                    duration: 60,
                    isPublic: true,
                }
            };
        },
        mounted() {
            if (!this.currentUser.canCreateMeetings) {
                this.$router.push('/');
            }
        }
    }
</script>