using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    /// <summary>
    /// The Beatline is an in memory storage the recent history of node statuses as checked
    /// in each crawl. Each crawl is a beat, so the beatline stores the lists of beats.
    /// The Beatline itself is supposed to be overriden by subclasses and not used directly, as
    /// it would grow withoout bounds.
    /// </summary>
    public class Beatline : IList<Beat>
    {
        public long SessionId { get; set; }

        public virtual int Count => ((IList<Beat>)store).Count;

        public bool IsReadOnly => ((IList<Beat>)store).IsReadOnly;

        public virtual Beat this[int index] { get => ((IList<Beat>)store)[index]; set => ((IList<Beat>)store)[index] = value; }

        protected List<Beat> store;

        public Beatline()
        {
            store = new List<Beat>();
        }

        public virtual int IndexOf(Beat item)
        {
            return ((IList<Beat>)store).IndexOf(item);
        }

        public virtual void Insert(int index, Beat item)
        {
            ((IList<Beat>)store).Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            ((IList<Beat>)store).RemoveAt(index);
        }

        public virtual void Add(Beat item)
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

        public virtual bool Remove(Beat item)
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
