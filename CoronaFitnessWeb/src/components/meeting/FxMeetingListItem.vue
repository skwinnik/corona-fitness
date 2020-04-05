<template>
    <div class="meeting-list-item">
        <h3 class="meeting-list-item__title">
            {{meeting.title}}
        </h3>
        <div class="meeting-list-item__actions">
            <router-link v-if="isAllowedToManage"
                         :to="{ path: '/meetings/manage/' + meeting.id }"
                         class="btn btn-outline-primary">
                Управление
            </router-link>

            <button v-if="isAllowedToRequestAttend && !isAttendee"
                    class="btn btn-outline-primary">
                Хочу посетить
            </button>

            <router-link v-if="isAttendee && meeting.isStarted"
                         :to="{ path: '/meetings/conference/' + meeting.id }"
                         class="btn btn-outline-primary">
                Подключиться
            </router-link>
        </div>
        <div class="meeting-list-item__description">
            {{meeting.description}}
        </div>
        <hr/>
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
    import {mapGetters} from 'vuex';

    export default {
        props: {
            meeting: Object
        },
        computed: {
            isAllowedToManage: function () {
                return this.currentUser.id === this.meeting.ownerId
            },

            isAttendee: function () {
                return !!this.meeting.attendees.find(a => a.userId === this.currentUser.id);
            },

            isAllowedToRequestAttend: function () {
                return this.meeting.isPublic;
            },

            ...mapGetters(['currentUser'])
        }
    }
</script>

<style lang="scss">
    @import "../../styles/design.scss";

    .meeting-list-item {
        display: flex;
        flex-wrap: wrap;
        border: 1px $color-gray solid;
        padding: 15px 30px;

        &__title {
            flex: 1 1 auto;
        }

        &__actions {
            flex: 0 0 auto;
        }

        &__description {
            flex: 0 0 100%;
        }

        &__date {
            flex: 0 0 100%;
            text-transform: capitalize;
        }

        &__item {
            flex: 0 0 100%;
        }

        &__duration {
            flex: 0 0 100%;
        }
    }
</style>