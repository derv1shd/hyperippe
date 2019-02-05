using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class Beatline : IList<Beat>
    {
        public long SessionId { get; set; }

        public int Count => ((IList<Beat>)store).Count;

        public bool IsReadOnly => ((IList<Beat>)store).IsReadOnly;

        public Beat this[int index] { get => ((IList<Beat>)store)[index]; set => ((IList<Beat>)store)[index] = value; }

        private List<Beat> store;

        public Beatline()
        {
            store = new List<Beat>();
        }

        public int IndexOf(Beat item)
        {
            return ((IList<Beat>)store).IndexOf(item);
        }

        public void Insert(int index, Beat item)
        {
            ((IList<Beat>)store).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Beat>)store).RemoveAt(index);
        }

        public void Add(Beat item)
        {
            ((IList<Beat>)store).Add(item);
        }

        public void Clear()
        {
            ((IList<Beat>)store).Clear();
        }

        public bool Contains(Beat item)
        {
            return ((IList<Beat>)store).Contains(item);
        }

        public void CopyTo(Beat[] array, int arrayIndex)
        {
            ((IList<Beat>)store).CopyTo(array, arrayIndex);
        }

        public bool Remove(Beat item)
        {
            return ((IList<Beat>)store).Remove(item);
        }

        public IEnumerator<Beat> GetEnumerator()
        {
            return ((IList<Beat>)store).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Beat>)store).GetEnumerator();
        }
    }
}
