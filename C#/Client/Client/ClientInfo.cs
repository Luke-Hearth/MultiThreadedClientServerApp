using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ClientInfo
    {
        public static bool hasBall;

        public static ConcurrentDictionary<string, Boolean> playerAsString = new ConcurrentDictionary<string, Boolean>();

        public static string newPlayerID;

        public static string playerWithBall;

        public static bool firstTime = true;

        public static string thisplayerNumber;

        public static int passPlayer;

        public static bool pass;

    }
}
