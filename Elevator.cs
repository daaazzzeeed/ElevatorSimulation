namespace ElevatorSimulation
{
    public class Elevator
    {
        public int ID { get; set; }
        public int CurrentFloor { get; set; }
        public int TargetFloor { get; set; }
        public bool IsBusy { get; set; }
        public int GroupID { get; set; }

        public Elevator(int StartFloor, int groupID)
        {
            CurrentFloor = StartFloor;
            GroupID = groupID;
        }

        void MoveToNext(int direction)
        {
            // not implemented yet
        }

    }
}