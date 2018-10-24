using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaMillions
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string file = GetFileName("MegaMillion.txt");
                var rawData = ReadDrawings(file);

                Console.WriteLine("Mega Million Winning Numbers\n");
                Console.WriteLine("1. Display All Winning Numbers");
                Console.WriteLine("2. Calculate Frequency of number selected");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("9. Exit");
                Console.Write ("Select Option : ");
                string option = Console.ReadLine();
                                
                if(option == "9")
                {
                    break;
                }
                if(option == "1")
                {
                    DisplayDrawings(rawData);
                }

                if(option == "2")
                {
                    GetAllFiveBalls(rawData);
                }

            }
        }

        public static string GetFileName(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            return Path.Combine(directory.FullName, fileName);
        }

        public static List<WeeklyDrawings> ReadDrawings(string fileLocation)
        {
            var weeklyDrawing = new List<WeeklyDrawings>();
            using (var reader = new StreamReader(fileLocation))
            {
                reader.ReadLine();
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    var wd = new WeeklyDrawings();
                    string[] values = line.Split(',');

                    string drawingDate;
                    //if(string.Parse(values[0], out drawingDate))
                    //{
                        wd.DrawingDate = values[0];
                    //}

                    int ball1;
                    if(int.TryParse(values[1], out ball1))
                    {
                        wd.BallOne = ball1;
                    }

                    int ball2;
                    if (int.TryParse(values[2], out ball2))
                    {
                        wd.BallTwo = ball2;
                    }

                    int ball3;
                    if (int.TryParse(values[3], out ball3))
                    {
                        wd.BallThree = ball3;
                    }

                    int ball4;
                    if (int.TryParse(values[4], out ball4))
                    {
                        wd.BallFour = ball4;
                    }

                    int ball5;
                    if (int.TryParse(values[5], out ball5))
                    {
                        wd.BallFive = ball5;
                    }

                    int MegaBall;
                    if (int.TryParse(values[6], out MegaBall))
                    {
                        wd.MegaBall = MegaBall;
                    }
                    weeklyDrawing.Add(wd);
                }            }
            return weeklyDrawing;
        }

        public static void DisplayDrawings(List<WeeklyDrawings> data)
        {
            Console.WriteLine("{0} \t\t{1} \t{2} \t{3} \t{4} \t{5} \t{6}", "Date", "Ball1", "Ball2", "Ball3", "Ball4", "Ball5", "Mega Ball") ;
            foreach(var dataItem in data)
            {
                Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6}", dataItem.DrawingDate, dataItem.BallOne, dataItem.BallTwo, dataItem.BallThree, dataItem.BallFour, dataItem.BallFive, dataItem.MegaBall);
            }
        }

        public static void GetAllFiveBalls(List<WeeklyDrawings> rawData)
        {
            Console.WriteLine("How Many latest Drawing to Pick : ");
            int pick = Int32.Parse(Console.ReadLine());
            var balls = new List<int>();
            var megaBalls = new List<int>();

            var allBalls = new List<AllFiveBalls>();
            var allMegaBalls = new List<AllFiveBalls>();

            var data = (from b in rawData orderby b.DrawingDate descending select b).Take(pick);


            data.ToList().ForEach(x => balls.Add(x.BallOne));
            data.ToList().ForEach(x => balls.Add(x.BallTwo));
            data.ToList().ForEach(x => balls.Add(x.BallThree));
            data.ToList().ForEach(x => balls.Add(x.BallFour));
            data.ToList().ForEach(x => balls.Add(x.BallFive));

            data.ToList().ForEach(x => megaBalls.Add(x.MegaBall));
            
            int totalCounts = balls.Count();
            for(int i = 1; i <= 70; i++)
            {
                var allFiveBalls = new AllFiveBalls();
                allFiveBalls.Ball = i;
                int x = balls.Where(b => b.ToString() == i.ToString()).Count();
                allFiveBalls.BallCounts = x;
                allBalls.Add(allFiveBalls);
            }

            for(int i = 1; i <= 25; i++)
            {
                var mBall = new AllFiveBalls();
                int x = megaBalls.Where(b => b.ToString() == i.ToString()).Count();
                mBall.Ball = i;
                mBall.BallCounts = x;
                allMegaBalls.Add(mBall);
            }
            //var ballcounts = allBalls.OrderByDescending(b => b.BallCounts);

            Console.WriteLine("{0} \t {1}", "Ball", "Count");
            foreach(var ballCount in allBalls.OrderByDescending(o => o.BallCounts))
            {
                Console.WriteLine("{0} \t {1}", ballCount.Ball, ballCount.BallCounts);
            }
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Mega Balls");
            Console.WriteLine();
            Console.WriteLine();
            foreach (var ballCount in allMegaBalls.OrderByDescending(o => o.BallCounts))
            {
                Console.WriteLine("{0} \t {1}", ballCount.Ball, ballCount.BallCounts);
            }

        }

    }
}
