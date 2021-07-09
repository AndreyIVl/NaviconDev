using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Workflows.AgreementActivity
{
    class InvoiceHandler
    {
        private readonly IOrganizationService _service;
        public InvoiceHandler(IOrganizationService service)
        {
            _service = service;
        }
        public bool IsHavePaymentStatusPaid(Guid agreementId)
        {
            bool allCondition = false;
            QueryExpression query = new QueryExpression("navde_new_invoice");
            query.Criteria.AddCondition("navde_new_dogovorid", ConditionOperator.Equal, agreementId);
            var result = _service.RetrieveMultiple(query);
            foreach(var invoice in result.Entities)
            {
                if (invoice.GetAttributeValue<bool>("navde_new_fact") == true || invoice.GetAttributeValue<bool>("navde_new_type") == false)
                {
                    return true;
                }                
            }
            return allCondition;
        }
        public void DeleteInvoice(Guid agreementId)
        {
            if(!IsHavePaymentStatusPaid(agreementId))
            {
                QueryExpression query = new QueryExpression("navde_new_invoice");
                query.Criteria.AddCondition("navde_new_dogovorid", ConditionOperator.Equal, agreementId);
                var result = _service.RetrieveMultiple(query);
                foreach (var invoice in result.Entities)
                {
                    if (invoice.GetAttributeValue<bool>("navde_new_type") == true)
                    {
                        DeleteInvoiceById(invoice.Id);
                    }
                }
            }
        }
        public void UpdateInvoiceCreditDataPlan(Guid id)
        {
            var agreement = _service.Retrieve("navde_new_agreement",id,new ColumnSet("navde_new_paymentplandate"));
            agreement["navde_new_paymentplandate"] = DateTime.Now.AddDays(1);
        }
        private Entity GetInvoiceById(Guid id)
        {
            return _service.Retrieve("",id,new ColumnSet(true));
        }
        private void DeleteInvoiceById(Guid id)
        {
            _service.Delete("navde_new_invoice",id);
        }
    }
}
