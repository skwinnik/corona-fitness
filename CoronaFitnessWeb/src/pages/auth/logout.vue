<template>
    <div>
        <FxLogout/>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import FxLogout from '../../components/auth/FxLogout.vue';

    export default {
        components: {FxLogout},
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