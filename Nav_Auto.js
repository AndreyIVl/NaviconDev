var Navicon = Navicon || {};

Navicon.navde_new_auto = (function()
{    
    var autoUsedOnChange = function(context)
    {
        let formContext = context.getFormContext();
        let isUsed = formContext.getControl("navde_new_used").getValue();
        
        if(isUsed)
        {
            setVisible(context, true);
        }
        else setVisible(context, false);
        
    }
    var setVisible = function(context, visible)
    {
        let formContext = context.getFormContext();
        formContext.getControl("navde_new_ownerscount").setVisible(visible);
        formContext.getControl("navde_new_km").setVisible(visible);
        formContext.getControl("navde_new_isdamaged").setVisible(visible);
    }
    return{
        onLoad : function(context)
        {
            let formContext = context.getFormContext();
            let usedAttribute = formContext.getAttribute("navde_new_used");
            if(usedAttribute)
            {
                setVisible(context,true);
            }
            else setVisible(false);
            
            
            usedAttribute.addOnChange(autoUsedOnChange); 
        }
    }
})();