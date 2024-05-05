using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12_1
{
    public class Point<T>
    {
        public T? Data { get; set; }
        public Point<T>? Next { get; set; }
        public Point<T>? Prev { get; set; }

        public Point()
        {
            this.Data = default;
            this.Next = null;
            this.Prev = null;
        }

        public Point(T data)
        {
            this.Data= data;
            this.Next = null;
            this.Prev = null;
        }

        public override string ToString()
        {
            return Data == null ? "": Data.ToString();
        }
    }

}
