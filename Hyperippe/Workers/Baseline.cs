﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class NodeContent
    {
        public Node Node { get; }
        private string content;
        public string Content { get => content; }

        public NodeContent(Node node, string newContent)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            content = newContent ?? throw new ArgumentNullException(nameof(newContent));
        }

        public void Update(string newContent)
        {
            content = newContent;
        }
    }

    public class Baseline : IDictionary<string, NodeContent>
    {
        private Dictionary<string, NodeContent> store;

        public Baseline()
        {
            store = new Dictionary<string, NodeContent>();
        }

        public Baseline(List<string> uriList)
        {
            store = new Dictionary<string, NodeContent>();

            foreach (var item in uriList)
            {
                Node node = new Node(item);
                store.Add(node.Key, new NodeContent(node, string.Empty));
            }
        }

        public NodeContent this[string key] { get => store[key]; set => throw new InvalidOperationException(); }

        public ICollection<string> Keys => store.Keys;

        public ICollection<NodeContent> Values => store.Values;

        public int Count => store.Count;

        public bool IsReadOnly => false;

        public void Add(Node node, string content)
        {
            store.Add(node.Key, new NodeContent(node, content));
        }

        public void Add(string key, NodeContent value)
        {
            store.Add(key, value);
        }

        public void Add(KeyValuePair<string, NodeContent> item)
        {
            store.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            store.Clear();
        }

        public bool Contains(KeyValuePair<string, NodeContent> item)
        {
           return store.ContainsKey(item.Key);
        }

        public bool ContainsKey(string key)
        {
            return store.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, NodeContent>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, NodeContent>> GetEnumerator()
        {
            return store.GetEnumerator();
        }

        public void Update(string key, string newContent)
        {
            store[key].Update(newContent);
        }

        public void Update(KeyValuePair<string, NodeContent> item)
        {
            store[item.Key].Update(item.Value.Content);
        }

        public bool Remove(string key)
        {
            return store.Remove(key);
        }

        public bool Remove(KeyValuePair<string, NodeContent> item)
        {
            return store.Remove(item.Key);
        }

        public bool TryGetValue(string key, out NodeContent value)
        {
            return store.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return store.GetEnumerator();
        }
    }
}
