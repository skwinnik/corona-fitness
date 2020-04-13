<template>
    <div class="fx-conference"
         :class="{'fx-conference_personal-view': focusedConnectionId !== null, 'fx-conference_grid-view': focusedConnectionId === null}">
        <FxMeetingConferenceLayoutControls class="fx-conference__controls"/>
        <FxVideo v-if="publisher && publisher.stream"
                 :stream-manager="publisher"
                 class="fx-conference__item"
                 :class="{'fx-conference__item_selected' : focusedConnectionId === publisher.stream.connection.connectionId }"/>

        <FxVideo v-for="subscriber in subscribers"
                 :key="subscriber.stream.connection.connectionId"
                 :stream-manager="subscriber"
                 class="fx-conference__item"
                 :class="{'fx-conference__item_selected' : focusedConnectionId === subscriber.stream.connection.connectionId }"/>
    </div>
</template>

<script>
    import {OpenVidu} from 'openvidu-browser';
    import FxVideo from "./FxVideo.vue";
    import {createNamespacedHelpers} from 'vuex';
    import FxMeetingConferenceLayoutControls from "./FxMeetingConferenceLayoutControls.vue";

    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');

    export default {
        components: {FxMeetingConferenceLayoutControls, FxVideo},
        props: {
            token: String
        },

        computed: {
            ...mapGetters(['session', 'publisher', 'subscribers', 'focusedConnectionId', 'videosCount'])
        },

        methods: {
            ...mapActions(['setSession', 'setPublisher', 'addSubscriber', 'removeSubscriber'])
        },

        mounted() {
            let ov = new OpenVidu();

            this.setSession(ov.initSession());
            this.session.connect(this.token).then(() => {
                const publisher = ov.initPublisher();
                this.session.publish(publisher);
                this.setPublisher(publisher);
            });

            this.session.on('streamCreated', event => {
                const subscriber = this.session.subscribe(event.stream);
                this.addSubscriber(subscriber);
            });

            this.session.on('streamDestroyed', event => {
                this.removeSubscriber(event.stream.connection.connectionId);
            });
        },

        beforeDestroy() {
            this.session.disconnect();
        }
    }
</script>

<style lang="scss">
    $grid-el-min-width: 200px;
    $personal-el-min-width: 100px;

    .fx-conference {
        $self: &;
        display: flex;
        align-items: center;
        justify-content: space-around;
        flex-wrap: wrap;

        &__controls {
            flex: 0 0 100%;
        }

        &__item {
            flex: 1 0 $grid-el-min-width;
        }

        &.fx-conference_personal-view {
            #{$self}__item {
                order: 2;
                max-height: 80vh;
                width: $personal-el-min-width;
                flex: 0 0 $personal-el-min-width;
                
                &_selected {
                    display: flex;
                    flex: 1 1 100%;
                    order: 1;
                }
            }
        }
    }
</style>