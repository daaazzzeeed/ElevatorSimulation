namespace ElevatorSimulation
{
    public class Elevator
    {
        public int ID { get; set; }
        public int CurrentFloor { get; set; }
        public int? TargetFloor { get; set; }
        public int? FinalFloor { get; set; }
        private bool isBusy { get; set; }
        public int GroupID { get; set; }

        public Session TargetSession { get; set; }

        static int instances = 0;

        public Elevator(int StartFloor, int groupID)
        {
            CurrentFloor = StartFloor;
            GroupID = groupID;
            instances++;
            ID = instances;
        }

        public void MoveToTarget(int targetFloor)
        {
            // moves elevator to a one floor in a given direction
            if (FinalFloor == null)
            {
                if ( CurrentFloor < TargetFloor)
                {
                    CurrentFloor++;
                }
                else
                {
                    CurrentFloor--;
                }
            }
            else
            {
                if ( CurrentFloor < FinalFloor)
                {
                    CurrentFloor++;
                }
                else
                {
                    CurrentFloor--;
                }
            }
            
        }

        public bool IsBusy()
        {
            return isBusy;
        }

        public bool IsAtTargetFloor()
        {
            if (CurrentFloor == TargetFloor)
            {
                return true;
            }

            return false;
        }

        public void RespondToSession(Session session)
        {
            // check group equality
            if (GroupID == session.FromGroup)
            {
                TargetSession = session;
                TargetFloor = session.FromFloor;
                isBusy = true;
            }
        }

        public void Free()
        {
            isBusy = false;
            TargetFloor = null;
            FinalFloor = null;
            TargetSession = null;
        }

    }
}