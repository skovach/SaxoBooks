using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaxoBooks.Startup))]
namespace SaxoBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
