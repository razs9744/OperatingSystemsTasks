using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    public class SJF : Scheduler
    {
        public override void Schedule(SimulatorModel simulator)
        {
            //sort in descending order 
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => x.Item1.CompareTo(y.Item2));

            int currentTime = simulator.ProcessArrivingTime[0].Item1;
            int[] restBurstTime = new int[simulator.NumProcBigThanZero];
            int[] waitingTime = new int[simulator.NumProcBigThanZero];



            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
                restBurstTime[i] = simulator.ProcessArrivingTime[i].Item2;

            int complete = 0, t = simulator.ProcessArrivingTime[0].Item1, minm = int.MaxValue;
            int shortest = 0, finish_time;
            bool check = false;

            // Process until all processes gets 
            // completed 
            while (complete != simulator.NumProcBigThanZero)
            {
                // Find process with minimum 
                // remaining time among the 
                // processes that arrives till the 
                // current time` 
                for (int j = 0; j < simulator.NumProcBigThanZero; j++)
                {
                    if ((simulator.ProcessArrivingTime[j].Item1 <= t) &&
                    (restBurstTime[j] < minm) && restBurstTime[j] > 0)
                    {
                        minm = restBurstTime[j];
                        shortest = j;
                        check = true;
                    }
                }

                if (check == false)
                {
                    t++;
                    continue;
                }

                // Reduce remaining time by one 
                restBurstTime[shortest]--;

                // Update minimum 
                minm = restBurstTime[shortest];
                if (minm == 0)
                    minm = int.MaxValue;

                // If a process gets completely 
                // executed 
                if (restBurstTime[shortest] == 0)
                {

                    // Increment complete 
                    complete++;
                    check = false;

                    // Find finish time of current 
                    // process 
                    finish_time = t + 1;

                    // Calculate waiting time 
                    waitingTime[shortest] = finish_time -
                                simulator.ProcessArrivingTime[shortest].Item2 -
                                simulator.ProcessArrivingTime[shortest].Item1;

                    if (waitingTime[shortest] < 0)
                        waitingTime[shortest] = 0;
                }
                // Increment time 
                t++;
            }
            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
            {
                turnAroundTime += waitingTime[i];
            }
            this.PrintResult("SJF", turnAroundTime, simulator.NumProc);
        }
    }
}
