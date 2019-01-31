using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.GraphModel
{
    public class NodeState
    {
        public Node Node { get; }
        public string Status { get; }
        public bool ContentChanged { get; }
        public bool LinksChanged { get; }

        public NodeState(Node node, string status, bool contentChanged, bool linksChanged)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }
    }

    public class Beat:IList<NodeState>
    {
        public long SessionId { get; }
        public int Id { get; }
        private List<NodeState> states = new List<NodeState>();
        private bool closed = false;

        int ICollection<NodeState>.Count => states.Count;

        bool ICollection<NodeState>.IsReadOnly => !closed;

        NodeState IList<NodeState>.this[int index] { get => states[index]; set => throw new NotImplementedException(); }

        public Beat(long sessionId, int beatId)
        {
            SessionId = sessionId;
            Id = beatId;
        }

        int IList<NodeState>.IndexOf(NodeState item)
        {
            return states.IndexOf(item);
        }

        void IList<NodeState>.Insert(int index, NodeState item)
        {
            if(closed) throw new InvalidOperationException();

            states.Insert(index, item);
        }

        void IList<NodeState>.RemoveAt(int index)
        {
            if (closed) throw new InvalidOperationException();

            states.RemoveAt(index);
        }

        void ICollection<NodeState>.Add(NodeState item)
        {
            if (closed) throw new InvalidOperationException();

            states.Add(item);
        }

        void ICollection<NodeState>.Clear()
        {
            if (closed) throw new InvalidOperationException();

            states.Clear();
        }

        bool ICollection<NodeState>.Contains(NodeState item)
        {
            return states.Contains(item);
        }

        void ICollection<NodeState>.CopyTo(NodeState[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<NodeState>.Remove(NodeState item)
        {
            if (closed) throw new InvalidOperationException();

            return states.Remove(item);
        }

        IEnumerator<NodeState> IEnumerable<NodeState>.GetEnumerator()
        {
            return states.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return states.GetEnumerator();
        }
    }
}
