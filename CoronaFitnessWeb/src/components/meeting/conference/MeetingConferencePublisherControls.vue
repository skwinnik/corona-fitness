<template>
    <div v-if="publisher && publisher.stream">
        <button class="btn btn-secondary btn-toggle" :class="{'btn-toggle_disabled': !this.audioActive}" @click="onToggleMic()">
            <BIconMic />
        </button>
        <button class="btn btn-secondary btn-toggle" :class="{'btn-toggle_disabled': !this.videoActive}" @click="onToggleVideo()">
            <BIconCameraVideo />
        </button>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';
    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');

    export default {
        computed: {
            videoActive() {
                return this.publisher.stream.videoActive;
            },
            audioActive() {
                return this.publisher.stream.audioActive;
            },
            
            ...mapGetters(['publisher'])
        },
        methods: {
            onToggleVideo(){
                this.toggleVideo(!this.videoActive);
            },
            onToggleMic(){
                this.toggleMic(!this.audioActive);
            },
            ...mapActions(['toggleVideo', 'toggleMic'])
        }
    }
</script>

<style lang="scss">
    .btn-toggle {
        width: 50px;
        height: 50px;
        
        &.btn-toggle_disabled {
            position: relative;

            &:after {
                content: '';
                width: 1px;
                height: 200%;
                background: white;
                position: absolute;
                right: 0;
                top: 0;
                transform: rotate(45deg);
                transform-origin: top right;
            }
        }
    }
</style>