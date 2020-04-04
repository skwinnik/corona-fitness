<template>
    <div :id="streamManager.stream.connection.connectionId">
        <video class="fx-video"></video>
    </div>
</template>

<script>
    export default {
        props: {
            streamManager: Object
        },
        
        mounted() {
            this.streamManager.addVideoElement(this.$el.querySelector('video'));
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

