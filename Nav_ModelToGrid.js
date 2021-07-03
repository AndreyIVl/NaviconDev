var Navicon = Navicon || {};
Navicon.fillGrid = (function(){

    function marksToCreditMapping(context) {
        var id = Xrm.Page.data.entity.getId();
        var fetchData = {
            navde_marksid: id
        };

        let formContext = context.getFormContext();
        let resourceControll = formContext.getControl("WebResource_mark_grid").getContentWindow();
            
        var fetchXml = [
    "<fetch top='50'>",
    "  <entity name='navde_new_credit'>",
    "    <attribute name='navde_new_creditperiod' />",
    "    <attribute name='navde_new_creditid' />",
    "    <attribute name='navde_name' />",
    "    <link-entity name='navde_navde_new_credit_navde_new_auto' from='navde_new_creditid' to='navde_new_creditid'>",
    "      <link-entity name='navde_new_auto' from='navde_new_autoid' to='navde_new_autoid' link-type='inner'>",
    "        <attribute name='navde_new_autoid' />",
    "        <attribute name='navde_new_modelid' />",
    "        <link-entity name='navde_marks' from='navde_marksid' to='navde_new_brandid' link-type='inner'>",
    "          <attribute name='navde_markname' />",
    "          <attribute name='navde_marksid' />",
    "          <filter>",
    "            <condition attribute='navde_marksid' operator='eq' value='", fetchData.navde_marksid, "'/>",
    "          </filter>",
    "        </link-entity>",
    "      </link-entity>",
    "    </link-entity>",
    "  </entity>",
    "</fetch>",
        ].join("");
        fetchXml = "?fetchXml=" + encodeURIComponent(fetchXml);

        Xrm.WebApi.retrieveMultipleRecords("navde_new_credit", fetchXml).then(
            function success(result) 
            {
                resourceControll.then(function(contentWindow){
                
                    console.error("chek arrray entities ======== \n " + result.entities);
                    contentWindow.documentOnLoad(result.entities);

                    for (var i = 0; i < result.entities.length; i++) 
                    {
                        console.error(result.entities[i]);
                    }
                },
                function(error)
                {
                    console.error(error.message);
                })                
            },
            function (error) {
                console.eror(error.message);
            }
        );
    }
    return{

        onLoad: function(context)
        { 
            marksToCreditMapping(context);
        }
    }
})();
