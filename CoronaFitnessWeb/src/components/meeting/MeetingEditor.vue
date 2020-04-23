<template>
    <form @submit="saveMeetingForm($event)">
        <div class="form-group">
            <label for="title">Название</label>
            <input class="form-control" id="title" name="title" type="text" v-model="title" required/>
        </div>
        <div class="form-group">
            <label for="description">Описание</label>
            <textarea class="form-control" id="description" name="description" v-model="description" required/>
        </div>
        <div class="form-group row">
            <div class="form-group col">
                <label for="startTimeDate">День</label>
                <BFormDatepicker class="form-control" v-model="dateEditor.model"
                                 @input="updateTimeModel()"
                                 :min="dateEditor.minDate"
                                 :state="validateStartTime()"
                                 name="startTimeDate" id="startTimeDate" locale="ru"/>
                <BFormInvalidFeedback :state="validateStartTime()">
                    Дата и время должны быть не раньше текущего момента
                </BFormInvalidFeedback>
            </div>
            <div class="form-group col">
                <label for="startTimeTime">Время</label>
                <BFormTimepicker class="form-control" v-model="timeEditor.model"
                                 :hour12="false"
                                 @input="updateTimeModel()"
                                 :state="validateStartTime()"
                                 name="startTimeTime" id="startTimeTime" locale="ru"/>
                <BFormInvalidFeedback :state="validateStartTime()">
                    Дата и время должны быть не раньше текущего момента
                </BFormInvalidFeedback>
            </div>
            <div class="form-group col">
                <label for="duration">Длительность (мин.)</label>
                <input class="form-control"
                       min="30"
                       max="120"
                       required
                       type="number"
                       id="duration"
                       name="duration" v-model="duration"/>
            </div>
        </div>
        
        <div class="form-check">
            <input class="form-check-input" id="isPublic" name="isPublic" v-model="isPublic" disabled type="checkbox">
            <label class="form-check-label" for="isPublic">Публичное выступление</label>
            <br>
            <small>Публичные выступления видимы всем и все могут запросить разрешение на участие. Вы можете решить кому позволить участие.</small>
            <br>
            <small>Приватные выступления пока недоступны</small>
        </div>

        <div class="form-group text-right">
            <button type="submit" class="btn btn-primary">
                Сохранить
            </button>
            <router-link class="btn btn-secondary" to="/meetings/">Отмена</router-link>
        </div>
    </form>
</template>

<script>
    import moment from 'moment';
    import {BFormDatepicker} from 'bootstrap-vue'

    export default {
        components: {
            BFormDatepicker
        },
        props: {
            meeting: Object
        },
        data: function () {
            return {
                id: this.meeting.id,
                title: this.meeting.title,
                description: this.meeting.description,
                startTime: this.meeting.startTime,
                duration: this.meeting.duration,
                isPublic: this.meeting.isPublic,
                dateEditor: {
                    model: moment(this.meeting.startTime).format('YYYY-MM-DD'),
                    minDate: new Date()
                },

                timeEditor: {
                    model: moment(this.meeting.startTime).format('HH:mm')
                }
            };
        },
        methods: {
            validateStartTime() {
                return this.dateEditor.minDate <= this.startTime;
            },

            updateTimeModel() {
                this.dateEditor.minDate = new Date();
                this.startTime = new Date(this.dateEditor.model + ' ' + this.timeEditor.model);
            },

            async saveMeetingForm($event) {
                $event.preventDefault();

                this.updateTimeModel();
                if (!this.validateStartTime())
                    return;

                this.$emit('saveMeeting',{
                    id: this.id,
                    title: this.title,
                    description: this.description,
                    startTime: this.startTime,
                    duration: this.duration,
                    isPublic: this.isPublic
                });
            }
        },

        mounted() {
            this.updateTimeModel();
        }
    }
</script>