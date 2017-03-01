using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : BasePage
{
    private string CultureLang = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CultureLang = Session["PreferredCulture"].ToString().ToLower().Trim();
            switch (CultureLang)
            {
                case "zh-cn":
                    this.DropDownList1.Items.FindByText("简体中文").Selected = true;
                    Session["PreferredCulture"] = "zh-cn";
                    break;
                case "en-us":
                    this.DropDownList1.Items.FindByText("English").Selected = true;
                    Session["PreferredCulture"] = "en-us";
                    break;
                default:
                    break;
            }
            Session["search_condition"] = "off";
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        string username = this.txtUserName.Text.Trim();
        string password = this.txtPwd.Text.Trim();
        SqlDataReader dr = db.reDr("select u.no,u.username,u.passowrd,u.department_id,u.role,d.department_name from points_management_DB_user as u inner join points_management_DB_department as d on u.department_id=d.no where username='" + username + "' and passowrd='" + password + "'");
        dr.Read();
        if (dr.HasRows)
        {
            Session["no"] = dr.GetValue(0);
            Session["username"] = dr.GetValue(1);
            Session["department_id"] = dr.GetValue(3);
            Session["role"] = dr.GetValue(4);
            Session["department_name"] = dr.GetValue(5);
            if ((Boolean)dr.GetValue(4))
                Response.Redirect("~/Main.aspx");
            else
                Response.Redirect("~/PersonalMain.aspx");
        }
        else
        {
            Response.Write("<script>alert('login failure');location='Login.aspx'</script>");
        }
        dr.Close();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PreferredCulture"] = DropDownList1.SelectedValue;
        Response.Redirect(Request.Url.PathAndQuery);
    }
}