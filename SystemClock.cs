namespace ElevatorSimulation
{
    public class SystemClock
    {
        private int time { get; set;}
        private int dt { get; set; }
        public SystemClock(int initialTime)
        {
            time = initialTime;
            dt = 1;
        }
        public static SystemClock operator ++ (SystemClock sc)
        {
            sc.time += sc.dt;
            return sc;
        }

        public void SetDeltaTime(int deltaTime)
        {
            dt = deltaTime;
        }

        public int Time()
        {
            return time;
        }

        public int Dt()
        {
            return dt;
        }
    }
}