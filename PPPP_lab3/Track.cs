namespace PPPP_lab3
{
    public class Track
    {
        double cost;
        double time;
#pragma warning disable CS0169 // Поле "Track.volume" никогда не используется.
        int volume;
#pragma warning restore CS0169 // Поле "Track.volume" никогда не используется.

        public Track() { }

        public Track(Car car1, int volume)
        {
            cost = car1.sumCost(volume, car1.getDistance());
            time = car1.sumTime(car1.getDistance());
        }

        public Track(Car car1, Car car2, Train train1, int volume)
        {
            cost = car1.sumCost(volume, car1.getDistance()) + car2.sumCost(volume, car2.getDistance());
            time = car1.sumTime(car1.getDistance()) + car2.sumTime(car2.getDistance());
            cost += train1.sumCost(volume, train1.getDistance());
            time += train1.sumTime(train1.getDistance());
        }

        public Track(Car car1, Car car2, Plane plane1, int volume)
        {
            cost = car1.sumCost(volume, car1.getDistance()) + car2.sumCost(volume, car2.getDistance());
            time = car1.sumTime(car1.getDistance()) + car2.sumTime(car2.getDistance());
            cost += plane1.sumCost(volume, plane1.getDistance());
            time += plane1.sumTime(plane1.getDistance());
        }

        public Track(Car car1, Car car2, Car car3, Train train1, Plane plane1, int volume)
        {
            cost = car1.sumCost(volume, car1.getDistance()) + car2.sumCost(volume, car2.getDistance()) + car3.sumCost(volume, car3.getDistance());
            time = car1.sumTime(car1.getDistance()) + car2.sumTime(car2.getDistance()) + car3.sumTime(car3.getDistance());
            cost += train1.sumCost(volume, train1.getDistance());
            time += train1.sumTime(train1.getDistance());
            cost += plane1.sumCost(volume, plane1.getDistance());
            time += plane1.sumTime(plane1.getDistance());
        }

        public Track(Car car1, Car car2, Car car3, Car car4, Train train1, Train train2, Plane plane1, int volume)
        {
            cost = car1.sumCost(volume, car1.getDistance()) + car2.sumCost(volume, car2.getDistance()) + car3.sumCost(volume, car3.getDistance()) + car4.sumCost(volume, car4.getDistance());
            time = car1.sumTime(car1.getDistance()) + car2.sumTime(car2.getDistance()) + car3.sumTime(car3.getDistance()) + car4.sumTime(car4.getDistance());
            cost += train1.sumCost(volume, train1.getDistance()) + train2.sumCost(volume, train2.getDistance());
            time += train1.sumTime(train1.getDistance()) + train2.sumTime(train2.getDistance());
            cost += plane1.sumCost(volume, plane1.getDistance());
            time += plane1.sumTime(plane1.getDistance());
        }

        public double getCost()
        {
            return this.cost;
        }
    }
}
