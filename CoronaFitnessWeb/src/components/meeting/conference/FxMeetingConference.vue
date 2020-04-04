<template>
    <div class="fx-conference row">
        <FxVideo v-if="publisher && publisher.stream" :stream-manager="publisher" :class="gridClass"/>

        <FxVideo v-for="subscriber in subscribers"
                 :key="subscriber.stream.connection.connectionId"
                 :stream-manager="subscriber"
                 :class="gridClass"
        />
    </div>
</template>

<script>
    import {OpenVidu} from 'openvidu-browser';
    import FxVideo from "./FxVideo.vue";

    export default {
        components: {FxVideo},
        props: {
            token: String
        },
        data: function () {
            return {
                session: null,
                publisher: null,
                subscribers: null,

                gridClass: 'col-12'
            };
        },

        mounted() {
            let ov = new OpenVidu();
            this.session = ov.initSession();
            this.subscribers = [];

            this.session.connect(this.token).then(() => {
                this.publisher = ov.initPublisher();
                this.session.publish(this.publisher);
            });

            this.session.on('streamCreated', event => {
                this.subscribers.push(this.session.subscribe(event.stream));
            });

            this.session.on('streamDestroyed', event => {
                let inx = this.subscribers.findIndex(x => x.stream.connection.connectionId === event.stream.connection.connectionId);
                if (inx > -1)
                    this.subscribers.splice(inx, 1);
            });
        },

        watch: {
            subscribers: function () {
                this.gridClass = 'col-' + (12 / (this.subscribers.length + 1));
            }
        },

        beforeDestroy() {
            this.session.disconnect();
        }
    }
</script>

<style lang="scss">
    .fx-conference {
        display: flex;
        align-items: center;
    }
</style>