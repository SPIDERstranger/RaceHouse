using System;
namespace RaceHouse
{
    /// <summary>
    /// 马类
    /// </summary>
    public class House
    {
        /// <summary>
        /// 马匹id
        /// </summary>
        public int Id;

        /// <summary>
        /// 马匹强壮等级
        /// </summary>
        public float Strong;

        /// <summary>
        /// 马匹重量
        /// </summary>
        public float Weight;

        /// <summary>
        /// 马匹当前位置
        /// </summary>
        public float Position;

        /// <summary>
        /// 马匹的力量
        /// </summary>
        public float Power;

        /// <summary>
        /// 当前马匹的赔率
        /// </summary>
        public float Race;

        /// <summary>
        /// 马匹的名次
        /// </summary>
        public int Ranking = -1;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="id">马匹id</param>
        public House(int id)
        {
            this.Id = id;
            this.Strong = ((float)Program.Random.NextDouble() * 5) + 10; // 10~15
            this.Weight = ((float)Program.Random.NextDouble() * 3) + 5; // 5~8   speed = 
            this.Power = (this.Strong / this.Weight) + 6 + (2 * (float)Program.Random.NextDouble());
            this.Race = 12 - this.Power + (float)Program.Random.NextDouble();
            this.Position = 0;
        }

        public House(int id, float strong, float weight, float position, float power, float race) : this(id)
        {
            Strong = strong;
            Weight = weight;
            Position = position;
            Power = power;
            Race = race;
        }



        /// <summary>
        /// 获取马匹速度
        /// </summary>
        /// <returns>马匹速度</returns>
        public float GetSpeed(bool random = true)
        {
            if (random)
                return ((this.Strong + Program.Random.Next(0, (15 - (int)this.Strong))) / this.Weight) + Program.Random.Next(-1, 2);
            else
                return Power;
            // return Strong / (Weight + Program.Random.Next(0, (12 - (int)Race))) + Program.Random.Next(-1, 2);
        }

        /// <summary>
        /// 马匹跑步驱动的方法
        /// </summary>
        /// <returns>当前赛马的长度</returns>
        public float Run()
        {
            this.Run(this.GetSpeed());
            return this.Position;
        }

        /// <summary>
        /// 马匹跑步驱动的方法
        /// </summary>
        /// <returns>当前赛马的长度</returns>
        public float Run(float speed)
        {
            this.Position += speed > 0 ? speed : 0;

            return this.Position;
        }

        /// <summary>
        /// 打印马匹位置信息
        /// </summary>
        /// <returns>信息</returns>
        public string PrintPos()
        {
            string str = string.Empty;
            for (int i = 0; i < this.Position; i++)
            {
                str += "*";
            }

            str += "马#" + this.Id.ToString();
            return str;
        }
    }
}
