using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.GraphModel
{
    public abstract class Link
    {
        public Uri Uri;

        public Link(Uri uri)
        {
            Uri = uri;
        }

        public string Key { get { return Uri.ToString(); } }
        public bool PursuePath { get; }
        public virtual string Kind { get; }
    }
}
