<template>
    <form @submit="saveMeeting($event)">
        <div class="form-group">
            <label for="title">Название</label>
            <input class="form-control" id="title" name="title" type="text" v-model="title" required/>
        </div>
        <div class="form-group">
            <label for="description">Описание</label>
            <textarea class="form-control" id="description" name="description" v-model="description" required/>
        </div>
        <div class="form-group">
            <button type="submit" class="btn btn-primary">
                Сохранить
            </button>
            <router-link class="btn btn-secondary" to="/meetings/">Отмена</router-link>
        </div>
    </form>
</template>

<script>
    import {mapGetters} from 'vuex';

    export default {
        computed: mapGetters(["currentUser"]),
        data: function () {
            return {
                title: '',
                description: ''
            };
        },
        methods: {
            async saveMeeting($event) {
                $event.preventDefault();
                await this.$store.dispatch('saveMeeting', {title: this.title, description: this.description});
                await this.$router.push('/');
            }
        }
    }
</script>