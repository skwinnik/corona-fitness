<template>
    <div :id="streamManager.stream.connection.connectionId" @click="onVideoClick">
        <video ref="videoElement" class="fx-video"/>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');
    
    export default {
        props: {
            streamManager: Object
        },
        
        methods: {
            onVideoClick() {
                this.setFocusedConnectionId(this.streamManager.stream.connection.connectionId);
            },
            ...mapActions(['setFocusedConnectionId'])
        },
        
        mounted() {
            this.streamManager.addVideoElement(this.$refs.videoElement);
        }
    }
</script>

<style lang="scss" scoped>
    .fx-video {
        width: 100%;
        border: 5px solid white;
        border-radius: 2px;
        transition: border-color 0.2s;
        
        &:hover {
            border-color: lightgreen;        
        }
    }
</style>

