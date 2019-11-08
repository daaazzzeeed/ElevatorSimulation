namespace ElevatorSimulation
{
    public class Session
    {
        public int StartTime { get; set; }
        public int? EndTime { get; set; }

        public int? TripTime { get; set; }
        public int FromGroup { get; set; }
        public int FromFloor { get; set; }

        public int? TargetFloor { get; set; }
        public Session(int startTime, int fromFloor, int fromGroup, int targetFloor)
        {
            StartTime = startTime;
            FromFloor = fromFloor;
            FromGroup = fromGroup;
            TargetFloor = targetFloor;
        }
    }
}