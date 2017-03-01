using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PersonalMain : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["no"] == null || Session["no"] == "")
            Response.Redirect("~/Login.aspx");
        if (Session["search_condition"] == null || Session["search_condition"] == "")
            Session["search_condition"] = "off";
        if (!Page.IsPostBack)
        {
            initDataWithSearchConditionOff();
        }

    }


    public void initDataWithSearchConditionOff()
    {
        DB db = new DB();
        int pageSize = 100;
        Session["page_size"] = pageSize;
        int user_id = Convert.ToInt32(Session["no"]);
        string countSql = "select count(*) from points_management_DB_points where user_id=" + user_id;
        int count = db.getFristValue(countSql);
        int pageCount = (int)Math.Ceiling((double)count / pageSize);
        Session["page_count"] = pageCount;

        this.currentPage.Text = "1";
        this.totalPage.Text = Convert.ToString(pageCount);

        bindWithSearchConditionOff();
    }

    public void initDataWithSearchConditionOn()
    {
        DB db = new DB();
        int pageSize = 100;
        Session["page_size"] = pageSize;
        int user_id = Convert.ToInt32(Session["no"]);
        string sqlWhereCondition = constructionOfWhereCondition(false);
        string countSql = "select count(*) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition+" and user_id="+user_id;
        int count = db.getFristValue(countSql);
        int pageCount = (int)Math.Ceiling((double)count / pageSize);
        Session["page_count"] = pageCount;

        this.currentPage.Text = "1";
        this.totalPage.Text = Convert.ToString(pageCount);

        bindWithSearchConditionOn();
    }

    public string constructionOfWhereCondition(Boolean withHead)
    {
        string eventTime = this.txtEventTime.Text.Trim();
        string startEventTime = this.txtStartEventTime.Text.Trim();
        string endEventTime = this.txtEndEventTime.Text.Trim();

        string sqlWhereCondition = "";
        if (withHead)
        {
            sqlWhereCondition = " where 1=1";
        }
        if (eventTime != "")
        {
            sqlWhereCondition += " and CONVERT(CHAR(20),p.event_time,23) like '" + eventTime + "%'";
        }
        if (startEventTime != "" && endEventTime != "")
        {
            sqlWhereCondition += " and operate_time between '" + startEventTime + "' and '" + endEventTime + "'";
        }

        return sqlWhereCondition;
    }

    public void bindWithSearchConditionOn()
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "on")
        {
            string sqlWhereCondition = constructionOfWhereCondition(false);
            string sqlWhereConditionWithHead = constructionOfWhereCondition(true);

            DB db = new DB();
            int pageSize = Convert.ToInt32(Session["page_size"]);
            int curpage = Convert.ToInt32(this.currentPage.Text);
            int user_id = Convert.ToInt32(Session["no"]);
            string dataSql = "";
            if (curpage == 1)
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereConditionWithHead + " and p.user_id = " + user_id + " order by p.no desc";
            else
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no where p.no not in(select top " + pageSize * (curpage - 1) + "  p.no from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereConditionWithHead + " and p.user_id=" + user_id + " order by p.no desc)" + sqlWhereCondition + " and p.user_id=" + user_id + " order by p.no desc";
            DataTable pageDataTable = db.reDt(dataSql);

            checkPageBtnStatus(curpage);

            this.points.DataSource = pageDataTable;
            this.points.DataKeyField = "no";
            this.points.DataBind();

        }
    }

    public void bindWithSearchConditionOff()
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            DB db = new DB();
            int pageSize = Convert.ToInt32(Session["page_size"]);
            int curpage = Convert.ToInt32(this.currentPage.Text);
            int user_id = Convert.ToInt32(Session["no"]);
            string dataSql = "";
            if (curpage == 1)
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + " where p.user_id="+user_id+" order by p.no desc";
            else
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no where p.no not in(select top " + pageSize * (curpage - 1) + "  p.no from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no where p.user_id="+user_id+" order by p.no desc) and p.user_id="+user_id+" order by p.no desc";
            DataTable pageDataTable = db.reDt(dataSql);

            checkPageBtnStatus(curpage);

            this.points.DataSource = pageDataTable;
            this.points.DataKeyField = "no";
            this.points.DataBind();
        }

    }

    public void checkPageBtnStatus(int currentPage)
    {
        this.lnkbtnUp.Enabled = true;
        this.lnkbtnNext.Enabled = true;
        this.lnkbtnBack.Enabled = true;
        this.lnkbtnOne.Enabled = true;
        if (currentPage == 1)
        {
            this.lnkbtnOne.Enabled = false;
            this.lnkbtnUp.Enabled = false;
        }

        int pageCount = Convert.ToInt32(Session["page_count"]);
        if (currentPage >= pageCount)
        {
            this.lnkbtnNext.Enabled = false;
            this.lnkbtnBack.Enabled = false;
        }
    }

    protected void lnkbtnOne_Click(object sender, EventArgs e)
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            this.currentPage.Text = "1";
            this.bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            this.currentPage.Text = "1";
            this.bindWithSearchConditionOn();
        }
    }
    protected void lnkbtnUp_Click(object sender, EventArgs e)
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.currentPage.Text) - 1);
            this.bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.currentPage.Text) - 1);
            this.bindWithSearchConditionOff();
        }
    }
    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.currentPage.Text) + 1);
            this.bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.currentPage.Text) + 1);
            this.bindWithSearchConditionOn();
        }
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            this.currentPage.Text = this.totalPage.Text;
            this.bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            this.currentPage.Text = this.totalPage.Text;
            this.bindWithSearchConditionOn();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            Session["search_condition"] = "on";
        }
        initDataWithSearchConditionOn();
        buildStatisticalInformation();
    }

    protected void buildStatisticalInformation()
    {
        DB db = new DB();
        int user_id = Convert.ToInt32(Session["no"]);
        string sqlWhereCondition = constructionOfWhereCondition(true);
        string sumSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and user_id=" + user_id;
        string addSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and p.point_value>0" + " and user_id=" + user_id;
        string minusSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and p.point_value<0" + " and user_id=" + user_id;
        int sum = db.getFristValue(sumSql);
        int add = db.getFristValue(addSql);
        int minus = db.getFristValue(minusSql);
        this.labSum.Text = "" + sum;
        this.labAdd.Text = "" + add;
        this.labMinus.Text = "" + minus;
    }
    protected void btnTurnOffSearch_Click(object sender, EventArgs e)
    {
        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "on")
        {
            Session["search_condition"] = "off";
        }
        initDataWithSearchConditionOff();
    }
}