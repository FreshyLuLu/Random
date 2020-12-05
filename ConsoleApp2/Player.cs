using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Player
    {

        public string Id { get; set; }
        public int Jine { get; set; }
        public double 权重 { get; set; }
        public double 概率{get; set; }
    }

    public class PlayersXML
    {
        public Player[] players;
    }
}
