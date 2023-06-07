using PPPP_lab3.Types;

namespace PPPP_lab3
{
    public static class Program
    {

        private static int decr(string str)
        {
            var it = SingltonGlobalNums.Instance.GetTowns().Values.ToArray();
            foreach (int i in it)
            {
                if (str.Equals(it))
                    return it.First();
            }
            return -1;
        }

        private static int[] optim(int[,] arr, int beginPoint, int endPoint)
        {
            int[] d = new int[SingltonGlobalNums.Instance.GetSize()];
            int[] v = new int[SingltonGlobalNums.Instance.GetSize()];
            int temp;
            int minindex, min;
            int begin_index = beginPoint;

            for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
            {
                d[i] = 99999;
                v[i] = 1;
            }
            d[begin_index] = 0;

            do
            {
                minindex = 99999;
                min = 99999;
                for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                {
                    if ((v[i] == 1) && (d[i] < min))
                    {
                        min = d[i];
                        minindex = i;
                    }
                }

                if (minindex != 99999)
                {
                    for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                    {
                        if (arr?[minindex, i] > 0)
                        {
                            temp = min + arr[minindex, i];
                            if (temp < d[i])
                            {
                                d[i] = temp;
                            }
                        }
                    }
                    v[minindex] = 0;
                }
            } while (minindex < 99999);

            int[] ver = new int[SingltonGlobalNums.Instance.GetSize()];
            int end = endPoint;
            ver[0] = end;
            int k = 1;
            int weight = d[end];

            while (end != begin_index)
            {
                for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                    if (arr?[end, i] != 0)
                    {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                        temp = weight - arr[end, i];
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                        if (temp == d[i])
                        {
                            weight = temp;
                            end = i;
                            ver[k] = i + 1;
                            k++;
                        }
                    }
            }
            for (int i = 0; i < k / 2; i++)
            {
                var num = ver[i];
                ver[i] = ver[k - 1 - i];
                ver[k - 1 - i] = num;
            }
            return ver;
        }

        public static Track best(string startP, string finishP, SpeedType deliv, int volume)
        {
            int start = decr(startP);
            int finish = decr(finishP);
            int[,] mat = new int[SingltonGlobalNums.Instance.GetSize(), SingltonGlobalNums.Instance.GetSize()];

            for (int k = 0; k < SingltonGlobalNums.Instance.GetSize(); k++)
            {
                for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                    mat[k, i] = k + i;
            }


            mat = matrixUpd(deliv);
            var path = new int[SingltonGlobalNums.Instance.GetSize()];
            for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
            {
                path[i] = -1;
            }
            path = optim(mat, start, finish);
            int count = 0;
            for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
            {
                if (path[i] != -1)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            switch (count)
            {
                case 1:
                    {
                        Car car1 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[0]]))
                            {
                                car1 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[0], path[1]]);
                            }
                        }

                        Track track = new Track(car1, volume);
                        return track;
                    }
                case 3:
                    {
                        Car car1 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[0]]))
                            {
                                car1 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[0], path[1]]);
                            }
                        }

                        Car car2 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[2]]))
                            {
                                car2 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[2], path[3]]);
                            }
                        }

                        Train train1 = new Train();
                        Plane plane1 = new Plane();
                        Track track = new Track();

                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[1]]) && SingltonGlobalNums.Instance.GetPoints()[path[1]].IndexOf("TS") != -1)
                            {
                                train1 = new Train(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 1], SingltonGlobalNums.Instance.GetMatrixDist()[path[1], path[2]]);
                                track = new Track(car1, car2, train1, volume);
                            }

                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[1]]) && SingltonGlobalNums.Instance.GetPoints()[path[1]].IndexOf("AP") != -1)
                            {
                                plane1 = new Plane(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3], SingltonGlobalNums.Instance.GetMatrixDist()[path[1], path[2]]);
                                track = new Track(car1, car2, plane1, volume);
                            }
                        }

                        return track;
                    }
                case 5:
                    {
                        Car car1 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[0]]))
                            {
                                car1 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[0], path[1]]);
                            }
                        }

                        Train train1 = new Train();
                        Plane plane1 = new Plane();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[1]]) && SingltonGlobalNums.Instance.GetPoints()[path[1]].IndexOf("TS") != -1)
                            {
                                train1 = new Train(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 1], SingltonGlobalNums.Instance.GetMatrixDist()[path[1], path[2]]);
                            }
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[1]]) && SingltonGlobalNums.Instance.GetPoints()[path[1]].IndexOf("AP") != -1)
                            {
                                plane1 = new Plane(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3], SingltonGlobalNums.Instance.GetMatrixDist()[path[1], path[2]]);
                            }
                        }

                        Car car2 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[2]]))
                            {
                                car2 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[2], path[3]]);
                            }
                        }

                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[3]]) && SingltonGlobalNums.Instance.GetPoints()[path[3]].IndexOf("AP") != -1)
                            {
                                plane1 = new Plane(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3], SingltonGlobalNums.Instance.GetMatrixDist()[path[3], path[4]]);
                            }
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[3]]) && SingltonGlobalNums.Instance.GetPoints()[path[3]].IndexOf("TS") != -1)
                            {
                                train1 = new Train(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 1], SingltonGlobalNums.Instance.GetMatrixDist()[path[3], path[4]]);
                            }
                        }

                        Car car3 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[4]]))
                            {
                                car3 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[4], path[5]]);
                            }
                        }

                        Track track = new Track(car1, car2, car3, train1, plane1, volume);
                        return track;
                    }
                case 7:
                    {
                        Car car1 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[0]]))
                            {
                                car1 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[0], path[1]]);
                            }
                        }

                        Train train1 = new Train();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[1]]))
                            {
                                train1 = new Train(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 1], SingltonGlobalNums.Instance.GetMatrixDist()[path[1], path[2]]);
                            }
                        }

                        Car car2 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[2]]))
                            {
                                car2 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[2], path[3]]);
                            }
                        }

                        Plane plane1 = new Plane();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[3]]))
                            {
                                plane1 = new Plane(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3], SingltonGlobalNums.Instance.GetMatrixDist()[path[3], path[4]]);
                            }
                        }

                        Car car3 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[4]]))
                            {
                                car3 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[4], path[5]]);
                            }
                        }

                        Train train2 = new Train();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[5]]))
                            {
                                train2 = new Train(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 1], SingltonGlobalNums.Instance.GetMatrixDist()[path[5], path[6]]);
                            }
                        }

                        Car car4 = new Car();
                        foreach (var it in SingltonGlobalNums.Instance.GetTable())
                        {
                            if (it.Key.Contains(SingltonGlobalNums.Instance.GetPoints()[path[6]]))
                            {
                                car4 = new Car(SingltonGlobalNums.Instance.GetTableCost()[it.Value * 3 + 2], SingltonGlobalNums.Instance.GetMatrixDist()[path[6], path[7]]);
                            }
                        }

                        Track track = new Track(car1, car2, car3, car4, train1, train2, plane1, volume);

                        return track;
                    }
                default:
                    {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
                        return null;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
                    }
            }

        }

        private static int[,] matrixUpd(SpeedType type)
        {
            int[,] matrix = new int[SingltonGlobalNums.Instance.GetSize(), SingltonGlobalNums.Instance.GetSize()];
            for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
            {
                for (int j = 0; j < SingltonGlobalNums.Instance.GetSize(); j++)
                {
                    matrix[i, j] = SingltonGlobalNums.Instance.GetMatrixDist()[i, j];
                }
            }
            switch (type)
            {
                case SpeedType.Economy:
                    for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                    {
                        if (SingltonGlobalNums.Instance.GetPoints()[i].Contains("TS"))
                            for (int j = 0; j < SingltonGlobalNums.Instance.GetSize(); j++)
                                matrix[i, j] = 99999;
                    }
                    break;
                case SpeedType.Standart:
                    for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                    {
                        if (SingltonGlobalNums.Instance.GetPoints()[i].Contains("AP"))
                            for (int j = 0; j < SingltonGlobalNums.Instance.GetSize(); j++)
                                matrix[i, j] = 99999;
                    }
                    break;
                case SpeedType.Turbo:
                    for (int i = 0; i < SingltonGlobalNums.Instance.GetSize(); i++)
                    {
                        for (int j = 0; j < SingltonGlobalNums.Instance.GetSize(); j++)
                            if (matrix[i, j] == 0)
                                matrix[i, j] = 99999;
                    }
                    break;
                default:
                    break;
            }
            return matrix;
        }

        public static void ChangeViewColor(ConsoleColor backColor, ConsoleColor foreColor)
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
        }

        public static void TestFactory()
        {
            List<Transport> transportList = new List<Transport>();
            List<Transport> transportList2 = new List<Transport>();
            var rand = new Random();

            transportList.Add(TransportFactory.CreateTransport(TransportType.Train));
            transportList.Add(TransportFactory.CreateTransport(TransportType.Plane));
            transportList.Add(TransportFactory.CreateTransport(TransportType.Car));

            var k = SingltonGlobalNums.Instance.GetMatrixDist();

            transportList.Add(TransportFactory.CreateTransport(TransportType.Train,
                                                               SingltonGlobalNums.Instance.
                                                               GetTableCost()[rand.Next(0, SingltonGlobalNums.Instance.GetTableCost().Count - 1)],
                                                               SingltonGlobalNums.Instance.
                                                               GetMatrixDist()[rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1),
                                                               rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1)]));
            transportList.Add(TransportFactory.CreateTransport(TransportType.Plane,
                                                               SingltonGlobalNums.Instance.
                                                               GetTableCost()[rand.Next(0, SingltonGlobalNums.Instance.GetTableCost().Count - 1)],
                                                               SingltonGlobalNums.Instance.
                                                               GetMatrixDist()[rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1),
                                                               rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1)]));
            transportList.Add(TransportFactory.CreateTransport(TransportType.Car,
                                                               SingltonGlobalNums.Instance.
                                                               GetTableCost()[rand.Next(0, SingltonGlobalNums.Instance.GetTableCost().Count - 1)],
                                                               SingltonGlobalNums.Instance.
                                                               GetMatrixDist()[rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1),
                                                               rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1)]));

            ChangeViewColor(ConsoleColor.Yellow, ConsoleColor.Black);

            Console.WriteLine("Transport Distans by ID:\n");
            foreach (var transport in transportList)
            {
                Console.WriteLine($"{transport.Id}) - {transport.getDistance().ToString()}");
            }

            transportList2 = TransportFactory.CreateTransport(TransportType.Train,
                                                             SingltonGlobalNums.Instance.
                                                             GetTableCost()[rand.Next(0, SingltonGlobalNums.Instance.GetTableCost().Count - 1)],
                                                             SingltonGlobalNums.Instance.
                                                             GetMatrixDist()[rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1),
                                                             rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1)], 2);

            ChangeViewColor(ConsoleColor.Black, ConsoleColor.Yellow);
            Console.WriteLine("------------------------------------------------------");
            foreach (var transport in transportList2)
            {
                Console.WriteLine($"{transport.Id}) - {transport.getDistance().ToString()}");
            }

            transportList2 = TransportFactory.CreateTransport(TransportType.Plane,
                                                             SingltonGlobalNums.Instance.
                                                             GetTableCost()[rand.Next(0, SingltonGlobalNums.Instance.GetTableCost().Count - 1)],
                                                             SingltonGlobalNums.Instance.
                                                             GetMatrixDist()[rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1),
                                                             rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1)],5);

            ChangeViewColor(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine("------------------------------------------------------");
            foreach (var transport in transportList2)
            {
                Console.WriteLine($"{transport.Id}) - {transport.getDistance().ToString()}");
            }

            transportList2 = TransportFactory.CreateTransport(TransportType.Car,
                                                              SingltonGlobalNums.Instance.
                                                              GetTableCost()[rand.Next(0, SingltonGlobalNums.Instance.GetTableCost().Count - 1)],
                                                              SingltonGlobalNums.Instance.
                                                              GetMatrixDist()[rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1),
                                                              rand.Next(0, SingltonGlobalNums.Instance.GetSize() - 1)], 10);

            ChangeViewColor(ConsoleColor.Black, ConsoleColor.Yellow);
            Console.WriteLine("------------------------------------------------------");
            foreach (var transport in transportList2)
            {
                Console.WriteLine($"{transport.Id}) - {transport.getDistance().ToString()}");
            }
        }

        public static void View()
        {
            ChangeViewColor(ConsoleColor.Black, ConsoleColor.Green);

            Console.WriteLine("------------------------------------------------------");

            TestFactory();

            ChangeViewColor(ConsoleColor.Black, ConsoleColor.Green);

            Console.WriteLine("------------------------------------------------------");
        }

        public static void Main()
        {
            try
            {
                View();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                ChangeViewColor(ConsoleColor.Red, ConsoleColor.Black);
                Console.WriteLine("ERROR: " + ex);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey();
                throw;
            }
        }
    }
}
