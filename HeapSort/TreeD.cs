using System;
using System.IO;

namespace HeapSort
{
    class TreeD : Tree{
        string operationalFileDir;
        BinaryWriter writer;
        BinaryReader reader;
        int length;
        public TreeD(string OperationalFileDir) : base(){
            operationalFileDir = OperationalFileDir;
            FileStream fileStream = File.Open(operationalFileDir, FileMode.Create);
            writer = new BinaryWriter(fileStream);
            reader = new BinaryReader(fileStream);
        }
        public override int GetLength()
        {
            return length;
        }
        public override DatePrice GetByIndex(int index)
        {
            reader.BaseStream.Seek(index * 20, SeekOrigin.Begin);
            return new DatePrice(reader.ReadInt32(), reader.ReadDecimal());
        }
        public override void FillFromFile(string fileDir)
        {
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(fileDir))
            {
                while (!file.EndOfStream)
                {
                    DatePrice tempDP = new DatePrice(file.ReadLine());

                    writer.Write(tempDP.date);
                    writer.Write(tempDP.price);
                    length++;
                }
            }
        }
        public override void PrintToFile(string fileDir)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fileDir))
            {
                for (int i = 0; i < length; i++)
                {
                    file.WriteLine(new DatePrice(reader.ReadInt32(), reader.ReadDecimal()));
                }
            }
        }
        public override void Swap(int a, int b)
        {
            int tempI_a;
            Decimal tempD_a;
            int tempI_b;
            Decimal tempD_b;
            
            reader.BaseStream.Seek(a * 20, SeekOrigin.Begin);
            tempI_a = reader.ReadInt32();
            tempD_a = reader.ReadDecimal();
            reader.BaseStream.Seek(b * 20, SeekOrigin.Begin);
            tempI_b = reader.ReadInt32();
            tempD_b = reader.ReadDecimal();

            writer.Seek(a * 20, SeekOrigin.Begin);
            writer.Write(tempI_b);
            writer.Write(tempD_b);
            writer.Seek(b * 20, SeekOrigin.Begin);
            writer.Write(tempI_a);
            writer.Write(tempD_a);
        }
    }
}
