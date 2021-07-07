using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleAppNavicon
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Console.OutputEncoding = Encoding.UTF8;
            string connectionString = "AuthType=OAuth;Url=https://org12e54a09.crm4.dynamics.com/;username=ishmanov@ishmanovtrial.onmicrosoft.com;password=89872757088Banshee!!; RequireNewInstance=true;AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";
            CrmServiceClient client = new CrmServiceClient(connectionString);
            if (client.LastCrmException != null)
            {
                Console.WriteLine(client.LastCrmException.Message);
                Console.WriteLine(client.LastCrmError);
            }
            
            var agreementid =Guid.Parse("f28762cf-1fd9-eb11-bacb-000d3ab3c6ea");
            var service = (IOrganizationService)client;

            QueryExpression query = new QueryExpression("account");
            query.ColumnSet = new ColumnSet("navde_new_contact", "navde_new_date");
            var link = query.AddLink("navde_new_agreement", "navde_new_contact", "contactid");
            link.EntityAlias = "su";
            link.Columns = new ColumnSet("navde_new_date");
            var rez = client.RetrieveMultiple(query);
            foreach(var item in rez.Entities)
            {
                Console.WriteLine();
            }
            
            var rezultAgreement = client.Retrieve("navde_new_agreement", agreementid, new ColumnSet("navde_new_contact"));
            Console.WriteLine("rezult agreement - contactid = " );
            Console.ReadLine();


            //QueryExpression query = new QueryExpression("account");
            //query.Criteria.AddCondition("contactid", ConditionOperator.Equal, contactId);
            //var contact = client.Retrieve("account",
            //    (Guid)contactId,
            //    new ColumnSet("navde_new_date"));

            //if (client.RetrieveMultiple(query).Entities.Count < 0)
            //{
            //    var date = targetAgreement["navde_new_date"];
            //    contact["navde_new_date"] = date;
            //}
        }
        
    }
}
