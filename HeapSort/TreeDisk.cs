using System.Numerics;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.IO;
using static System.Math;
namespace HeapSort
{
    class TreeDisk : Tree
    {
        int size;
        FileStream fileStreamTree;
        BinaryWriter writerTree;
        BinaryReader readerTree;
        FileStream fileStreamData;
        BinaryWriter writerData;
        BinaryReader readerData;
        public TreeDisk(string OperationalFileDir) : base()
        {
            size = 0;
            fileStreamTree = File.Open($"{OperationalFileDir}tree.dat", FileMode.Create);
            writerTree = new BinaryWriter(fileStreamTree);
            readerTree = new BinaryReader(fileStreamTree);
            fileStreamData = File.Open($"{OperationalFileDir}objects.dat", FileMode.Create);
            writerData = new BinaryWriter(fileStreamData);
            readerData = new BinaryReader(fileStreamData);
        }

        public override int GetLength()
        {
            return size;
        }
        public override void Swap(int i, int j)
        {
            int iNodePlace = GetNodeAdress(i);
            int jNodePlace = GetNodeAdress(j);
            fileStreamTree.Seek(iNodePlace, SeekOrigin.Begin);
            int iDataAddress = readerTree.ReadInt32();
            fileStreamTree.Seek(jNodePlace, SeekOrigin.Begin);
            int jDataAddress = readerTree.ReadInt32();
            fileStreamTree.Seek(iNodePlace, SeekOrigin.Begin);
            writerTree.Write(jDataAddress);
            fileStreamTree.Seek(jNodePlace, SeekOrigin.Begin);
            writerTree.Write(iDataAddress);
        }
        public override void FillFromFile(string fileDir)
        {
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(fileDir))
            {
                if (!file.EndOfStream)
                {
                    add(new DatePrice(file.ReadLine()));
                }
                for (int i = 1; !file.EndOfStream; i++)
                {
                    DatePrice left = new DatePrice(file.ReadLine());
                    if (!file.EndOfStream)
                    {
                        DatePrice right = new DatePrice(file.ReadLine());
                        add(GetNodeAdress(i), left, right);
                    }
                    else
                    {
                        add(GetNodeAdress(i), left);
                    }
                }
            }
        }
        public override void PrintToFile(string fileDir)
        {
            fileStreamTree.Seek(0, SeekOrigin.Begin);
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileDir))
            {
                for (int i = 1; i <= size; i++)
                {

                    file.WriteLine(Get(i));
                }
            }
        }
        public void add(int parentNodeAddress, DatePrice left, DatePrice right)
        {
            fileStreamData.Seek(size * 20, SeekOrigin.Begin);
            writerData.Write(left.date); //writes data of left element
            writerData.Write(left.price);
            writerData.Write(right.date); //writes data of right element
            writerData.Write(right.price);
            fileStreamTree.Seek(parentNodeAddress + 4, SeekOrigin.Begin);
            writerTree.Write(size * 12); //writes address of left node
            writerTree.Write((size + 1) * 12); //writes address of right node
            fileStreamTree.Seek(size * 12, SeekOrigin.Begin); 
            writerTree.Write(size * 20); //creates left node
            writerTree.Write(0);
            writerTree.Write(0);
            writerTree.Write((size + 1) * 20); //creates right node
            writerTree.Write(0);
            writerTree.Write(0);
            size += 2;
        }
        public void add(int parentNodeAddress, DatePrice left)
        {
            fileStreamData.Seek(size * 20, SeekOrigin.Begin);
            writerData.Write(left.date); //writes data of left element
            writerData.Write(left.price);
            fileStreamTree.Seek(parentNodeAddress + 4, SeekOrigin.Begin);
            writerTree.Write(size * 12); //writes address of left node
            fileStreamTree.Seek(size * 12, SeekOrigin.Begin);
            writerTree.Write(size * 20); //creates left node
            writerTree.Write(0);
            writerTree.Write(0);
            size++;
        }
        public void add(DatePrice obj)
        {
            fileStreamData.Seek(size * 20, SeekOrigin.Begin);
            writerData.Write(obj.date); //writes data of element
            writerData.Write(obj.price);
            fileStreamTree.Seek(size * 12, SeekOrigin.Begin); //creates node
            writerTree.Write(size * 20);
            writerTree.Write(0);
            writerTree.Write(0);
            size++;
        }
        public override DatePrice Get(int index)
        {
            int nodeAddres = GetNodeAdress(index);
            fileStreamTree.Seek(nodeAddres, SeekOrigin.Begin);
            int dataAddress = readerTree.ReadInt32();
            fileStreamData.Seek(dataAddress, SeekOrigin.Begin);
            return new DatePrice(readerData.ReadInt32(), readerData.ReadDecimal());
        }
        int GetNodeAdress(int index)
        {
            if (index > size)
            {
                throw new IndexOutOfRangeException();
            }
            int rez = 0;
            int height = (int)Math.Ceiling(Math.Log(index + 1, 2));
            int lastRowFill = index - ((int)Math.Pow(2, height - 1) - 1);
            int lastRowSize = (int)Math.Pow(2, height - 1);
            double lastRowFillRatio = (double)lastRowFill / lastRowSize;
            double compareTo = 0.5;
            for (int i = 1; i < height; i++)
            {
                if (lastRowFillRatio <= compareTo)
                {
                    fileStreamTree.Seek(rez + 4, SeekOrigin.Begin);
                    rez = readerTree.ReadInt32();
                }
                else
                {
                    fileStreamTree.Seek(rez + 8, SeekOrigin.Begin);
                    rez = readerTree.ReadInt32();
                    lastRowFillRatio -= compareTo;
                }
                compareTo /= 2;
            }
            return rez;
        }
    }
}