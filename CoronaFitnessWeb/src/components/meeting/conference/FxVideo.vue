<template>
    <div class="fx-video" :id="connectionId" @click="onVideoClick">
        <BIconPersonFill class="fx-video__icon" v-show="!streamManager.stream.hasVideo"/>
        <video ref="videoElement" class="fx-video__video" v-show="streamManager.stream.hasVideo"/>
        <div class="fx-video__name">
            {{userName}}
        </div>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';

    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');

    export default {
        props: {
            streamManager: Object,
            connectionId: String
        },

        computed: {
            userName() {
                return this.streamManager.stream.connection.data;
            },
            ...mapGetters(['focusedConnectionId'])
        },

        methods: {
            onVideoClick() {
                if (this.focusedConnectionId === this.connectionId) {
                    this.setFocusedConnectionId(null)
                } else {
                    this.setFocusedConnectionId(this.connectionId);
                }
            },
            ...mapActions(['setFocusedConnectionId'])
        },

        mounted() {
            this.streamManager.addVideoElement(this.$refs.videoElement);
        },

        watch: {
            connectionId() {
                this.streamManager.addVideoElement(this.$refs.videoElement);
            },
            
            streamManager() {
                this.streamManager.addVideoElement(this.$refs.videoElement);
            }
        }
    }
</script>

<style lang="scss" scoped>
    .fx-video {
        position: relative;

        &:hover {
            .fx-video__name {
                opacity: 1;
            }
        }

        &__video {
            height: 100%;
            width: 100%;
            background-color: transparent;
        }

        &__icon {
            height: 100%;
            width: 100%;
            text-align: center;
            font-size: 100px;
            background-color: #f2f2f2;
        }

        &__name {
            position: absolute;
            bottom: 50px;
            left: 0;
            width: 100%;

            background: rgba(0, 0, 0, 0.5);
            color: white;

            text-align: center;
            height: 2em;
            line-height: 2;

            opacity: 0;
            transition: opacity .2s;
        }
    }
</style>

