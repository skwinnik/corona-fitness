<template>
    <div class="meeting-list-item">
        <h3 class="meeting-list-item__title">
            {{meeting.title}}
            <router-link :to="{ path: '/meetings/view/' + meeting.id }" class="btn btn-outline-primary">Перейти</router-link>
        </h3>
        <div class="meeting-list-item__description">
            {{meeting.description}}
        </div>
        <hr />
        <div class="meeting-list-item__date">
            {{meeting.startTime | moment('DD MMMM, YYYY')}}
        </div>

        <div class="meeting-list-item__time">
            {{meeting.startTime | moment('HH:mm')}}
        </div>

        <div class="meeting-list-item__duration">
            {{meeting.duration}} мин.
        </div>
        
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
        
        &__title {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }
        
        &__date {
            text-transform: capitalize;
        }
    }
</style>