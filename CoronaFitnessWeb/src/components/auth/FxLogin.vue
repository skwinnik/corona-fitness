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
        
        <div class="form-group" v-if="invalidEmailOrPassword">
            <small class="text-danger">
                Invalid Email/Password
            </small>
        </div>

        <button type="submit" class="btn btn-primary">Login</button>
        &nbsp;
        <router-link to="/auth/register" class="btn btn-link">Register</router-link>
    </form>
</template>

<script>
    export default {
        props: {
            invalid: Boolean
        },
        data: function () {
            return {
                email: '',
                password: '',
                invalidEmailOrPassword: !!this.invalid
            };
        },
        
        methods: {
            async onLogin($event) {
                $event.preventDefault();
                try {
                    await this.$store.dispatch('login', {email: this.email, password: this.password});
                }
                catch (e) {
                    this.invalidEmailOrPassword = true;
                }
            }
        }

    }
</script>
