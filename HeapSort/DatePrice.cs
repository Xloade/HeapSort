using System;
using System.Collections.Generic;
using System.Text;

namespace HeapSort
{
    public class DatePrice : IComparable
    {
        public int date;
        public decimal price;
        public DatePrice(int date, decimal price)
        {
            this.date = date;
            this.price = price;
        }
        public DatePrice(string data)
        {
            string[] splitData = data.Split(new char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            this.date = dateConvrt(splitData[0]);
            this.price = decimal.Parse(splitData[1]);
        }
        public DatePrice(string rawDate, int price) : this(dateConvrt(rawDate), price)
        {
        }
        static int dateConvrt(string rawDate)
        {
            string[] splitDate = rawDate.Split('.');
            DateTime date = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
            return (date - new DateTime(1, 1, 1)).Days;
        }
        public override string ToString()
        {
            DateTime dateConverter = new DateTime(1, 1, 1).AddDays(date);
            return $"{dateConverter.Year}.{dateConverter.Month:D2}.{dateConverter.Day:D2} {price}";
        }
        public int CompareTo(object obj)
        {
            DatePrice dp = (DatePrice)obj;
            return price.CompareTo(dp.price);
        }

    }
}
