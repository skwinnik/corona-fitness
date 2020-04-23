<template>
    <div>
        <div class="row justify-content-between">
            <div class="col-auto">
                <h1>Запланированные встречи</h1>
            </div>
            <div class="col-auto" v-if="currentUser.canCreateMeetings">
                <router-link to="/meetings/create" class="btn btn-primary">
                    Создать <b-icon icon="plus" />
                </router-link>
            </div>
        </div>
        <br />
        <MeetingList />
    </div>
</template>

<script>
    import MeetingList from '../../components/meeting/MeetingList.vue'
    import {mapGetters} from 'vuex';
    
    export default {
        components: {
            MeetingList
        },
        computed: mapGetters(["currentUser"])
    }
</script>