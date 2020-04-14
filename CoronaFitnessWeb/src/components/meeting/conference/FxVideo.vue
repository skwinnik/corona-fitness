<template>
    <div class="fx-video-container" :id="connectionId" @click="onVideoClick">
        <BIconPersonFill class="fx-video-icon" v-show="!streamManager.stream.hasVideo"/>
        <video ref="videoElement" class="fx-video" v-show="streamManager.stream.hasVideo"/>
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

        methods: {
            onVideoClick() {
                this.setFocusedConnectionId(this.connectionId);
            },
            ...mapActions(['setFocusedConnectionId'])
        },

        mounted() {
            this.streamManager.addVideoElement(this.$refs.videoElement);
        },

        watch: {
            connectionId() {
                this.streamManager.addVideoElement(this.$refs.videoElement);
            }
        }
    }
</script>

<style lang="scss" scoped>
    .fx-video {
        height: 100%;
        width: 100%;
        background-color: transparent;
    }

    .fx-video-container {
        position: relative;
        border: 5px solid transparent;
        border-radius: 2px;
        transition: border-color 0.2s;

        &:hover {
            border-color: lightgreen;
        }

        .fx-video-icon {
            height: 100%;
            width: 100%;
            text-align: center;
            font-size: 100px;
            background-color: #f2f2f2;
        }
    }
</style>

