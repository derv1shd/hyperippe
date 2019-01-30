using System;
using System.Collections.Generic;

namespace Hyperippe.GraphModel
{
    public class Node
    {
        #region Currently used properties/methods/variables
        public Node(string uriString)
        {
            Uri = new Uri(uriString ?? throw new ArgumentNullException(nameof(uriString)));
            Links = new List<Link>();
        }

        public Node(Uri uri)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            Links = new List<Link>();
        }

        public string Key { get { return Uri.ToString(); } }
        public Uri Uri { get; }
        public List<Link> Links;
        #endregion

        #region Future properties/methods/variables thought of but not used or implemented
        public string Name { get { return Uri.ToString(); } }
        public string MimeType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion
    }
}
