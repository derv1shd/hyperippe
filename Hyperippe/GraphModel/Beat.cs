using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.GraphModel
{
    public class Beat:IList<NodeState>
    {
        public long SessionId { get; }
        public int Id { get; }

        private List<NodeState> states = new List<NodeState>();
        private bool closed = false;

        public Beat(long sessionId, int beatId)
        {
            SessionId = sessionId;
            Id = beatId;
        }
        public int Count => ((IList<NodeState>)states).Count;

        public bool IsReadOnly => ((IList<NodeState>)states).IsReadOnly;

        public NodeState this[int index] { get => ((IList<NodeState>)states)[index]; set => ((IList<NodeState>)states)[index] = value; }

        public int IndexOf(NodeState item)
        {
            return ((IList<NodeState>)states).IndexOf(item);
        }

        public int IndexOf(Node item)
        {
            foreach (NodeState i in states)
            {
                if (i.Node.Equals(item))
                    return IndexOf(i);
            }
            return -1;
        }

        public void Insert(int index, NodeState item)
        {
            ((IList<NodeState>)states).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<NodeState>)states).RemoveAt(index);
        }

        public void Add(NodeState item)
        {
            ((IList<NodeState>)states).Add(item);
        }

        public void Clear()
        {
            ((IList<NodeState>)states).Clear();
        }

        public bool Contains(NodeState item)
        {
            return ((IList<NodeState>)states).Contains(item);
        }

        public bool Contains(Node item)
        {
            foreach (NodeState i in states)
            {
                if (i.Node.Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(NodeState[] array, int arrayIndex)
        {
            ((IList<NodeState>)states).CopyTo(array, arrayIndex);
        }

        public bool Remove(NodeState item)
        {
            return ((IList<NodeState>)states).Remove(item);
        }

        public IEnumerator<NodeState> GetEnumerator()
        {
            return ((IList<NodeState>)states).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<NodeState>)states).GetEnumerator();
        }
    }
}
