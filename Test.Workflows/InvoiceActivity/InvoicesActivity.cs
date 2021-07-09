using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Workflows.AgreementActivity;

namespace Test.Workflows.InvoiceActivity
{
    class InvoicesActivity : CodeActivity
    {
        [Input("Agreement")]
        [RequiredArgument]
        [ReferenceTarget("navde_new_agreement")]
        public InArgument<EntityReference> agrementId { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(Guid.Empty);
            var traceService = context.GetExtension<ITracingService>();
            try
            {
                var id = agrementId.Get(context).Id;
                InvoiceHandler handler = new InvoiceHandler(service);
                handler.IsHavePaymentStatusPaid(id);
                handler.DeleteInvoice(id);
                handler.UpdateInvoiceCreditDataPlan(id);
            }
            catch(Exception e)
            {
                traceService.Trace(e.ToString());
            }
        }
    }
}
