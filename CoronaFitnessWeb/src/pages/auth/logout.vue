<template>
    <div>
        <LogoutComponent/>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import LogoutComponent from '../../components/auth/LogoutComponent.vue';

    export default {
        components: {LogoutComponent},
        computed: mapGetters(['isLoggedIn']),

        mounted() {
            if (!this.isLoggedIn)
                this.$router.push('/');
        },

        watch: {
            isLoggedIn: function (val) {
                if (!val)
                    this.$router.push('/auth');
            }
        },
    }
</script>