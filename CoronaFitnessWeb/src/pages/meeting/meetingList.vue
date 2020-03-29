<template>
    <div>
        <h1>Запланированные встречи</h1>
        <br />
        <FxMeetingList />
    </div>
</template>

<script>
    import FxMeetingList from '../../components/meeting/FxMeetingList.vue'
    export default {
        components: {
            FxMeetingList
        }
    }
</script>