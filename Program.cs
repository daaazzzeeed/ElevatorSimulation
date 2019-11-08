using System;
using System.Collections.Generic;

namespace ElevatorSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Elevator simulation");

            SystemClock sc = new SystemClock(0); // create clock
            var Elevators = new List<Elevator>(); // create list of elevators
            const int ElevatorCount = 4; // number of elevators
            for (int i=0; i<ElevatorCount; i++)
            {
                // create elevators separated in two groups
                // pace elevators on to first floor
                if (i < 2)
                {
                    Elevators.Add(new Elevator(0, 1));  
                }
                else
                {
                    Elevators.Add(new Elevator(0, 2));
                }
            }

            int floorsCount = 29;
            House house = new House(floorsCount, Elevators, sc); // create house with 29 floors

            Console.WriteLine("ID\tFloor\tGroup");
            foreach (var el in Elevators)
            {
                // display info
                Console.WriteLine(el.ID + "\t" + el.CurrentFloor + "\t" + el.GroupID);
            }

            var Sessions = new List<Session>();
            int SessionsCount = 4;

            for (int i=0; i<SessionsCount; i++)
            {
                // create sessions
                if (i < 2)
                {
                    Sessions.Add(new Session(5, 20, 1, 5));
                }
                else
                {
                    Sessions.Add(new Session(8, 29, 2, 0));
                }
                
            }

            house.InitialSessions = Sessions;

            

            var info = house.FreeElevators();
            Console.WriteLine("there are {0} free elevators", info[0]);
            Console.WriteLine("there are {0} busy elevators", info[1]);
            int InitialSize = Sessions.Count;
            Console.WriteLine(InitialSize);

            while (house.ClosedSessions.Count < InitialSize)
            {
                Console.WriteLine("System Time: {0}", sc.Time());
                Console.WriteLine("Active sessions: " + Convert.ToString(house.ActiveSessions.Count));
                Console.WriteLine("Initial sessions: " + Convert.ToString(house.InitialSessions.Count));
                Console.WriteLine("Closed sessions: " + Convert.ToString(house.ClosedSessions.Count));
                // while house has inactive sessions and active sessions
                // handle sessions
                var ToRemove = new List<Session>();

                for (int i=0; i<house.InitialSessions.Count; i++)
                {
                    // iterate through initial sessions 
                    if (info[0] != 0)
                    {
                        // if house has free elevators
                        foreach (var elevator in house.Elevators)
                        {
                            // iterate through elevators
                            // find free elevator
                            if (!elevator.IsBusy())
                            {
                                // if elevator is free
                                // respond to a session
                                // remove session from list of initial sessions 
                                // add session to a list of active sessions 
                                elevator.RespondToSession(house.InitialSessions[i]);
                                ToRemove.Add(house.InitialSessions[i]);
                                house.ActiveSessions.Add(house.InitialSessions[i]); 
                                break;                               
                            }
                        }
                    }
                }

                for (int i=0; i<ToRemove.Count; i++)
                {
                    house.InitialSessions.Remove(ToRemove[i]);
                }

                foreach ( var elevator in house.Elevators)
                {
                    if (elevator.FinalFloor == null)
                    {
                        if (elevator.CurrentFloor == elevator.TargetFloor)
                        {
                            elevator.FinalFloor = elevator.TargetSession.TargetFloor;
                        }
                        else
                        {
                            elevator.MoveToTarget(elevator.TargetSession.FromFloor);
                        }
                    }
                    else
                    {
                        if (elevator.CurrentFloor == elevator.FinalFloor)
                        {
                            house.CloseSession(elevator.TargetSession);
                            //elevator.Free();
                        }
                        else
                        {
                            elevator.MoveToTarget(elevator.TargetSession.TargetFloor.Value);
                        }
                    }
                    if (elevator.IsBusy())
                    {
                        Console.WriteLine("Elevator" + elevator.ID + " on floor " + elevator.CurrentFloor +
                     " (Target floor: " + elevator.TargetFloor + " / Final floor: " + elevator.FinalFloor +
                      ", Trip Time: "+ elevator.TargetSession.TripTime+" timeunits)");
                    }
                    
                } 

                house.Clock++;
            }

        }
    }
}
