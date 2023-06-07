namespace PPPP_lab3
{
    public class Train : Transport
    {
        int trainSpeed;
        int trainVolume;
        double trainPrice;
        int distance;

        public Train()
        {
            this.trainPrice = 200;
            this.trainSpeed = 100;
            this.trainVolume = 500;
            this.distance = 0;
            this.Id++;
        }

        public Train((int, int, double) tuple, int dist)
        {
            this.trainPrice = tuple.Item3;
            this.trainSpeed = tuple.Item2;
            this.trainVolume = tuple.Item1;
            this.distance = dist;
            this.Id++;
        }

        public override double sumCost(int mass, int dist)
        {
            return (mass / trainVolume) * sumTime(dist) * trainPrice;
        }

        public override double sumTime(int dist)
        {
            return dist / trainSpeed;
        }

        public override int getDistance()
        {
            return this.distance;
        }
    }
}
