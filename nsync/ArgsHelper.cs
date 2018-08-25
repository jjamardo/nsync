using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsync
{
    class ArgsHelper
    {
        private bool initialSync = false;
        private bool initialSyncSeted = false;
        public bool InitialSync { get { return initialSync; } }
        public bool InitialSyncSeted { get { return initialSyncSeted; } }
        public ArgsHelper(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "/ise")
                {
                    initialSync = true;
                    initialSyncSeted = true;
                }
                else if (args[i] == "/isd")
                {
                    initialSyncSeted = true;
                }
            }
        }
    }
}
