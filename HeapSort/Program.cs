
using System.IO;
using System;
using System.Diagnostics;
namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {

            string memoryMode;
            string inDir = @"C:\Users\swifty\Desktop\HeapSort\dataFiles\long.csv";;
            string outDir = @"C:\Users\swifty\Desktop\HeapSort\dataFiles\out.csv";;
            string[] inFiles1 = {"25600.csv","51200.csv","102400.csv","204800.csv", "409600.csv"};
            string[] inFiles2 = {"800.csv","1600.csv","3200.csv","6400.csv","12800.csv"};
            
            do
            {
                Console.WriteLine("Use op / d memory or test?");
                memoryMode = Console.ReadLine();

            } while (!(memoryMode == "op" || memoryMode == "d" || memoryMode == "test"));

            if (memoryMode == "op")
            {
                TreeOperational treeOP = new TreeOperational();
                treeOP.FillFromFile(inDir);
                treeOP.HeapSort();
                treeOP.PrintToFile(outDir);
            }
            else if(memoryMode == "d")
            {
                TreeDisk treeD = new TreeDisk(@"../dataFiles/");
                treeD.FillFromFile(inDir);
                treeD.HeapSort();
                treeD.PrintToFile(outDir);
            } else{
                inDir = @"C:\Users\swifty\Desktop\HeapSort\dataFiles";
                testOP(inDir,inFiles1);
                testD(inDir,inFiles2,@"../dataFiles/");
            }
            Console.ReadLine();
        }

        static void testOP(string inDir, string[] inFiles){
            Stopwatch stopWatch = new Stopwatch();
            TreeOperational treeOP;
            Console.WriteLine("Heapsort test with operational memory");
            Console.WriteLine("  N      time elapsed    compare count    swap count");
            foreach (string item in inFiles)
            {
                treeOP = new TreeOperational();
                treeOP.FillFromFile($@"{inDir}\{item}");
                stopWatch.Start();

                treeOP.HeapSort();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string rezults = $"{treeOP.GetLength(),7} {ts} {treeOP.compareCount, 12} {treeOP.swapCount, 12}";
                Console.WriteLine(rezults);
                stopWatch.Reset();
            }
        }
        static void testD(string inDir, string[] inFiles, string externalFileDir){
            Stopwatch stopWatch = new Stopwatch();
            TreeDisk treeD;
            Console.WriteLine("Heapsort test with external memory");
            Console.WriteLine("  N      time elapsed    compare count    swap count");
            foreach (string item in inFiles)
            {
                treeD = new TreeDisk(@"../dataFiles/");
                treeD.FillFromFile($@"{inDir}\{item}");
                stopWatch.Start();

                treeD.HeapSort();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string rezults = $"{treeD.GetLength(),7} {ts} {treeD.compareCount, 12} {treeD.swapCount, 12}";
                Console.WriteLine(rezults);
                stopWatch.Reset();
                treeD.EndStreams();
            }
            Console.ReadLine();
        }
    }
}
