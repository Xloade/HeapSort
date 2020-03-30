using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HeapSort
{
    class TreeOP : Tree
    {
        DatePrice[] TreeArray;

        public TreeOP():base(){
        }
        public override int GetLength()
        {
            return TreeArray.Length;
        }
        public override DatePrice GetByIndex(int index)
        {
            if (TreeArray.Length > index)
            {
                return TreeArray[index];
            }
            else
            {
                System.Console.WriteLine("In DatePrice GetByIndex(int index) index = 0 ");
                return null;
            }
        }
        public override void Swap(int i, int j)
        {
            DatePrice sawpObject = TreeArray[i];
            TreeArray[i] = TreeArray[j];
            TreeArray[j] = sawpObject;
        }
        public override void PrintToFile(string fileDir)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileDir))
            {
                foreach (DatePrice item in TreeArray)
                {
                    file.WriteLine(item);
                }
            }
        }
        public override void FillFromFile(string fileDir)
        {
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(fileDir))
            {
                List<DatePrice> tempList = new List<DatePrice>();
                while (!file.EndOfStream)
                {
                    tempList.Add(new DatePrice(file.ReadLine()));
                }
                TreeArray = tempList.ToArray();
            }
        }
    }
}
