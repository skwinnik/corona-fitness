<template>
    <div class="text-right">
        <button class="btn btn-secondary" @click="onGridLayoutClick()">
            <BIconGrid3x2Gap />
        </button>
        <button class="btn btn-secondary" @click="onPersonLayoutClick()">
            <BIconPersonFill />
        </button>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');
    
    export default {
        computed: {
            ...mapGetters(['publisher'])
        },
        methods: {
            onPersonLayoutClick(){
                this.setFocusedConnectionId(this.publisher.stream.connection.connectionId);
            },
            onGridLayoutClick(){
                this.setFocusedConnectionId(null);
            },
            ...mapActions(['setFocusedConnectionId'])
        }
    }
</script>

<style>
    
</style>