<template>
    <div>
        <FxLogin />
    </div>
</template>

<script>
    import {mapGetters} from "vuex";

    export default {
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