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
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => x.Item1.CompareTo(y.Item1));

            int currentTime = simulator.ProcessArrivingTime[0].Item1;
            int[] restBurstTime = new int[simulator.NumProcBigThanZero];
            int[] waitingTime = new int[simulator.NumProcBigThanZero];

            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
                restBurstTime[i] = simulator.ProcessArrivingTime[i].Item2;

            int completeProc = 0, totalRunningTime = simulator.ProcessArrivingTime[0].Item1, minRemainTime = int.MaxValue;
            int shortestProcIndex = 0, finishTime;
            bool checkIfProcessFinished = false;

            // Process until all processes gets completed 
            while (completeProc != simulator.NumProcBigThanZero)
            {
                // Find process with minimum remaining time
                for (int i = 0; i < simulator.NumProcBigThanZero; i++)
                {
                    if ((simulator.ProcessArrivingTime[i].Item1 <= totalRunningTime) &&
                    (restBurstTime[i] < minRemainTime) && restBurstTime[i] > 0)
                    {
                        minRemainTime = restBurstTime[i];
                        shortestProcIndex = i;
                        checkIfProcessFinished = true;
                    }
                }

                if (checkIfProcessFinished == false)
                {
                    totalRunningTime++;
                    continue;
                }

                // Reduce remaining time by one 
                restBurstTime[shortestProcIndex]--;

                // Update minimum 
                minRemainTime = restBurstTime[shortestProcIndex];
                if (minRemainTime == 0)
                    minRemainTime = int.MaxValue;

                // If a process gets completely 
                // executed 
                if (restBurstTime[shortestProcIndex] == 0)
                {
                    // Increment completeProc 
                    completeProc++;
                    checkIfProcessFinished = false;

                    // Find finish time of current process
                    finishTime = totalRunningTime + 1;

                    // Calculate waiting time 
                    waitingTime[shortestProcIndex] = finishTime -
                                simulator.ProcessArrivingTime[shortestProcIndex].Item2 -
                                simulator.ProcessArrivingTime[shortestProcIndex].Item1;

                    if (waitingTime[shortestProcIndex] < 0)
                        waitingTime[shortestProcIndex] = 0;
                }
                // Increment time 
                totalRunningTime++;
            }

            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
            {
                turnAroundTime += waitingTime[i]+ simulator.ProcessArrivingTime[i].Item2;
            }
            this.PrintResult("SJF", turnAroundTime, simulator.NumProc);
        }
    }
}
