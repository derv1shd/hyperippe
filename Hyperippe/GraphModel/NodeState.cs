using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.GraphModel
{
    public class NodeState
    {
        public Node Node { get; }
        public string Status { get; private set; }
        public bool ContentChanged { get; private set; }
        public bool LinksChanged { get; private set; }
        public string ContentType { get; private set; }
        public long ContentLength { get; private set; }

        public NodeState(Node node, string status, bool contentChanged, bool linksChanged, string contentType, long contentLength)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            ContentLength = contentLength;
        }

        public void Update(string status, bool contentChanged, bool linksChanged, string contentType, long contentLength)
        {
            Status = status;
            ContentChanged = contentChanged;
            LinksChanged = linksChanged;
            ContentType = contentType;
            ContentLength = contentLength;
        }

        public void Update(bool contentChanged, bool linksChanged)
        {
            ContentChanged = contentChanged;
            LinksChanged = linksChanged;
        }

        public void Update(string status)
        {
            Status = status;
        }
    }
}