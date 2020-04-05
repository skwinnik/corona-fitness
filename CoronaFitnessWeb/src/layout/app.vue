<template>
    <div>
        <section id="header" class="header">
            <div class="container">
                <div class="header__title">
                    <router-link to="/" tag="span">Corona Fitness</router-link>
                </div>
                <div class="header__logout">
                    <FxLogoutButton/>
                </div>
            </div>
        </section>
        <section id="body">
            <div class="container">
                <span v-if="isLoading">LOADING</span>
                <router-view v-if="!isLoading"/>
            </div>
        </section>
        <section id="footer">

        </section>
    </div>
</template>

<script>
    import {mapGetters, mapActions} from 'vuex';

    export default {
        computed: mapGetters(['isLoggedIn', 'currentUser']),
        methods: mapActions(['loadCurrentUser']),
        data: function () {
            return {
                isLoading: true
            }
        },

        async created() {
            await this.loadCurrentUser();
            this.isLoading = false;
        },

        mounted() {
            if (!this.isLoggedIn) {
                this.$router.push('/auth');
            }
        }
    }
</script>

<style lang="scss">
    @import '../styles/design.scss';

    .header {
        height: 50px;
        background: $color-gray-light;
        margin-bottom: 20px;
        
        &__title {
            cursor: pointer;
        }

        .container {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }
    }
</style>