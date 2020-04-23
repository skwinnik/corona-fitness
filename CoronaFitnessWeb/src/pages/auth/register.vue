<template>
    <div>
        <RegisterComponent/>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import RegisterComponent from '../../components/auth/RegisterComponent.vue';

    export default {
        components: {RegisterComponent},
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