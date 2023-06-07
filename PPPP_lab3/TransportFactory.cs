using PPPP_lab3.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPP_lab3
{
    public static class TransportFactory
    {
        public static Transport CreateTransport(TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.Train:
                    return new Train();
                case TransportType.Plane:
                    return new Plane();
                case TransportType.Car:
                    return new Car();
                default:
                    throw new ArgumentException("Invalid transport type");
            }
        }

        public static Transport CreateTransport(TransportType transportType, (int, int, double) tuple, int dist)
        {
            switch (transportType)
            {
                case TransportType.Train:
                    return new Train(tuple, dist);
                case TransportType.Plane:
                    return new Plane(tuple, dist);
                case TransportType.Car:
                    return new Car(tuple, dist);
                default:
                    throw new ArgumentException("Invalid transport type");
            }
        }
        
        public static List<Transport> CreateTransport(TransportType transportType, (int, int, double) tuple, int dist, int count)
        {
            List<Transport> transports = new List<Transport>();

            for (int i = 0; i < count; i++)
            {
                switch (transportType)
                {
                    case TransportType.Train:
                        transports.Add( new Train(tuple, dist));
                        break;
                    case TransportType.Plane:
                        transports.Add(new Plane(tuple, dist));
                        break;
                    case TransportType.Car:
                        transports.Add(new Car(tuple, dist));
                        break;
                    default:
                        throw new ArgumentException("Invalid transport type");
                }
            }
            return transports;
        }
    }
}
