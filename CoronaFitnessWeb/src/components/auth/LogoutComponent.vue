<template>
    <form @submit="logout($event)">
        <div class="form-group">
            Вы уверены, что хотите выйти?
        </div>
        <div class="form-group">
            <button type="submit" class="btn btn-link">Да</button>
            <router-link to="/" class="btn btn-primary">Нет</router-link>
        </div>
    </form>
</template>

<script>
    export default {
        methods: {
            logout($event) {
                $event.preventDefault();
                this.$store.dispatch('logout');
            }
        }
    }
</script>

<style lang="scss" scoped>
    form {
        text-align: center;
    }
</style>