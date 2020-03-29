<template>
    <div>
        <section id="header">
            <div class="container">
                <div class="header__title">
                    Corona Fitness                    
                </div>
                <div class="header__logout">
                    <FxLogoutButton />
                </div>
            </div>
        </section>
        <section id="body">
            <div class="container">
                <router-view/>
            </div>
        </section>
        <section id="footer">
            
        </section>
    </div>
</template>

<script>
    import {mapGetters} from 'vuex';

    export default {
        computed: mapGetters(['isLoggedIn']),
        mounted() {
            if (!this.isLoggedIn)
                this.$router.push('/auth');
        }
    }
</script>

<style lang="scss">
    @import '../styles/design.scss';
    
    #header {
        height: 50px;
        background: $color-gray-light;
        margin-bottom: 20px;
        
        .container {
            height: 100%;    
            display: flex;
            align-items: center;
            justify-content: space-between;
        }
    }

    #body {

    }

    #footer {

    }
</style>