using NGenerics.DataStructures.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProcessSimulator
{
    public class RoundRobin : Scheduler
    {
        public override void Schedule(SimulatorModel simulator)
        {
            // sort all the proceeses
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => x.Item1.CompareTo(y.Item1));

            //array initialize 
            int[] restBurstTime = new int[simulator.NumProcBigThanZero];

            //var's init
            int currentTime = simulator.ProcessArrivingTime[0].Item1;
            const int timeQuantom = 2;
            int activeProcesses = simulator.NumProcBigThanZero;
            bool isProcessFinished = false;

            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
            {
                restBurstTime[i] = simulator.ProcessArrivingTime[i].Item2;
            }

            for (int i = 0; activeProcesses > 0;)
            {
                if (restBurstTime[i] <= timeQuantom && restBurstTime[i] > 0)
                {
                    currentTime += restBurstTime[i];
                    restBurstTime[i] = 0;
                    isProcessFinished = true;
                }
                else if (restBurstTime[i] > 0)
                {
                    restBurstTime[i] = restBurstTime[i] - timeQuantom;
                    currentTime = currentTime + timeQuantom;
                }
                if (restBurstTime[i] == 0 && isProcessFinished)
                {
                    activeProcesses--;
                    turnAroundTime += currentTime - simulator.ProcessArrivingTime[i].Item1;
                    isProcessFinished = false;
                }
                //end of the cycle
                if (i == simulator.NumProcBigThanZero - 1)
                {
                    i = 0;
                }
                else if (simulator.ProcessArrivingTime[i + 1].Item1 <= currentTime) //continue to next process if arrived
                {
                    i++;
                }
                else //back to the beginning if new process ain't arrived
                {
                    i = 0;
                }
            }
            this.PrintResult("Round Robin", turnAroundTime, simulator.NumProc);
        }
    }
}
