<template>
    <div>
        <div v-for="r in requests" :key="r.userId" class="row">
            <div class="col">
                {{r.name}}
            </div>
            <div class="col-auto">
                <button class="btn btn-primary" @click="onApprove(r.userId)">Принять</button>
            </div>
            <div class="col-auto">
                <button class="btn btn-secondary" @click="onReject(r.userId)">Отказать</button>
            </div>
        </div>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';

    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/manage');
    
    export default {
        props: {
            meetingId: String,
            requests: Array
        },
        
        methods: {
            onApprove(userId) {
                this.approve({meetingId: this.meetingId, userId});
            },
            
            onReject(userId) {
                this.reject({meetingId: this.meetingId, userId});
            },
            ...mapActions(['approve', 'reject'])
        }
    }
</script>

<style>
    
</style>