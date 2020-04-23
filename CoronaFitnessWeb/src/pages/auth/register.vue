<template>
    <div>
        <FxRegister/>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import FxRegister from '../../components/auth/FxRegister.vue';

    export default {
        components: {FxRegister},
        computed: mapGetters(['isLoggedIn']),

        mounted() {
            if (this.isLoggedIn)
                this.$router.push('/');
        },

        watch: {
            isLoggedIn: function (val) {
                if (val)
                    this.$router.push('/');
            }
        },
    }
</script>