using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class Tree
    {
        static public IEnumerable<T> DepthFirst<T>(T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            yield return root;
            foreach (T child in DepthFirstExclusive(root, childrenSelector))
                yield return child;
        }

        static public IEnumerable<T> DepthFirstExclusive<T, TBase>(TBase ignoredRoot, Func<TBase, IEnumerable<T>> childrenSelector)
            where T : TBase
        {
            return DepthFirstAlgorithm(ignoredRoot);

            IEnumerable<T> DepthFirstAlgorithm(TBase parent)
            {
                foreach (T item in childrenSelector(parent))
                {
                    yield return item;

                    foreach (T child in DepthFirstAlgorithm(item))
                        yield return child;
                }
            }
        }

        static public IEnumerable<T> BreadthFirst<T>(T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            yield return root;
            foreach (T child in BreadthFirstExclusive(root, childrenSelector))
                yield return child;
        }

        static public IEnumerable<T> BreadthFirstExclusive<T, TBase>(TBase ignoredRoot, Func<TBase, IEnumerable<T>> childrenSelector)
            where T : TBase
        {
            return BreadthFirstAlgorithm(Enumerable<TBase>.New(ignoredRoot));

            IEnumerable<T> BreadthFirstAlgorithm(IEnumerable<TBase> parent)
            {
                var childrenLayer = new List<TBase>();

                foreach (T child in parent.SelectMany(childrenSelector))
                {
                    yield return child;
                    childrenLayer.Add(child);
                }

                if (childrenLayer.Count == 0)
                    yield break;

                foreach (T child in BreadthFirstAlgorithm(childrenLayer))
                    yield return child;
            }
        }
    }
}