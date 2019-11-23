using System;
using System.Collections.Generic;

namespace RaceHouse
{
    public class NotRandom
    {
        List<House> houses = new List<House>();

        public float StartGame(int id, float m)
        {
            float playerMoney = 100;
            int houseNum = houses.Count;
            int houseId = id;
            float money = m;
            playerMoney -= money;

            // 比赛
            int raceLen = 100;
            int nowRank = 1; // 用于判断到达的是第几名
            do
            {
                foreach (var house in houses)
                {
                    if (house.Ranking == -1)
                    {
                        house.Run(house.GetSpeed(false));
                        if (house.Position > raceLen)
                        {
                            house.Ranking = nowRank;
                            nowRank++;
                        }
                    }
                }
            }
            while (nowRank < houseNum);
            if (houses[houseId - 1].Ranking == 1)
            {
                playerMoney += money * houses[houseId - 1].Race;
            }
            return playerMoney;

        }

        public void AddHouse(int id, float power, float race)
        {
            houses.Add((new House(id, 0, 0, 0, power, race)));
        }
    }
}
