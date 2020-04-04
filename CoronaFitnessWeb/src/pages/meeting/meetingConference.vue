<template>
    <FxMeetingConference v-if="currentToken" :token="currentToken" />
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    import FxMeetingConference from "../../components/meeting/conference/FxMeetingConference.vue";
    const {mapActions, mapGetters} = createNamespacedHelpers('meetings/conference');
    
    export default {
        components: {FxMeetingConference},
        computed: mapGetters(['currentToken']),
        methods: mapActions(['loadToken', 'cleanToken']),
        mounted() {
            this.loadToken(this.$route.params.id);
        },
        
        beforeRouteLeave() {
            this.cleanToken();
        }
    }
</script>