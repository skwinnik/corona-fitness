<template>
    <form @submit="onRegister($event)">
        <div class="form-group">
            <label for="name">Name</label>
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
        <router-link to="/auth/login" class="btn btn-link">Login</router-link>
    </form>
</template>

<script>
    export default {
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

        methods: {
            async onRegister($event) {
                $event.preventDefault();
                try {
                    await this.$store.dispatch('register', {email: this.email, password: this.confirmPassword, name: this.name});
                }
                catch (e) {
                    if (e.status === 401) {
                        //user with the specified email already registered
                        //but password is invalid
                        this.$router.push({ name: 'login', query: {invalid: true} })
                    }
                }
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
