import meetingList from './meetingList.js'
import meetingView from './meetingView.js'
import meetingConference from './meetingConference.js'

export default {
    namespaced: true,
    modules: {
        list: meetingList,
        view: meetingView,
        conference: meetingConference
    }
}