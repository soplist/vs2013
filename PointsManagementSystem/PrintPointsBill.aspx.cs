using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintPointsBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string startEventTime = this.txtStartEventTime.Text.Trim();
        string endEventTime = this.txtEndEventTime.Text.Trim();

        //check
        if (startEventTime == "" || endEventTime == "")
        {
            Response.Write("<script>alert('input date');</script>");
            return;
        }

        string sql = "select u1.real_name as real_name_1,u2.real_name as real_name_2,fill_user,p.event,CONVERT(CHAR(20),p.operate_time,23) as time,p.point_value " +
                     "from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no " +
                     "where operate_time between '" + startEventTime + "' and '" + endEventTime + "' " +
                     "and p.point_value>0 " +
                     "and p.event not in (select event from points_management_DB_points " + "where operate_time between '" + startEventTime + "' and '" + endEventTime + "' " + " group by event having count(*)>1)";

        DB db = new DB();
        DataTable dataTable = db.reDt(sql);

        this.points.DataSource = dataTable;
        this.points.DataBind();
    }
}