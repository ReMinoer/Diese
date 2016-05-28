using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Lintree
{
    static public class LintreeExtensions
    {
        static public IEnumerable<T> GetLeaves<T>(this T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            if (childrenSelector?.Invoke(root) == null || !childrenSelector(root).Any())
            {
                yield return root;
                yield break;
            }

            foreach (T child in childrenSelector(root))
                foreach (T leaf in child.GetLeaves(childrenSelector))
                    yield return leaf;
        }

        static public IEnumerable<T> UnstackTree<T>(this T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            if (childrenSelector?.Invoke(root) == null)
                yield break;

            foreach (T child in childrenSelector(root))
                yield return child;

            foreach (T child in childrenSelector(root))
                foreach (T subChild in child.UnstackTree(childrenSelector))
                    yield return subChild;
        }

        static public IEnumerable<T> UnqueueTree<T>(this T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            if (childrenSelector?.Invoke(root) == null)
                yield break;

            foreach (T child in childrenSelector(root))
                foreach (T subChild in child.UnqueueTree(childrenSelector))
                    yield return subChild;

            foreach (T child in childrenSelector(root))
                yield return child;
        }

        static public IEnumerable<T> InlineTree<T>(this T root, Func<T, IEnumerable<T>> childrenSelector)
        {
            if (childrenSelector?.Invoke(root) == null)
                yield break;

            foreach (T child in childrenSelector(root))
            {
                yield return child;
                foreach (T subChild in child.InlineTree(childrenSelector))
                    yield return subChild;
            }
        }
    }
}
