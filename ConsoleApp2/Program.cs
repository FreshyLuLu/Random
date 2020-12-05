using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    class Program
    {
        public static List<Player> AllPlayers = new List<Player>();
        public static FileInfo PlayersInfoFile = new FileInfo("D:/playersinfo.xml");
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("输入1=录入，输入2=增加金额，输入3=输出概率值，输入4=开奖");
                LoadPlayersInfo();
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("输入ID");
                        luru(Console.ReadLine());
                        break;
                    case "2":
                        Console.WriteLine("输入ID");
                        String Id = Console.ReadLine();
                        Console.WriteLine("输入金额");
                        int qian = int.Parse(Console.ReadLine());
                        addgl(Id, qian);
                        break;
                    case "3":
                        output();
                        break;
                    case "4":
                        开奖();
                        break;
                }
            }
        }
        public static void luru(String ID)
        {
            Player p = new Player();
            p.Id = ID;
            AllPlayers.Add(p);
            Console.WriteLine("成功添加了"+ID);
            SavePlayersInfo();
            Console.ReadLine();
        }
        public static void addgl(String ID, int qian)
        {
            foreach(Player p in AllPlayers)
            {
                if(p.Id == ID)
                {
                    p.Jine += qian;
                    Console.WriteLine("成功了");
                }
            }
            Console.WriteLine("如果没输出成功了就是没找到这个ID");
            SavePlayersInfo();
            Console.ReadLine();
        }
        public static void output()
        {
                Double total = 0;
                foreach (Player p in AllPlayers)
                {
                    p.权重 = (Double)(p.Jine / 25) / 2 + 1;
                    total += p.权重;
                }
                foreach (Player p in AllPlayers)
                {
                    p.概率 = (Double)p.权重 / total;
                    p.概率 = p.概率 * 100;
                }
                AllPlayers = AllPlayers.AsEnumerable().OrderBy(Player => Player.Jine).ToList();
                AllPlayers.Reverse();
                foreach (Player p in AllPlayers)
                {
                    String 概率真 = p.概率.ToString("f2");
                    Console.WriteLine("ID：" + p.Id + "   氪金总额：" + p.Jine + "    目前概率：" + 概率真 + "%");
                }
                SavePlayersInfo();
                Console.ReadLine();
         }
        public static void 开奖()
        {
            double 总权重数 = 0;
            foreach(Player p in AllPlayers)
            {
                总权重数 += p.权重;
            }
            Random r = new Random();
            总权重数=总权重数 * r.NextDouble();
            Console.WriteLine(总权重数);
            foreach(Player p in AllPlayers)
            {
                总权重数 -= p.权重;
                if (总权重数 <= 0)
                {
                    Console.WriteLine("中奖者为"+p.Id);
                    break;
                }
            }
            Console.ReadLine();
        }
        public static void SavePlayersInfo()
        {
            var x = new XmlSerializer(typeof(PlayersXML));
            var stream = PlayersInfoFile.Create();
            PlayersXML p = new PlayersXML();
            p.players = AllPlayers.ToArray();
            x.Serialize(stream, p);
            stream.Close();
        }
        public static void LoadPlayersInfo()
        {
            if (AllPlayers.Count > 0)
            {
                return;
            }
            var x = new XmlSerializer(typeof(PlayersXML));
            PlayersInfoFile.Refresh();
            if (PlayersInfoFile.Exists)
            {
                var stream = PlayersInfoFile.OpenRead();
                PlayersXML p = (PlayersXML)x.Deserialize(stream);
                AllPlayers.AddRange(p.players);
                stream.Close();
            }
        }
    }
}