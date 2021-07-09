using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using Test.Workflows.AgreementActivity.AgreementHandler;

namespace Test.Workflows.AgreementActivity
{
    public class AgreementActivity : CodeActivity
    {
        [Input("Agreement")]
        [RequiredArgument]
        [ReferenceTarget("navde_new_agreement")]
        public InArgument<EntityReference> agreementId { get; set; }
        [Output("Is agreement have Invoice")]
        public OutArgument<bool> isHaveInvoice { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(Guid.Empty);
            var traceService = context.GetExtension<ITracingService>();
            try
            {
                AgreementHandlers agreement = new AgreementHandlers(service);
                isHaveInvoice.Set(context,agreement.IsHaveInvoice(agreementId.Get(context).Id));
            }
            catch(Exception e)
            {
                traceService.Trace(e.ToString());
            }
        }
    }
}
