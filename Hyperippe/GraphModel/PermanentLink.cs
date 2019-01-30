using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.GraphModel
{
    /// <summary>
    /// Unused class, this is thought to accomodate changes in behavior once links are treated with more complexity
    /// </summary>
    public class PermanentLink : Link
    {
        public PermanentLink(Uri uri) : base(uri)
        {
            throw new NotImplementedException();
        }
    }
}
