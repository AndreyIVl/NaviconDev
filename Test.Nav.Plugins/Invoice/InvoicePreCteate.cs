using Microsoft.Xrm.Sdk;
using System;
using Test.Nav.Plugins.Invoice.Handlers;

namespace Test.Nav.Plugins.Invoice
{
    class InvoicePreCteate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var traceService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(Guid.Empty);
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var targetInvoice = (Entity)pluginContext.InputParameters["Target"];
            try
            {
                InvoiceService invoice = new InvoiceService(service);
                invoice.UpdateInvoiceType(targetInvoice);
            }
            catch(Exception e)
            {
                traceService.Trace(e.ToString());
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
