<template>
    <form @submit="onRegister($event)">
        <div class="form-group">
            <label for="email">Name</label>
            <input class="form-control" id="name" name="name" v-model="name" type="text" maxlength="100" required/>
        </div>

        <div class="form-group">
            <label for="email">Email</label>
            <input class="form-control" id="email" name="email" v-model="email" type="email" required/>
        </div>

        <div class="form-group">
            <label for="password">Password</label>
            <input class="form-control" id="password" name="password" v-model="password" type="password" required/>
        </div>

        <div class="form-group">
            <label for="confirm-password">Confirm Password</label>
            <input class="form-control" id="confirm-password" name="confirmPassword" v-model="confirmPassword"
                   type="password" required ref="confirmPasswordEl"/>
        </div>

        <button type="submit" class="btn btn-primary">Register</button>
        &nbsp;
        <router-link to="/login" class="btn btn-link">Login</router-link>
    </form>
</template>

<script>
    import {mapGetters} from 'vuex';

    export default {
        computed: mapGetters(['isLoggedIn']),
        data: function () {
            return {
                email: '',
                name: '',
                password: '',
                confirmPassword: ''
            };
        },

        watch: {
            password: 'checkPasswordsEquality',
            confirmPassword: 'checkPasswordsEquality'
        },

        mounted() {
            // if (this.isLoggedIn)
            //     this.$router.push('/');
        },

        methods: {
            onRegister: function ($event) {
                $event.preventDefault();
                debugger;
                this.$store.dispatch('register', {email: this.email, password: this.confirmPassword, name: this.name});
            },

            checkPasswordsEquality() {
                const { password, confirmPassword } = this;
                const { confirmPasswordEl } = this.$refs;

                if (password !== confirmPassword) {
                    confirmPasswordEl.setCustomValidity(
                        'Пароли должны совпадать',
                    );
                } else {
                    confirmPasswordEl.setCustomValidity('');
                }
            },
        }

    }
</script>

<style lang="scss">
    @import 'login-form.scss';
</style>