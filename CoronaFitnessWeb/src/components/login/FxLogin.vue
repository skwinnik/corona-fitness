<template>
    <div>
        login! {{isLoggedIn}}
        <input v-model="email" type="text" />
        <input v-model="password" type="text" />
        <button @click="onLogin()">Login</button>
    </div>
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
        
        methods: {
            onLogin: function () {
                this.$store.dispatch('login', { email: this.email, password: this.password });
            }
        }
        
    }
</script>