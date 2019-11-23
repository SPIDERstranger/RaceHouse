using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceHouse
{
    public class House
    {


        public int Id;
        public float Strong;
        public float Weight;
        public float Position;
        public float Power;
        public float Race;
        public int Ranking = -1;
        public House(int id)
        {
            Id = id;
            Strong = (float)Program.Random.NextDouble() * 5 + 10; //10~15
            Weight = (float)Program.Random.NextDouble() * 3 + 5;//5~8   speed = 
            Power = (float)Strong / Weight + 6 + 2 * (float)Program.Random.NextDouble();
            Race = 12 - Power + (float)Program.Random.NextDouble();
            Position = 0;
        }

        public float getSpeed()
        {

            return (Strong + Program.Random.Next(0,(15-(int)Strong))) / Weight + Program.Random.Next(-1, 2);
            //return Strong / (Weight + Program.Random.Next(0, (12 - (int)Race))) + Program.Random.Next(-1, 2);
        }
        public float Run()
        {
            Position += getSpeed();
            return Position;
        }
        public string PrintPos()
        {
            string str = "";
            for (int i = 0; i < Position; i++)
            {
                str += " ";
            }

            str += "马#" + Id.ToString();
            return str;
        }
    }
}
