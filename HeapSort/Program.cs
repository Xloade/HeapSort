﻿using System;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string memoryMode;
            string inDir;
            string outDir;
            do
            {
                Console.WriteLine("Use OP / D memory?");
                memoryMode = Console.ReadLine();

            } while (!(memoryMode == "OP" || memoryMode == "D"));
            Console.WriteLine("Input file:");
            inDir = Console.ReadLine();
            Console.WriteLine("Output file:");
            outDir = Console.ReadLine();

            if(memoryMode == "OP"){
                TreeOP treeOP = new TreeOP();
                treeOP.FillFromFile(inDir);
                treeOP.HeapSort();
                treeOP.PrintToFile(outDir);
            }else{
                TreeD treeD = new TreeD(@"../../../../dataFiles/dinamicFile.dat");
                treeD.FillFromFile(inDir);
                treeD.HeapSort();
                treeD.PrintToFile(inDir);
            }


            //TreeOP treeOP = new TreeOP();
            //treeOP.FillFromFile(@"../../../../dataFiles/long.csv");
            //treeOP.HeapSort();
            //treeOP.PrintToFile(@"../../../../dataFiles/long_sorted_OP.csv");

            //TreeD treeD = new TreeD(@"../../../../dataFiles/dinamicFile.dat");
            //treeD.FillFromFile(@"../../../../dataFiles/long.csv");
            //treeD.HeapSort();
            //treeD.PrintToFile(@"../../../../dataFiles/long_sorted_D.csv");
        }
    }
}