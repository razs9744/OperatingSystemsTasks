using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    public class FCFS : Scheduler
    {

        public override void Schedule(SimulatorModel simulator)
        {
            int sigmaBurstTime = 0;
            int[] compTime = new int[simulator.NumProcBigThanZero];
            int[] waitingTime = new int[simulator.NumProcBigThanZero];
            waitingTime[0] = 0;

            //sort in ascending order 
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => x.Item1.CompareTo(y.Item1));


            // calculating waiting time  
            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
            {
                sigmaBurstTime += simulator.ProcessArrivingTime[i].Item2;
                compTime[i] = simulator.ProcessArrivingTime[0].Item1 + sigmaBurstTime;
                if (i > 0)
                {
                    // Add burst time of previous processes  

                    // Find waiting time for current process 
                    waitingTime[i] = compTime[i - 1] - simulator.ProcessArrivingTime[i].Item1;

                    // If waiting time for a process is negative  
                    // that means the process does not wait at all  
                    if (waitingTime[i] < 0)
                        waitingTime[i] = 0;
                }
                turnAroundTime += simulator.ProcessArrivingTime[i].Item2 + waitingTime[i];
            }
            this.PrintResult("FCFS", turnAroundTime, simulator.NumProc);
        }
    }
}
