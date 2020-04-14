<template>
    <div v-if="publisher && publisher.stream"
         class="fx-conference"
         :class="{'fx-conference_speaker-view': focusedConnectionId !== null, 'fx-conference_grid-view': focusedConnectionId === null,
         'fx-conference_fullscreen': isFullscreen}">
        <div class="text-center fx-conference__controls fx-conference__controls_top">
            <FxMeetingConferenceLayoutControls/>
        </div>

        <div class="fx-conference__speaker-container" v-if="speakerStreamManager">
            <FxVideo :stream-manager="speakerStreamManager"
                     :connection-id="speakerStreamManager.stream.connection.connectionId"
                     class="fx-conference__item"/>
        </div>
        <div class="fx-conference__swipe-container">
            <FxVideo v-for="streamManager in streamManagers"
                     :key="streamManager.stream.connection.connectionId"
                     :stream-manager="streamManager"
                     :connection-id="streamManager.stream.connection.connectionId"
                     class="fx-conference__item"/>
        </div>

        <div class="text-center fx-conference__controls fx-conference__controls_bottom">
            <FxMeetingConferencePublisherControls/>
        </div>
    </div>
</template>

<script>
    import FxVideo from "./FxVideo.vue";
    import {createNamespacedHelpers} from 'vuex';
    import FxMeetingConferenceLayoutControls from "./FxMeetingConferenceLayoutControls.vue";
    import FxMeetingConferencePublisherControls from "./FxMeetingConferencePublisherControls.vue";

    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');

    export default {
        components: {FxMeetingConferencePublisherControls, FxMeetingConferenceLayoutControls, FxVideo},
        props: {
            token: String
        },

        computed: {
            streamManagers() {
                const streamManagers = [this.publisher, ...this.subscribers];
                return streamManagers.filter(sm => sm.stream.connection.connectionId !== this.focusedConnectionId);
            },
            speakerStreamManager() {
                return [this.publisher, ...this.subscribers].find(sm => sm.stream.connection.connectionId === this.focusedConnectionId);  
            },
            ...mapGetters(['ov', 'session', 'publisher', 'subscribers', 'focusedConnectionId', 'videosCount', 'isFullscreen'])
        },

        methods: {
            ...mapActions(['initOv', 'initSession', 'initPublisher', 'addSubscriber', 'removeSubscriber'])
        },

        mounted() {
            this.initOv();
            this.initSession(this.ov);

            this.session.connect(this.token).then(() => {
                this.initPublisher(this.ov);
                this.session.publish(this.publisher);
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
    $speaker-view-swipe-container-height: 120px;
    $controls-height: 50px;
    $controls-margin: 30px;
    
    .fx-conference {
        overflow: hidden;
        width: 100%;
        height: 800px;
        
        &.fx-conference_fullscreen {
            position: absolute;
            left: 0;
            top: 0;
            height: 100vh;
            padding: 20px 0;
            background: #f2f2f2;
        }
        
        &.fx-conference_speaker-view {
            .fx-conference__speaker-container {
                display: flex;
                height: calc(100%
                - #{$speaker-view-swipe-container-height} 
                - #{2 * $controls-height} 
                - #{2 * $controls-margin});    
            }
            
            .fx-conference__swipe-container {
                height: $speaker-view-swipe-container-height;
            }
        }
        
        &__controls {
            flex: 0 0 100%;
            height: $controls-height;

            &_top {
                margin-bottom: $controls-margin;
            }

            &_bottom {
                margin-top: $controls-margin;
            }
        }
        
        &__speaker-container {
            
        }
        
        &__swipe-container {
            display: flex;
        }

        &__item {
            flex: 1 0 200px;
        }
    }
</style>