using System;

namespace CoronaFitnessApi.Model.Meeting
{
    public class SaveMeetingRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public bool IsPublic { get; set; }
    }
}