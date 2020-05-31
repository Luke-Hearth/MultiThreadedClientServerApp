using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameInfo
    {

        public static int playerNumber = 1;

        public static ConcurrentDictionary<int, Boolean> gameStatus = new ConcurrentDictionary<int , Boolean>();

        public static String newPLayerID;

        public static ConcurrentDictionary<string , bool> players = new ConcurrentDictionary<string , bool>();



    }
}
