using PPPP_lab3.Types;

namespace PPPP_lab3
{
    public class Order
    {
        double cost;
        int volume;
        string startPoint;
        string finishPoint;
        SpeedType type;
        Track track;

        public Order()
        {
            this.cost = 0;
            this.volume = 0;
            this.startPoint = "";
            this.finishPoint = "";
            this.track = new Track(new Car(), 22);
            this.type = SpeedType.Economy;
        }

        public Order(SpeedType deliv, string startP, string finishP, int vol)
        {

            this.startPoint = startP;
            this.finishPoint = finishP;
            type = deliv;
            this.track = Program.best(startP, finishP, deliv, vol);
            this.cost = track.getCost();
            this.volume = vol;
        }
    }
}
