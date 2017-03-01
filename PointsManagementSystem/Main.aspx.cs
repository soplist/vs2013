using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : BasePage
{
    private const int full_year_event_category = 11;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["no"] == null || Session["no"] == "")
            Response.Redirect("~/Login.aspx");
        if (Session["search_condition"] == null || Session["search_condition"] == "")
            Session["search_condition"] = "off";
        if (Session["export_switch"] == null || Session["export_switch"] == "")
            Session["export_switch"] = "off";
        if (!Page.IsPostBack)
        {
            initDataWithSearchConditionOff();
            dropDownListBindDepartments();
            initInsertData();
        }
        //61.150.109.162
    }

    public void dropDownListBindDepartments()
    {
        DB db = new DB();
        string deptSql = "select * from points_management_DB_department";
        DataTable dt = db.reDt(deptSql);
        lstDepartment.DataSource = dt;
        lstDepartment.DataValueField = "no";
        lstDepartment.DataTextField = "department_name";
        lstDepartment.DataBind();
        ListItem initItem = new ListItem();
        initItem.Text = "-please select-";
        initItem.Value = "0";
        lstDepartment.Items.Insert(0, initItem);
    }

    protected void initInsertData()
    {
        DB db = new DB();
        string deptSql = "select * from points_management_DB_department";
        DataTable dt_1 = db.reDt(deptSql);
        insert_lstDepartment.DataSource = dt_1;
        insert_lstDepartment.DataValueField = "no";
        insert_lstDepartment.DataTextField = "department_name";
        insert_lstDepartment.DataBind();
        ListItem initItem = new ListItem();
        initItem.Text = "-please select-";
        initItem.Value = "0";
        insert_lstDepartment.Items.Insert(0, initItem);

        string eventCategorySql = "select * from points_management_DB_event_category";
        DataTable dt_2 = db.reDt(eventCategorySql);
        insert_lstEventCategory.DataSource = dt_2;
        insert_lstEventCategory.DataValueField = "no";
        insert_lstEventCategory.DataTextField = "category";
        insert_lstEventCategory.DataBind();
    }

    public void initDataWithSearchConditionOff()
    {
        DB db = new DB();
        int pageSize = 100;
        Session["page_size"] = pageSize;
        string countSql = "select count(*) from points_management_DB_points";
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
        string sqlWhereCondition = constructionOfWhereCondition(false);
        string countSql = "select count(*) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition;
        int count = db.getFristValue(countSql);
        int pageCount = (int)Math.Ceiling((double)count / pageSize);
        Session["page_count"] = pageCount;

        this.currentPage.Text = "1";
        this.totalPage.Text = Convert.ToString(pageCount);

        bindWithSearchConditionOn();
    }

    public string constructionOfWhereCondition(Boolean withHead)
    {
        string name = this.txtName.Text.Trim();
        string startEventTime = this.txtStartEventTime.Text.Trim();
        string endEventTime = this.txtEndEventTime.Text.Trim();
        string operateTime = this.txtOperateTime.Text.Trim();
        string department = this.lstDepartment.SelectedValue.ToString();

        string sqlWhereCondition = "";
        if (withHead)
        {
            sqlWhereCondition = " where 1=1";
        }
        if (name != "")
        {
            sqlWhereCondition += " and u1.real_name='" + name + "'";
        }
        if (startEventTime != "" && endEventTime != "")
        {
            sqlWhereCondition += " and operate_time between '" + startEventTime + "' and '" + endEventTime + "'";
        }
        if (operateTime != "")
        {
            sqlWhereCondition += " and CONVERT(CHAR(20),p.operate_time,23) like '" + operateTime + "%'";
        }
        if (department != "0")
        {
            sqlWhereCondition += " and u1.department_id=" + department;
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
            string dataSql = "";
            if (curpage == 1)
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereConditionWithHead + " order by p.no desc";
            else
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no where p.no not in(select top " + pageSize * (curpage - 1) + "  p.no from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereConditionWithHead + " order by p.no desc)" + sqlWhereCondition + " order by p.no desc";
            DataTable pageDataTable = db.reDt(dataSql);

            checkPageBtnStatus(curpage);

            this.points.DataSource = pageDataTable;
            this.points.DataKeyField = "no";
            this.points.DataBind();

            string exportSwitch = (string)Session["export_switch"];
            if (exportSwitch == "on")
            {
                initExportData(pageDataTable);
            }

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
            string dataSql = "";
            if (curpage == 1)
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + " order by p.no desc";
            else
                dataSql = "select top " + pageSize + " p.no as no,d.department_name as department,u1.real_name as username,CONVERT(CHAR(20),p.event_time,23) as event_time,p.point_value as point_value,c.category as event_category,p.fill_user as fill_user,u2.real_name as operate_user,CONVERT(CHAR(20),p.operate_time,23) as operate_time,isnull(cast(nullif('','') as datetime),p.update_time) as update_time,p.event as event from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no where p.no not in(select top " + pageSize * (curpage - 1) + "  p.no from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no order by p.no desc) order by p.no desc";
            DataTable pageDataTable = db.reDt(dataSql);

            checkPageBtnStatus(curpage);

            this.points.DataSource = pageDataTable;
            this.points.DataKeyField = "no";
            this.points.DataBind();

            string exportSwitch = (string)Session["export_switch"];
            if (exportSwitch == "on")
            {
                initExportData(pageDataTable);
            }
            
        }

    }

    protected void initExportData(DataTable dt)
    {
        Session["export_data"] = dt;
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        Dictionary<string, string> dit = prepareInformation();
        string defaultCategory = dit["category_id"];
        string operateUser = dit["operate_user"];
        string operateTime = dit["operate_time"];

        if (FileUpload.HasFile == false)
        {
            Response.Write("<script>alert('please choose excel file');location='Main.aspx'</script> ");
            return;
        }
        string IsXls = System.IO.Path.GetExtension(FileUpload.FileName).ToString().ToLower();
        if (IsXls != ".xls")
        {
            Response.Write("<script>alert('please choose excel file');location='Main.aspx'</script>");
            return;
        }
        string filename = FileUpload.FileName;
        string savePath = Server.MapPath(("upfiles\\") + filename);
        FileUpload.SaveAs(savePath);
        DataSet ds = ExcelSqlConnection(savePath, filename);
        DataRow[] dr = ds.Tables[0].Select();
        int rowsnum = ds.Tables[0].Rows.Count;
        if (rowsnum == 0)
        {
            Response.Write("<script>alert('null table');location='Main.aspx'</script>");
        }
        else
        {
            for (int i = 0; i < dr.Length; i++)
            {
                string name = dr[i]["name"].ToString();
                string getUserIdSql = "select no from points_management_DB_user where real_name='" + name + "'";
                int user_id = db.getFristValue(getUserIdSql);
                string event_time = dr[i]["time"].ToString();
                string value = dr[i]["value"].ToString();
                string fill_user = dr[i]["fill user"].ToString();
                string evt = dr[i]["event"].ToString();

                if (user_id == 0)
                {
                    Response.Write("<script>alert('already import " + i + " rows,user name not exist,at row " + (i + 1) + "');location='Main.aspx'</script>");
                    return;
                }
                DateTime date = DateTime.MinValue;
                bool flag = DateTime.TryParse(event_time, out date);
                if (!flag)
                {
                    Response.Write("<script>alert('already import " + i + " rows,date format error,at row " + (i + 1) + "');location='Main.aspx'</script>");
                    return;
                }
                if (!Regex.IsMatch(value, @"^[+-]?\d*$"))
                {
                    Response.Write("<script>alert('already import " + i + " rows,please input integer,at row " + (i + 1) + "');location='Main.aspx'</script>");
                    return;
                }
                if (fill_user == "")
                {
                    Response.Write("<script>alert('already import " + i + " rows,please input fill user,at row " + (i + 1) + "');location='Main.aspx'</script>");
                    return;
                }
                if (evt == "")
                {
                    Response.Write("<script>alert('already import " + i + " rows,please input event,at row " + (i + 1) + "');location='Main.aspx'</script>");
                    return;
                }

                string sql = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + user_id + ",'" + event_time + "'," + value + "," + defaultCategory + ",'" + fill_user + "'," + operateUser + ",'" + operateTime + "',null,'" + evt + "');";
                db.ExSql(sql);
                //Response.Write("<script>alert('" + name + event_time + value + fill_user + evt + "')</script>");
                //insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event)
            }
            //Response.Write("<script>alert('import success');location='Main.aspx'</script>");
            initDataWithSearchConditionOff();
        }

    }


    protected Dictionary<string, string> prepareInformation()
    {
        Dictionary<string, string> dit = new Dictionary<string, string>();
        DB db = new DB();
        string categorySql = "select top 1 no from points_management_DB_event_category";
        int category_no = db.getFristValue(categorySql);
        string operate_user = Convert.ToString(Session["no"]);
        string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
        dit.Add("category_id", Convert.ToString(category_no));
        dit.Add("operate_user", operate_user);
        dit.Add("operate_time", nowDate);
        return dit;
    }

    public System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
    {
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        try
        {
            string strCom = string.Format("SELECT * FROM [Sheet1$]");
            ExcelConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "[" + tableName + "$]");
            ExcelConn.Close();
            return ds;
        }
        catch
        {
            ExcelConn.Close();
            return null;
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
        string sqlWhereCondition = constructionOfWhereCondition(true);
        string sumSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and p.event_category!=" + full_year_event_category;
        string addSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and p.point_value>0" + " and p.event_category!=" + full_year_event_category;
        string minusSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and p.point_value<0" + " and p.event_category!=" + full_year_event_category;
        string fullYearSql = "select sum(point_value) from points_management_DB_points as p inner join points_management_DB_user as u1 on p.user_id=u1.no inner join points_management_DB_user as u2 on p.operate_user=u2.no inner join points_management_DB_event_category as c on p.event_category=c.no inner join points_management_DB_department as d on u1.department_id=d.no" + sqlWhereCondition + " and p.event_category=" + full_year_event_category;
        int sum = db.getFristValue(sumSql);
        int add = db.getFristValue(addSql);
        int minus = db.getFristValue(minusSql);
        int fullYear = db.getFristValue(fullYearSql);
        this.labSum.Text = "sum:" + sum;
        this.labAdd.Text = "add:" + add;
        this.labMinus.Text = "minus:" + minus;
        this.labFullYear.Text = "full year:" + fullYear;
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
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        //string department = this.insert_lstDepartment.SelectedValue.ToString();
        string eventTime = this.insert_calDate.SelectedDate.ToString("yyyy-MM-dd");
        string value = this.insert_txtValue.Text.Trim();
        string eventCategory = this.insert_lstEventCategory.SelectedValue.ToString();
        string fillUser = this.insert_txtFillUser.Text.Trim();
        string operateUserId = Convert.ToString(Session["no"]);
        string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
        string evt = this.insert_txtEvent.Text.Trim();
        if (!(insert_chkNames.SelectedIndex >= 0))
        {
            Response.Write("<script>alert('please select staff');location='Main.aspx'</script>");
            return;
        }
        if (eventTime == "0001-01-01")
        {
            Response.Write("<script>alert('please select date');location='Main.aspx'</script>");
            return;
        }
        if (!Regex.IsMatch(value, @"^[+-]?\d*$"))
        {
            Response.Write("<script>alert('please input integer');location='Main.aspx'</script>");
            return;
        }
        if (fillUser == "")
        {
            Response.Write("<script>alert('please input fill user');location='Main.aspx'</script>");
            return;
        }
        if (evt == "")
        {
            Response.Write("<script>alert('please input event');location='Main.aspx'</script>");
            return;
        }

        foreach (ListItem li in insert_chkNames.Items)
        {
            if (li.Selected)
            {
                string sql = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + li.Value.ToString() + ",'" + eventTime + "'," + value + "," + eventCategory + ",'" + fillUser + "'," + operateUserId + ",'" + nowDate + "',null,'" + evt + "');";
                db.ExSql(sql);
            }
        }
        initDataWithSearchConditionOff();
        returnInputForm();
    }
    protected void SelectedDepartmentChanged(object sender, EventArgs e)
    {
        string department = this.insert_lstDepartment.SelectedValue.ToString();
        if (department != "0")
        {
            insert_chkNames.Items.Clear();
            DB db = new DB();
            string userIndeptSql = "select * from points_management_DB_user where department_id=" + department;
            SqlDataReader dr = db.reDr(userIndeptSql);
            int nameIndex = 0;
            while (dr.Read())
            {
                ListItem initItem = new ListItem();
                initItem.Value = Convert.ToString(dr.GetValue(0));
                initItem.Text = Convert.ToString(dr.GetValue(5));
                insert_chkNames.Items.Add(initItem);
                nameIndex++;
            }
        }
    }

    protected void btnDelete_Load(object sender, EventArgs e)
    {
        ((Button)sender).Attributes["onclick"] = "javascript:return confirm('are you sure to delete this record?')";
    }

    protected void points_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        DB db = new DB();
        string userID = e.CommandArgument.ToString();
        string sqlStr = "delete from points_management_DB_points where no=" + userID;
        int reValue = db.sqlEx(sqlStr);
        if (reValue == 0)
            Response.Write("<script>alert('delete failure!');</script>");

        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            bindWithSearchConditionOn();
        }
    }

    protected void points_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        DB db = new DB();
        string no = e.CommandArgument.ToString();
        string name = ((TextBox)e.Item.FindControl("update_txtName")).Text;
        string getUserIdSql = "select no from points_management_DB_user where real_name='" + name + "'";
        int user_id = db.getFristValue(getUserIdSql);
        string event_time = ((TextBox)e.Item.FindControl("update_txtEventTime")).Text.Trim();
        string point_value = ((TextBox)e.Item.FindControl("update_txtPointValue")).Text.Trim();
        string event_category = ((DropDownList)e.Item.FindControl("update_lstEventCategory")).SelectedValue.ToString();
        string fill_user = ((TextBox)e.Item.FindControl("update_txtFillUser")).Text.Trim();
        string evt = ((TextBox)e.Item.FindControl("update_txtEvent")).Text.Trim();
        string nowDate = DateTime.Now.ToString("yyyy-MM-dd");

        if (user_id == 0)
        {
            Response.Write("<script>alert('user not exist');</script>");
            return;
        }
        DateTime date = DateTime.MinValue;
        bool flag = DateTime.TryParse(event_time, out date);
        if (!flag)
        {
            Response.Write("<script>alert('date format error');</script>");
            return;
        }
        if (!Regex.IsMatch(point_value, @"^[+-]?\d*$"))
        {
            Response.Write("<script>alert('please input integer');</script>");
            return;
        }
        if (fill_user == "")
        {
            Response.Write("<script>alert('please input fill user');</script>");
            return;
        }
        if (evt == "")
        {
            Response.Write("<script>alert('please input event');</script>");
            return;
        }

        string sqlStr = "update points_management_DB_points set user_id=" + user_id + ",event_time='" + event_time + "',point_value=" + point_value + ",event_category=" + event_category + ",fill_user='" + fill_user + "',event='" + evt + "',update_time='" + nowDate + "' where no=" + no;
        int reValue = db.sqlEx(sqlStr);

        if (reValue == 0)
            Response.Write("<script>alert('update failure!');</script>");
        else
            Response.Write("<script>alert('update success!');</script>");

        points.EditItemIndex = -1;

        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            bindWithSearchConditionOn();
        }

    }

    protected void points_CancelCommand(object source, DataListCommandEventArgs e)
    {
        points.EditItemIndex = -1;

        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            bindWithSearchConditionOn();
        }
    }

    protected void points_EditCommand(object source, DataListCommandEventArgs e)
    {
        points.EditItemIndex = e.Item.ItemIndex;

        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            bindWithSearchConditionOn();
        }

        DB db = new DB();
        DropDownList update_lstEventCategory = (DropDownList)points.Items[e.Item.ItemIndex].FindControl("update_lstEventCategory");
        string eventCategorySql = "select * from points_management_DB_event_category";
        DataTable dt_2 = db.reDt(eventCategorySql);
        update_lstEventCategory.DataSource = dt_2;
        update_lstEventCategory.DataValueField = "no";
        update_lstEventCategory.DataTextField = "category";
        update_lstEventCategory.DataBind();

        string oldEventCategory = ((TextBox)points.Items[e.Item.ItemIndex].FindControl("update_hiddenEventCategory")).Text;
        int i = 0;
        foreach (ListItem item in update_lstEventCategory.Items)
        {
            if (item.Text == oldEventCategory)
            {
                update_lstEventCategory.SelectedIndex = i;
            }
            i++;
        } 
        
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string exportSwitch = (string)Session["export_switch"];
        if (exportSwitch == "on")
        {
            DataTable dt = (DataTable)Session["export_data"];
            if (dt == null)
            {
                Response.Write("<script>alert('please init export data');</script>");
            }
            else 
            {
                CreateExcel(dt, "exoprt.xls");
            }
            
        }
    }
    public void CreateExcel(DataTable dt, string FileName)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
        string colHeaders = "", ls_item = "";

        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;

        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            }

        }
        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))
                {
                    ls_item += row[i].ToString() + "\n";
                }
                else
                {
                    ls_item += row[i].ToString() + "\t";
                }

            }
            resp.Write(ls_item);
            ls_item = "";

        }
        resp.End();
    }
    protected void btnExportSwitch_Click(object sender, EventArgs e)
    {
        string exportSwitch = (string)Session["export_switch"];
        if (exportSwitch == "on")
        {
            Session["export_switch"] = "off";
        }
        if (exportSwitch == "off")
        {
            Session["export_switch"] = "on";
        }

        string searchCondition = (string)Session["search_condition"];
        if (searchCondition == "off")
        {
            bindWithSearchConditionOff();
        }
        else if (searchCondition == "on")
        {
            bindWithSearchConditionOn();
        }
    }

    public void returnInputForm()
    {
        this.insert_chkNames.ClearSelection();
        this.insert_txtEvent.Text = "";
    }
}