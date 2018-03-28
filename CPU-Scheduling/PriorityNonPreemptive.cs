using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_Scheduling
{
    class PriorityNonPreemptive :SchedulingAlgorithm
    {
        public PriorityNonPreemptive(Process[] processArray) : base(processArray)
        {
            isPreemptive = false;
        }

        //Overriden Methods
        protected override void ProcessArrival(Process process)
        {
            readyQueue.Enqueue(process, process.priority);
        }

        protected override Boolean SwappingNow()
        {
            //Nothing here STF is non-preemptive
            return false;
        }

        protected override void SwapLogic()
        {
            //Nothing here STF is non-preemptive
            return;
        }
    }
}
