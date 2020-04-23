<template>
    <div v-if="publisher && publisher.stream"
         class="conference"
         :class="{'conference_speaker-view': focusedConnectionId !== null, 'conference_grid-view': focusedConnectionId === null,
         'conference_fullscreen': isFullscreen}">
        <div class="text-center conference__controls conference__controls_top">
            <MeetingConferenceLayoutControls/>
        </div>

        <div class="conference__speaker-container" v-if="speakerStreamManager">
            <MeetingVideo :stream-manager="speakerStreamManager"
                     :connection-id="speakerStreamManager.stream.connection.connectionId"
                     class="conference__item"/>
        </div>
        <div class="conference__swipe-container">
            <MeetingVideo v-for="streamManager in streamManagers"
                     :key="streamManager.stream.connection.connectionId"
                     :stream-manager="streamManager"
                     :connection-id="streamManager.stream.connection.connectionId"
                     class="conference__item"/>
        </div>

        <div class="text-center conference__controls conference__controls_bottom">
            <MeetingConferencePublisherControls/>
        </div>
    </div>
</template>

<script>
    import MeetingVideo from "./MeetingVideo.vue";
    import {createNamespacedHelpers} from 'vuex';
    import MeetingConferenceLayoutControls from "./MeetingConferenceLayoutControls.vue";
    import MeetingConferencePublisherControls from "./MeetingConferencePublisherControls.vue";

    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');

    export default {
        components: {MeetingConferencePublisherControls, MeetingConferenceLayoutControls, MeetingVideo},
        props: {
            token: String
        },

        computed: {
            streamManagers() {
                const streamManagers = [this.publisher, ...this.subscribers];
                return streamManagers.filter(sm => sm
                    && sm.stream
                    && sm.stream.connection
                    && sm.stream.connection.connectionId !== this.focusedConnectionId);
            },
            speakerStreamManager() {
                return [this.publisher, ...this.subscribers]
                    .find(sm => sm 
                        && sm.stream 
                        && sm.stream.connection 
                        && sm.stream.connection.connectionId === this.focusedConnectionId);  
            },
            ...mapGetters(['ov', 'session', 'publisher', 'subscribers', 'focusedConnectionId', 'videosCount', 'isFullscreen'])
        },

        methods: {
            ...mapActions(['initOv', 'initSession', 'initPublisherNoVideo', 'addSubscriber', 'removeSubscriber'])
        },

        mounted() {
            this.initOv();
            this.initSession(this.ov);

            this.session.connect(this.token).then(() => {
                this.initPublisherNoVideo(this.ov);
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
    $header-height: 70px;
    
    .conference {
        overflow: hidden;
        width: 100%;
        height: calc(100vh - #{$header-height});
        
        &.conference_speaker-view {
            position: relative;
            
            .conference__speaker-container {
                display: flex;
                height: 100%;
                
                .conference__item {
                    border-color: transparent;
                }
            }
            
            .conference__swipe-container {
                display: none;
                height: $speaker-view-swipe-container-height;
            }
            
            .conference__controls {
                position: absolute;
                width: 100%;
                z-index: 2;
                
                &_top {
                    top: 0;
                }
                
                &_bottom {
                    bottom: 0;
                }
            }
        }

        &.conference_fullscreen {
            position: absolute;
            left: 0;
            top: 0;
            height: 100vh;
            background: black;
            
            .conference__controls {
                opacity: 0.3;
                transition: opacity 0.2s;

                &:hover, &:focus, &:active {
                    opacity: 1;
                }
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
            flex-wrap: wrap;
            align-items: flex-start;
            
            height: calc(100%
            - #{2 * $controls-height}
            - #{2 * $controls-margin});
        }

        &__item {
            flex: 1 0 200px;
            border: 5px solid transparent;
            border-radius: 2px;
            transition: border-color 0.2s;
            cursor: pointer;
            
            &:hover {
                border-color: lightgreen;
            }
            
            @media (max-width: 767px) {
                flex-basis: 100px;
            }
        }
    }
</style>