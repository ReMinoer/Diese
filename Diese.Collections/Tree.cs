using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class Tree
    {
        static public IEnumerable<T> DepthFirst<T, TBase>(T root, Func<TBase, IEnumerable<T>> childrenSelector)
            where T : TBase
        {
            yield return root;
            foreach (T child in DepthFirst(root))
                yield return child;

            IEnumerable<T> DepthFirst(TBase parent)
            {
                foreach (T item in childrenSelector(parent))
                {
                    yield return item;

                    foreach (T child in DepthFirst(item))
                        yield return child;
                }
            }
        }

        static public IEnumerable<T> DepthFirstExclusive<T, TBase>(TBase ignoredRoot, Func<TBase, IEnumerable<T>> childrenSelector)
            where T : TBase
        {
            return DepthFirst(ignoredRoot);

            IEnumerable<T> DepthFirst(TBase parent)
            {
                foreach (T item in childrenSelector(parent))
                {
                    yield return item;

                    foreach (T child in DepthFirst(item))
                        yield return child;
                }
            }
        }

        static public IEnumerable<T> BreadthFirst<T>(T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            yield return root;
            foreach (T child in BreadthFirst(Enumerable<T>.New(root)))
                yield return child;

            IEnumerable<T> BreadthFirst(IEnumerable<T> parent)
            {
                var childrenLayer = new List<T>();

                foreach (T child in parent.SelectMany(childrenSelector))
                {
                    yield return child;
                    childrenLayer.Add(child);
                }

                if (childrenLayer.Count == 0)
                    yield break;

                foreach (T child in BreadthFirst(childrenLayer))
                    yield return child;
            }
        }

        static public IEnumerable<T> BreadthFirstExclusive<T, TBase>(TBase ignoredRoot, Func<TBase, IEnumerable<T>> childrenSelector)
            where T : TBase
        {
            return BreadthFirst(Enumerable<TBase>.New(ignoredRoot));

            IEnumerable<T> BreadthFirst(IEnumerable<TBase> parent)
            {
                var childrenLayer = new List<TBase>();

                foreach (T child in parent.SelectMany(childrenSelector))
                {
                    yield return child;
                    childrenLayer.Add(child);
                }

                if (childrenLayer.Count == 0)
                    yield break;

                foreach (T child in BreadthFirst(childrenLayer))
                    yield return child;
            }
        }
    }
}