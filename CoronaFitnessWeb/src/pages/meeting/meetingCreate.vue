<template>
    <FxMeetingCreate />
</template>

<script>
    import FxMeetingCreate from "../../components/meeting/FxMeetingCreate.vue";
    import {mapGetters} from 'vuex';
    
    export default {
        components: {FxMeetingCreate},
        computed: mapGetters(['currentUser']),
        mounted() {
            if (!this.currentUser.canCreateMeetings) {
                this.$router.push('/');
            }
        }
    }
</script>