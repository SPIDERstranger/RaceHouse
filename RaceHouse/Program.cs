namespace RaceHouse
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 程序主体类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 随机种子
        /// </summary>
        private static Random random;

        /// <summary>
        ///  Gets 随机种子的set get方法
        /// </summary>
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

        /// <summary>
        /// 方法主题
        /// </summary>
        /// <param name="args">args</param>
        public static void Main(string[] args)
        {
            Program start = new Program();
            Task task = Task.Factory.StartNew(start.StartGame);
            task.Wait();
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame()
        {
            bool isEnd = false;
            float playerMoney = 100;
            while (!isEnd)
            {
                // 输入数据块
                int houseNum = this.InputHousesNum();
                List<House> houses = this.InitHouses(houseNum);
                this.PrintHouseProperties(houses);
                int houseId = this.InputHouseId(houseNum);
                float money = this.InputMoney(playerMoney);
                playerMoney -= money;
                Console.Clear();

                // 比赛
                int raceLen = 100;
                int nowRank = 1; // 用于判断到达的是第几名
                do
                {
                    Thread.Sleep(250);
                    Console.Clear();
                    this.PrintRace(raceLen);
                    foreach (var house in houses)
                    {
                        Console.WriteLine(house.PrintPos());
                        this.PrintRace(raceLen);
                        if (house.Ranking == -1)
                        {
                            house.Run();
                            if (house.Position > raceLen)
                            {
                                house.Ranking = nowRank;
                                nowRank++;
                            }
                        }
                    }

                    this.PrintPlayerInfo(playerMoney, houseId, money);
                }
                while (nowRank < houseNum);

                Console.WriteLine("你下注的马获得了第{0}名", houses[houseId - 1].Ranking);
                if (houses[houseId - 1].Ranking == 1)
                {
                    playerMoney += money * houses[houseId - 1].Race;
                    Console.WriteLine("恭喜你获得{0},目前你还剩￥{1}", houses[houseId - 1].Race, playerMoney);
                }
                else
                {
                    Console.WriteLine("很遗憾,你输了这场比赛,你还剩￥{0}", playerMoney);
                }

                Console.WriteLine("是否继续游戏，如果需要退出，请按Q/q");
                string q = Console.ReadLine();
                isEnd = (q == "Q" || q == "q");
            }
        }

        /// <summary>
        /// 输入要押注的金额
        /// </summary>
        /// <param name="playerMoney">金额</param>
        /// <returns>返回值</returns>
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

        /// <summary>
        /// 初始化马匹列表
        /// </summary>
        /// <param name="houseNum">马匹数量</param>
        /// <returns>马匹列表</returns>
        public List<House> InitHouses(int houseNum)
        {
            List<House> houses = new List<House>();
            for (int i = 0; i < houseNum; i++)
            {
                houses.Add(new House(i + 1));
            }

            return houses;
        }

        /// <summary>
        /// 打印马匹属性
        /// </summary>
        /// <param name="houses">马匹列表</param>
        private void PrintHouseProperties(List<House> houses)
        {
            foreach (var house in houses)
            {
                Console.WriteLine("马#{0}    健壮：{1:.0}      体重：{2:.0}      总分：{3:.0}      赔率：{4}", house.Id, house.Strong, house.Weight, house.Power, house.Race);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 输入马匹数量
        /// </summary>
        /// <returns>返回数量</returns>
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

        /// <summary>
        /// 输入要压住的马匹id
        /// </summary>
        /// <param name="houseNum">数量范围</param>
        /// <returns>返回id</returns>
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

        /// <summary>
        /// 打印赛道
        /// </summary>
        /// <param name="raceLen">赛道长度</param>
        private void PrintRace(int raceLen)
        {
            for (int i = 0; i < raceLen; i++)
            {
                Console.Write("=");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// 打印玩家当前信息
        /// </summary>
        /// <param name="playerMoney">用户资金</param>
        /// <param name="houseId">用户押注id</param>
        /// <param name="houseMoney">押注金额</param>
        private void PrintPlayerInfo(float playerMoney, int houseId, float houseMoney)
        {
            Console.WriteLine("你当前的金额是：{0}", playerMoney);
            Console.WriteLine("你押注的马是：马#{0}     押注金额是：{1}", houseId, houseMoney);
        }
    }
}
