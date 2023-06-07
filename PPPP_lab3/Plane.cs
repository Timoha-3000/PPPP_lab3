namespace PPPP_lab3
{
    public class Plane : Transport
    {
        int planeSpeed;
        int planeVolume;
        double planePrice;
        int distance;

        public Plane()
        {
            this.planePrice = 500;
            this.planeSpeed = 300;
            this.planeVolume = 100;
            this.distance = 0;
            this.Id++;
        }

        public Plane((int, int, double) tuple, int dist)
        {
            this.planePrice = tuple.Item3;
            this.planeSpeed = tuple.Item2;
            this.planeVolume = tuple.Item1;
            this.distance = dist;
            this.Id++;
        }

        public override double sumCost(int mass, int dist)
        {
            return (mass / planeVolume) * sumTime(dist) * planePrice;
        }

        public override double sumTime(int dist)
        {
            return dist / planeSpeed;
        }

        public override int getDistance()
        {
            return this.distance;
        }

    }
}
