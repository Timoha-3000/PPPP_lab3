namespace PPPP_lab3
{
    public class Car : Transport
    {
        int carSpeed;
        int carVolume;
        double carPrice;
        int distance;

        public Car()
        {
            this.carPrice = 100;
            this.carSpeed = 60;
            this.carVolume = 50;
            this.distance = 0;
            this.Id++;
        }

        public Car((int, int, double) tuple, int dist)
        {
            this.carPrice = tuple.Item3;
            this.carSpeed = tuple.Item2;
            this.carVolume = tuple.Item1;
            this.distance = dist;
            this.Id++;
        }

        public override double sumCost(int mass, int dist)
        {
            return (mass / carVolume) * sumTime(dist) * carPrice;
        }

        public override double sumTime(int dist)
        {
            return dist / carSpeed;
        }

        public override int getDistance()
        {
            return this.distance;
        }

    }
}
