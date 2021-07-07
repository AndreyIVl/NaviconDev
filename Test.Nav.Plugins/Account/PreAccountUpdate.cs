using Microsoft.Xrm.Sdk;
using System;

namespace Test.Nav.Plugins.Account
{
    public sealed class PreAccountUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var traceService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            traceService.Trace("gjke");

            var plugin = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            throw new InvalidPluginExecutionException("test");
        }
    }
}
