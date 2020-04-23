<template>
    <div>
        <div v-for="a in attendees" :key="a.userId" class="row">
            <div class="col">
                {{a.name}}
            </div>
            <div class="col-auto">
                <button class="btn btn-secondary" @click="onRemove(a.userId)">Удалить</button>
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
            attendees: Array
        },
        
        methods: {
            async onRemove(userId) {
                try {
                    await this.removeAttendee({meetingId: this.meetingId, userId: userId});
                    this.$toastr.s('Готово!');                    
                }
                catch (e) {
                    this.$toastr.e('Ошибка!');
                }
            },
            ...mapActions(['removeAttendee'])
        }
    }
</script>