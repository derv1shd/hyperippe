using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.GraphModel
{
    public class NodeContent
    {
        public Node Node { get; }
        private string content;
        public string Content { get => content; }
        private string contentType;
        public string ContentType { get => contentType; }
        private long contentLength;
        public long ContentLength { get => contentLength; }

        public List<Link> Links = new List<Link>();

        public NodeContent(Node node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            content = string.Empty;
            contentType = string.Empty;
        }

        public NodeContent(Node node, string newContent, string newContentType, long newContentLegth)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            content = newContent ?? throw new ArgumentNullException(nameof(newContent));
            contentType = newContentType ?? throw new ArgumentNullException(nameof(newContentType));
        }

        public void Update(string newContent, string newContentType, long newContentLegth)
        {
            content = newContent;
            contentType = newContentType;
            contentLength = newContentLegth;
        }
    }
}
