using System.Collections.Generic;

namespace ElevatorSimulation
{
    public class House
    {
        public int FloorCount { get; set; }
        public SystemClock Clock { get; set; }
        public List<Elevator> Elevators { get; set; }
        public List<Session> InitialSessions { get; set; }
        public List<Session> ActiveSessions { get; set; }
        public List<Session> ClosedSessions { get; set; }

        public House(int floorCount, List<Elevator> elevators, SystemClock clock)
        {
            FloorCount = floorCount;
            Elevators = elevators;
            Clock = clock;
            InitialSessions = new List<Session>();
            ActiveSessions = new List<Session>();
            ClosedSessions = new List<Session>();
        }

        public int[] FreeElevators()
        {
            int free = 0;
            int busy = 0;
            foreach (var el in Elevators)
            {
                if (!el.IsBusy())
                {
                    free++;
                }
                else
                {
                    busy++;
                }
            }

            return new int[]{free, busy};
            
        }
        
        public void CloseSession(Session session)
        {
            ActiveSessions.Remove(session);
            session.EndTime = Clock.Time();
            session.TripTime = session.EndTime - session.StartTime;
            ClosedSessions.Add(session);
        }
    }
}