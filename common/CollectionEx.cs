using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.ABI;
using Quasar.DCPU;

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

        public static ushort AssembledLength(this IEnumerable<IAssemblable> source)
        {
            return source
                .Select(op => op.AssembledLength)
                .Aggregate((ushort)0, MathsEx.Add);
        }


        public static IEnumerable<Label> Resolve(this IEnumerable<Label> source, ushort baseAddress)
        {
            return from label in source
                   select new Label(label.Name, label.Address.Pointer, true);
        }
    }
}
