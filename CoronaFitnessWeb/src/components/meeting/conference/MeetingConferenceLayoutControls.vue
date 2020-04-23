<template>
    <div>
        <button class="btn btn-secondary" @click="onFullscreenClick()">
            <BIconFullscreen/>
        </button>
        <button class="btn btn-secondary" @click="onGridLayoutClick()">
            <BIconGrid3x2Gap/>
        </button>
        <button class="btn btn-secondary" @click="onPersonLayoutClick()">
            <BIconPersonFill/>
        </button>
    </div>
</template>

<script>
    import {createNamespacedHelpers} from 'vuex';

    const {mapGetters, mapActions} = createNamespacedHelpers('meetings/conferenceUi');

    export default {
        computed: {
            ...mapGetters(['publisher', 'isFullscreen'])
        },
        methods: {
            onPersonLayoutClick() {
                this.setFocusedConnectionId(this.publisher.stream.connection.connectionId);
            },
            onGridLayoutClick() {
                this.setFocusedConnectionId(null);
            },
            onFullscreenClick() {
                this.toggleFullscreen(!this.isFullscreen);
            },
            ...mapActions(['setFocusedConnectionId', 'toggleFullscreen'])
        }
    }
</script>

<style>

</style>