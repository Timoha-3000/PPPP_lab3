using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPP_lab3
{
    public abstract class Transport
    {
        private static int count_id;
        private string? _name;
        private int _id;

        public int Id { get { return _id; } set { _id = count_id++; } }
        public abstract double sumCost(int mass, int dist);
        public abstract double sumTime(int dist);
        public abstract int getDistance();
    }
}
