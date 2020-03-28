<template>
    <form @submit="onLogin($event)">
        <div class="form-group">
            <label for="email">Email</label>
            <input class="form-control" id="email" name="email" v-model="email" type="email" required/>
        </div>

        <div class="form-group">
            <label for="password">Password</label>
            <input class="form-control" id="password" name="password" v-model="password" type="password" required/>
        </div>

        <button type="submit" class="btn btn-primary">Login</button>
        &nbsp;
        <router-link to="/register" class="btn btn-link">Register</router-link>
    </form>
</template>

<script>
    import {mapGetters} from 'vuex';

    export default {
        computed: mapGetters(['isLoggedIn']),
        data: function () {
            return {
                email: '',
                password: ''
            };
        },
        
        mounted() {
            // if (this.isLoggedIn)
            //     this.$router.push('/');
        },

        methods: {
            onLogin: function ($event) {
                $event.preventDefault();
                this.$store.dispatch('login', {email: this.email, password: this.password});
            }
        }

    }
</script>

<style lang="scss">
    @import 'login-form.scss';
</style>