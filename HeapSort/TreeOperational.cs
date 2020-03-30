using System;
using System.IO;
namespace HeapSort
{
    public class TreeOperational : Tree
    {
        int size = 0;
        Node root;

        public override int GetLength(){
            return size;
        }
        public override void Swap(int i, int j)
        {
            Node one = GetNodeByIndex(i);
            Node two = GetNodeByIndex(j);
            DatePrice temp;
            temp = one.obj;
            one.obj = two.obj;
            two.obj = temp;
        }
        public override void PrintToFile(string fileDir)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileDir))
            {
                for (int i = 1; i <= size; i++)
                {
                    file.WriteLine(GetByIndex(i));
                }
            }
        }
        public override void FillFromFile(string fileDir)
        {
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(fileDir))
            {
                if(!file.EndOfStream)
                {
                    root = new Node(new DatePrice(file.ReadLine()));
                    size++;
                }
                for (int i = 1; !file.EndOfStream; i++)
                {
                    DatePrice left = new DatePrice(file.ReadLine());
                    if(!file.EndOfStream){
                        DatePrice right = new DatePrice(file.ReadLine());
                        new Node(GetNodeByIndex(i), left, right);
                        size += 2;
                    }else{
                        new Node(GetNodeByIndex(i), left);
                        size++;
                    }
                }
            }
        }
        public override DatePrice GetByIndex(int index){
            return GetNodeByIndex(index).obj;
        }
        Node GetNodeByIndex(int index){
            if(index > size){
                return null;
            }
            Node rez = root;
            int height = (int)Math.Ceiling(Math.Log(index + 1, 2));
            int lastRowFill = index - ((int)Math.Pow(2, height - 1) - 1);
            int lastRowSize = (int)Math.Pow(2, height-1);
            double lastRowFillRatio = (double)lastRowFill / lastRowSize;
            double compareTo = 0.5;
            for (int i = 1; i < height; i++)
            {
                if(lastRowFillRatio <= compareTo){
                    rez = rez.left;
                }else{
                    rez = rez.right;
                    lastRowFillRatio -= compareTo;
                }
                compareTo /= 2;
            }
            return rez;
        }
        class Node{
            public Node left;
            public Node right;
            public DatePrice obj;

            public Node(DatePrice Obj){
                obj = Obj;
            }
            public Node(Node parent, DatePrice left, DatePrice right){
                parent.left = new Node(left);
                parent.right = new Node(right);
            }
            public Node(Node parent, DatePrice left){
                parent.left = new Node(left);
            }
        }
    }
}