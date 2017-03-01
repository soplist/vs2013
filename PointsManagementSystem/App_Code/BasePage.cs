using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage : Page
{
    public BasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    override
    protected void InitializeCulture()
    {
        if (Session["PreferredCulture"] == null)
        {
            Session["PreferredCulture"] = Request.UserLanguages[0];
        }
        string UserCulture = Session["PreferredCulture"].ToString();
        if (UserCulture != "")
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(UserCulture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(UserCulture);
        }
    }
        
}