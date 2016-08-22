using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFortressData
{
    class TF2DataContainer
    {
        public string ServerIP { get; set; }
        public int TotalKills { get; set; }
        public int TotalPlayers { get; set; } // guessed
        public int TotalSuicides { get; set; }
        public int TotalAmountOfBackstabs { get; set; }
        public int TotalAmountOfHeadshots { get; set; }
        public int TotalAmountOfSentryKills { get; set; }
        public int TotalAmountOfCrits { get; set; }
        public int TotalAmountOfReflectKills { get; set; }
        public ArrayList AllPlayers { get; set; }

    }
}
