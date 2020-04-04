import meetingList from './meetingList.js'
import meetingView from './meetingView.js'

export default {
    namespaced: true,
    modules: {
        list: meetingList,
        view: meetingView
    }
}