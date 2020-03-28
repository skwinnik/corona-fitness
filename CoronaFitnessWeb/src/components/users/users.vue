<template>
    <div>
        <span v-for="user in getUsers">
            {{user.email}}
        </span>
    </div>
</template>

<script>
    import {mapActions, mapGetters} from 'vuex'

    export default {
        computed: mapGetters(['getUsers']),
        methods: mapActions(['get']),
        async mounted() {
            this.get();
        }
        
    }
</script>