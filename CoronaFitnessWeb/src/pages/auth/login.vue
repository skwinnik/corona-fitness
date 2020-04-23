<template>
    <div>
        <FxLogin :invalid="invalid"/>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import FxLogin from '../../components/auth/FxLogin.vue';

    export default {
        components: {FxLogin},
        props: ['invalid'],
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