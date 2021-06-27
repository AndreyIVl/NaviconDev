var Navicon = Navicon || {};

Navicon.navde_new_credit = (function () {

    var validateCreditDate = function (context) 
    {
        let formContext = context.getFormContext();
        let startDate = formContext.getAttribute("navde_new_datestart").getValue();
        let endDate = formContext.getAttribute("navde_new_dateend").getValue();            
           
        if (checkYer(endDate,startDate)) 
        {
            return;
        }
        else
        {
            alert("Кредитная программма не может быть меньше года!");
            context.getEventArgs().preventDefault();
        }
    }
    function checkYer(dateStart, dateEnd)
    {
        if(dateEnd.getYear() - dateStart.getYear()>1)
        {
            return true
        }
        else
        {
            if(checkMounth(dateEnd.getMonth, dateStart.getMonth))
            {
                return true;
            }
            else return false;
        }
    }
    function checkMounth(dateEnd, dateStart)
    {
        if(dateEnd-dateStart >= 0)
        {
            return true;
        }
        return false;
    }

    return {
        onSave: function (context) 
        {
            validateCreditDate(context);
        }
    }
})();