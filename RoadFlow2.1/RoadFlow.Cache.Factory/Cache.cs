using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadFlow.Cache.Factory
{
    public class Cache
    {
        public static Interface.ICache CreateInstance()
        {
            return new RoadFlow.Cache.InProc.Cache();
        }
    }
}
