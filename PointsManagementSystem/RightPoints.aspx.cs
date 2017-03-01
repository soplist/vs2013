using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RightPoints : System.Web.UI.Page
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

        string sql = "select fill_user,sum(case when point_value > 0 then point_value else 0 end) as a," +
                     "sum(case when point_value < 0 then point_value else 0 end) as b " +
                     "from points_management_DB_points as p inner join points_management_DB_user as u on p.user_id=u.no " +
                     "where operate_time between '" + startEventTime + "' and '" + endEventTime + "' "+
                     "group by fill_user";

        DB db = new DB();
        DataTable dataTable = db.reDt(sql);

        this.points.DataSource = dataTable;
        this.points.DataBind();
    }
}