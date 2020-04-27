<template>
    <div>
        <LoginComponent :invalid="invalid"/>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import LoginComponent from '../../components/auth/LoginComponent.vue';

    export default {
        components: {LoginComponent},
        props: ['invalid', 'redirect'],
        computed: mapGetters(['isLoggedIn']),

        mounted() {
            if (this.isLoggedIn)
                this.$router.push('/');
        },

        watch: {
            isLoggedIn: function (val) {
                if (val)
                    this.$router.push(this.redirect || '/');
            }
        },
    }
</script>