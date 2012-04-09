using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar
{
    public static class CollectionEx
    {
        public static IList<T> DequeueAll<T>(this Queue<T> source)
        {
            List<T> result = new List<T>(source.Count);

            while (source.Count > 0)
            {
                result.Add(source.Dequeue());
            }

            return result;
        }

        public static IList<T> PopAll<T>(this Stack<T> source)
        {
            List<T> result = new List<T>(source.Count);

            while (source.Count > 0)
            {
                result.Add(source.Pop());
            }

            return result;
        }
    }
}
