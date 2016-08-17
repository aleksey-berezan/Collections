using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class FragmentedEnumerable<T> : IEnumerable<T>
    {
        private readonly LinkedList<T[]> _buckets;
        private readonly int _lastIndex;

        public FragmentedEnumerable(IEnumerable<T> items, int bucketSize = 64)
        {
            if (bucketSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bucketSize), bucketSize, "must be greater than zero");
            }

            int index = -1;
            var buckets = new LinkedList<T[]>();
            T[] bucket = buckets.AddLast(new T[bucketSize]).Value;

            foreach (var item in items)
            {
                index++;
                if (index >= bucketSize)
                {
                    bucket = buckets.AddLast(new T[bucketSize]).Value;
                    index = 0;
                }

                bucket[index] = item;
            }

            _buckets = buckets;
            _lastIndex = index;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_buckets.Count == 0)
            {
                yield break;
            }

            T[] lastBucket = _buckets.Last.Value;
            foreach (T[] bucket in _buckets)
            {
                int upperBound = bucket == lastBucket
                    ? _lastIndex + 1
                    : bucket.Length;
                for (int i = 0; i < upperBound; i++)
                {
                    yield return bucket[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

