using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace SaxoBooks.Infrastructure
{
    public class SaxoBooksContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; set; }

        public SaxoBooksContractResolver()
        {
            this.PropertyMappings = new Dictionary<string, string>
                {
                    {"isnb13", "isnb13"},
                    {"imageurl", "imageurl"},
                };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName = null;
            var resolved = this.PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}