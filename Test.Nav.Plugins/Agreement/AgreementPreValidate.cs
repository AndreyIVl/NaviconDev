using Microsoft.Xrm.Sdk;
using System;
using Test.Nav.Plugins.Agreement.Handler;

namespace Test.Nav.Plugins.Agreement
{
    class AgreementPreValidate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var traceService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(Guid.Empty);
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var targetAgreement = (Entity)pluginContext.InputParameters["Target"];

            try
            {
                AgreementService agreementService = new AgreementService(service);
                agreementService.AgreementSetFact(targetAgreement);
            }
            catch(Exception e)
            {
                traceService.Trace(e.ToString());
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
