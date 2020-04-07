import meetingList from './meetingList.js'
import meetingView from './meetingView.js'
import meetingConference from './meetingConference.js'
import meetingManage from "./meetingManage.js";

export default {
    namespaced: true,
    modules: {
        list: meetingList,
        view: meetingView,
        manage: meetingManage,
        conference: meetingConference
    }
}