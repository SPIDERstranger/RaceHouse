using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceHouse
{
    class Program
    {
        private static Random random;
        public static Random Random
        {
            get
            {
                if (random == null)
                {
                    random = new Random();
                }
                return random;
            }
        }
        static void Main(string[] args)
        {
            Program start = new Program();
            Task task = Task.Factory.StartNew(start.StartGame);
            task.Wait();
        }

        private void StartGame()
        {
            bool IsEnd = false;
            float PlayerMoney = 100;
            while (!IsEnd)
            {
                #region 输入数据块
                int houseNum = InputHousesNum();
                List<House> houses = InitHouses(houseNum);
                PrintHouseProperties(houses);
                int houseId = InputHouseId(houseNum);
                float money = InputMoney(PlayerMoney);
                PlayerMoney -= money;
                Console.Clear();

                #endregion
                #region 比赛
                int RaceLen = 100;
                int nowRank = 1;//用于判断到达的是第几名
                do
                {
                    Thread.Sleep(250);
                    Console.Clear();
                    //PrintHouseProperties(houses);
                    PrintRace(RaceLen);
                    foreach (var house in houses)
                    {
                        Console.WriteLine(house.PrintPos());
                        PrintRace(RaceLen);
                        if (house.Ranking == -1)
                        {
                            house.Run();
                            if (house.Position > RaceLen)
                            {
                                house.Ranking = nowRank;
                                nowRank++;
                            }
                        }
                    }
                    PrintPlayerInfo(PlayerMoney, houseId, money);
                    } while (nowRank > houseNum) ;
                #endregion

                Console.WriteLine("你下注的马获得了第{0}名", houses[houseId - 1].Ranking);
                if (houses[houseId - 1].Ranking == 1)
                {
                    PlayerMoney += money * houses[houseId - 1].Race;
                    Console.WriteLine("恭喜你获得{0},目前你还剩￥{1}", houses[houseId - 1].Race, PlayerMoney);
                }
                else
                {
                    Console.WriteLine("很遗憾,你输了这场比赛,你还剩￥{0}", PlayerMoney);
                }
                Console.WriteLine("是否继续游戏，如果需要退出，请按Q/q");
                string q = Console.ReadLine();
                IsEnd = (q == "Q" || q == "q");
            }
        }

        private float InputMoney(float playerMoney)
        {

            Console.WriteLine("你当前的金额：{0}", playerMoney);
            float money;
            try
            {
                Console.Write("请输入你要押注的金额：");
                money = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                money = -1;
            }
            while (money > playerMoney || money < 0)
            {
                try
                {
                    Console.Write("请重新输入你要押注的金额：");
                    money = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    money = -1;
                }
            }
            return money;
        }

        private List<House> InitHouses(int houseNum)
        {
            List<House> houses = new List<House>();
            for (int i = 0; i < houseNum; i++)
            {
                houses.Add(new House(i + 1));
            }
            return houses;
        }

        private void PrintHouseProperties(List<House> houses)
        {
            foreach (var house in houses)
            {
                Console.WriteLine("马#{0}    健壮：{1:.0}      体重：{2:.0}      总分：{3:.0}      赔率：{4}", house.Id, house.Strong, house.Weight, house.Power, house.Race);
                Console.WriteLine();
            }
        }

        private int InputHousesNum()
        {
            Console.Write("输入你想赛马的马数量[5~10] : ");
            int houseNum = Convert.ToInt32(Console.ReadLine());
            while (houseNum < 5 || houseNum > 10)
            {
                Console.Write("请重新输入你想赛马的马数量[5~10] : ");
                houseNum = Convert.ToInt32(Console.ReadLine());
            }
            return houseNum;
        }

        private int InputHouseId(int houseNum)
        {
            Console.Write("你要下注的马#是[1~{0}]:", houseNum);
            int houseId = Convert.ToInt32(Console.ReadLine());
            while (houseId < 1 || houseId > houseNum)
            {
                Console.Write("请重新输入你要下注的马#[1~{0}]:", houseNum);
                houseId = Convert.ToInt32(Console.ReadLine());
            }
            return houseId;
        }

        private void PrintRace(int raceLen)
        {
            for (int i = 0; i < raceLen; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        private void PrintPlayerInfo(float PlayerMoney, int houseId, float houseMoney)
        {
            Console.WriteLine("你当前的金额是：{0}", PlayerMoney);
            Console.WriteLine("你押注的马是：马#{0}     押注金额是：{1}", houseId, houseMoney);
        }
    }
}
