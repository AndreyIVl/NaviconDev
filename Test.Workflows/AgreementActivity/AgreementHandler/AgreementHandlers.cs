using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Workflows.AgreementActivity.AgreementHandler
{
    class AgreementHandlers
    {
        private readonly IOrganizationService _service;
        public AgreementHandlers(IOrganizationService service)
        {
            _service = service;
        }
        public bool IsHaveInvoice(Guid id)
        {
            QueryExpression query = new QueryExpression("navde_new_agreement");
            query.Criteria.AddCondition("navde_new_agreementid", ConditionOperator.Equal, id);
          
            var result = _service.RetrieveMultiple(query);
            if (result.Entities.Count > 0)
            {
                return true;
            }
            else return false;
        }
        
    }
}
