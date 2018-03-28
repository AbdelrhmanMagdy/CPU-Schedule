using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_Scheduling
{
    class PriorityPreemptive : SchedulingAlgorithm
    {
        public PriorityPreemptive(Process[] processArray) : base(processArray)
        {
            isPreemptive = true;
        }

        //Overriden Methods
        protected override void ProcessArrival(Process process)
        {
            readyQueue.Enqueue(process, process.priority);
        }

        protected override Boolean SwappingNow()
        {
            Process top = readyQueue.Peek();

            if (top != null && currentProcess != null)
                return top.priority < currentProcess.priority;
            else
                return false;
        }

        protected override void SwapLogic()
        {
            readyQueue.Enqueue(currentProcess, currentProcess.priority);
        }
    }
}
