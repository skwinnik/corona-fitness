<template>
    <div>
        <div class="row justify-content-between">
            <div class="col-auto">
                <h1>Запланированные встречи</h1>
            </div>
            <div class="col-auto" v-if="currentUser.canCreateMeetings">
                <router-link to="/meetings/create/" class="btn btn-primary">
                    Создать <b-icon icon="plus" />
                </router-link>
            </div>
        </div>
        <br />
        <FxMeetingList />
    </div>
</template>

<script>
    import FxMeetingList from '../../components/meeting/FxMeetingList.vue'
    import {mapGetters} from 'vuex';
    
    export default {
        components: {
            FxMeetingList
        },
        computed: mapGetters(["currentUser"])
    }
</script>