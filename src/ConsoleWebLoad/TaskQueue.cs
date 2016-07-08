using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebLoad
{
    public class TaskQueue:List<Task>
    {
        public TaskQueue(int queueSize)
        {
            this._QueueSize = queueSize;
        }

        private int _QueueSize;

        protected List<Task> WorkingTasks = new List<Task>();

        public void WaitToDone()
        {

        }
    }
}
