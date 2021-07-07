using Microsoft.Xrm.Sdk;
using System;
using Test.Nav.Plugins.Communication.Handler;

namespace Test.Nav.Plugins.Communication
{
    class CommunicationPreCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var traceService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(Guid.Empty);
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var target = (Entity)pluginContext.InputParameters["Target"];
            try
            {
                CommunicationService communication = new CommunicationService(service);
                communication.CheckDoubleRecords(target);
            }
            catch(Exception e)
            {
                traceService.Trace(e.ToString());
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
