using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FixedPointsSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindData();
        }
    }

    protected void bindData()
    {
        DB db = new DB();
        string sql = "select f.no as no,u.real_name as real_name,f.education as education,f.specialty as specialty,f.office as office,f.level as level,f.certificate as certificate from points_management_DB_fixed_points as f inner join points_management_DB_user as u on f.user_id = u.no";
        DataTable dt = db.reDt(sql);
        this.fixedPoints.DataSource = dt;
        //this.fixedPoints.DataKeyField = "no";
        this.fixedPoints.DataBind();
    }

    protected void fixedPoints_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        DB db = new DB();
        string no = e.CommandArgument.ToString();
        string education = ((TextBox)e.Item.FindControl("update_txtEducation")).Text.Trim();
        string specialty = ((TextBox)e.Item.FindControl("update_txtSpecialty")).Text.Trim();
        string office = ((TextBox)e.Item.FindControl("update_txtOffice")).Text.Trim();
        string level = ((TextBox)e.Item.FindControl("update_txtLevel")).Text.Trim();
        string certificate = ((TextBox)e.Item.FindControl("update_txtCertificate")).Text.Trim();


        if (!Regex.IsMatch(education, @"^[+-]?\d*$") || !Regex.IsMatch(specialty, @"^[+-]?\d*$") || !Regex.IsMatch(office, @"^[+-]?\d*$") || !Regex.IsMatch(level, @"^[+-]?\d*$") || !Regex.IsMatch(certificate, @"^[+-]?\d*$"))
        {
            Response.Write("<script>alert('please input integer');location='FixedPointsSetting.aspx'</script>");
            return;
        }

        string sqlStr = "update points_management_DB_fixed_points set education=" + education + ",specialty=" + specialty + ",office=" + office + ",level=" + level + ",certificate=" + certificate + " where no=" + no;
        int reValue = db.sqlEx(sqlStr);

        if (reValue == 0)
            Response.Write("<script>alert('update failure!');location='FixedPointsSetting.aspx'</script>");
        else
            Response.Write("<script>alert('update success!');location='FixedPointsSetting.aspx'</script>");

        fixedPoints.EditItemIndex = -1;

        bindData();
    }

    protected void fixedPoints_CancelCommand(object source, DataListCommandEventArgs e)
    {
        fixedPoints.EditItemIndex = -1;
        bindData();

    }

    protected void fixedPoints_EditCommand(object source, DataListCommandEventArgs e)
    {
        fixedPoints.EditItemIndex = e.Item.ItemIndex;
        bindData();
    }

    protected void btnAdd_Click(object sender, CommandEventArgs e)
    {
        string no = e.CommandArgument.ToString();
        string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
        string operate_user = Convert.ToString(Session["no"]);

        DB db = new DB();
        string sql = "select f.no as no,u.no as uno,f.education as education,f.specialty as specialty,f.office as office,f.level as level,f.certificate as certificate from points_management_DB_fixed_points as f inner join points_management_DB_user as u on f.user_id = u.no where f.no="+no;
        SqlDataReader dr = db.reDr(sql);
        dr.Read();
        if (dr.HasRows)
        {
            string fno = Convert.ToString(dr.GetValue(0));
            string uno = Convert.ToString(dr.GetValue(1));
            string education = Convert.ToString(dr.GetValue(2));
            string specialty = Convert.ToString(dr.GetValue(3));
            string office = Convert.ToString(dr.GetValue(4));
            string level = Convert.ToString(dr.GetValue(5));
            string certificate = Convert.ToString(dr.GetValue(6));

            string insertSql_1 = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + uno + ",'" + nowDate + "'," + education + ",9,''," + operate_user + ",'" + nowDate + "',null,'学历');";
            string insertSql_2 = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + uno + ",'" + nowDate + "'," + specialty + ",9,''," + operate_user + ",'" + nowDate + "',null,'特长');";
            string insertSql_3 = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + uno + ",'" + nowDate + "'," + office + ",9,''," + operate_user + ",'" + nowDate + "',null,'职务');";
            string insertSql_4 = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + uno + ",'" + nowDate + "'," + level + ",9,''," + operate_user + ",'" + nowDate + "',null,'岗位级别');";
            string insertSql_5 = "insert into points_management_DB_points(user_id,event_time,point_value,event_category,fill_user,operate_user,operate_time,update_time,event) values(" + uno + ",'" + nowDate + "'," + certificate + ",9,''," + operate_user + ",'" + nowDate + "',null,'荣誉证书');";
            db.ExSql(insertSql_1);
            db.ExSql(insertSql_2);
            db.ExSql(insertSql_3);
            db.ExSql(insertSql_4);
            db.ExSql(insertSql_5);

            Response.Write("<script>alert('insert success');location='FixedPointsSetting.aspx'</script>");
        }
        
        
        //Response.Write("<script>alert('"+temp+"');location='FixedPointsSetting.aspx'</script>");
    }
}