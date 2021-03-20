using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        public static IEnumerable<T> Swap<T>(this IEnumerable<T> source, int i, int j)
        {
            T[] elements = source.ToArray();

            T temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;

            foreach (T element in elements)
                yield return element;
        }

        public static IEnumerable<T> SkipAll<T>(this IEnumerable<T> source, T skip, IComparer<T> comparer)
        {
            foreach (T element in source)
            {
                if (comparer.Compare(element, skip) == 0)
                    continue;

                yield return element;
            }
        }
    }
}
