<template>
    <MeetingConference v-if="currentToken" :token="currentToken"/>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    import MeetingConference from "../../components/meeting/conference/MeetingConference.vue";

    const {mapActions, mapGetters} = createNamespacedHelpers('meetings/conference');

    export default {
        components: {MeetingConference},
        computed: mapGetters(['currentToken']),
        methods: {
            confirm() {
                return confirm("Вы уверены, что хотите покинуть конференцию?");
            },

            ...mapActions(['loadToken', 'cleanToken'])
        },
        created() {
            this.cleanToken();
            window.onbeforeunload = () => {
                let result = this.confirm();
                if (!result) {
                    this.cleanToken();
                }
                
                return result;
            };
        },
        mounted() {
            this.loadToken(this.$route.params.id);
        },

        beforeRouteLeave(to, from, next) {
            if (!this.confirm()) {
                next(from);
                return;
            }
            
            window.onbeforeunload = null;
            this.cleanToken();
            next();
        }
    }
</script>