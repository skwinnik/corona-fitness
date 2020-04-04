<template>
    <div class="meeting-list-item">
        <h3 class="meeting-list-item__title">
            {{meeting.title}}
        </h3>
        <div class="meeting-list-item__description">
            {{meeting.description}}
        </div>
        
        <router-link :to="{ path: '/meetings/view/' + meeting.id }" class="btn btn-outline-primary">Перейти</router-link>
    </div>
</template>

<script>
    export default {
        props: {
            meeting: Object
        }
    }
</script>

<style lang="scss">
    @import "../../styles/design.scss";
    
    .meeting-list-item {
        border: 1px $color-gray solid;
        padding: 15px 30px;
    }
</style>