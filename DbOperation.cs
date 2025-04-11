using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;


namespace HouseRental
{
    
    public class DbOperation
    {
      public  SqlConnection con=new SqlConnection();
      public SqlCommand cmd = new SqlCommand();


        public  DbOperation()
        {
            string conn = ConfigurationManager.ConnectionStrings["rose"].ConnectionString;
            con=new SqlConnection(conn);
            cmd.Connection = con;
        }

        //sql connection open function
        public SqlConnection Getcon()
        {
            
            if (con.State == ConnectionState.Closed)

            {
                con.Open();
            }

            return con;
        }

        //connection closed function.
        public void dbclose()
        {
            con.Close();
        }

        //Execute Non Query Common function

        public int exenonquery(String Sql)
        {
            Getcon();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Sql;//query is sql;
            int i = cmd.ExecuteNonQuery();
            return i;
        }

        //Execute Non Scalar Common function
        public object exescalar(String sql)
        {
            Getcon();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            object ob = cmd.ExecuteScalar();
            return ob;

        }
        //Execute Readerr Common function
          public SqlDataReader exereader(String sql)
          {
              Getcon();
              cmd.CommandType = CommandType.Text;
              cmd.CommandText = sql;
              SqlDataReader dr = cmd.ExecuteReader();
              return dr;
          }

        /*Disconnected Architecture*/
        public DataSet exedataset(String sql)
        {

            cmd.CommandType = CommandType.Text;  //SET COMMAND TYPE TEXT OR STOREPROCEDURE OR XML LIKE WISE
            cmd.CommandText = sql; //PASS SQL COMMAND TO COMMAND TEXT
            SqlDataAdapter da = new SqlDataAdapter(cmd); //CREATE ADAPTER CLASS BECAUSE DISCONNECTED ARCHITECTURE
            DataSet ds = new DataSet(); //CREATE DATASET
            da.Fill(ds); //FILL DATA TO DATASET USING ADAPTER OBJECT
            return ds; //RETURN DATASET
        }


        public DataTable exedatatable(String sql)
        {

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //fill Gridview Control
        public void fillgrid(String sql, GridView dv)
        {
            dv.DataSource = exedataset(sql);
            dv.DataBind();
        }

        //fill datalist

        public void filldatalist(String sql, DataList  dl)
        {
            dl.DataSource = exedataset(sql);
            dl.DataBind();
        }

        public void fillddl(String sql, DropDownList ddl, String txt, String val)
        {
            ddl.DataTextField = txt;
            ddl.DataValueField = val;
            ddl.DataSource = exedataset(sql);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---select---", "0"));
        }

        public void fillcheck(String sql, DropDownList ddl, String txt, String val)
        {
            ddl.DataTextField = txt;
            ddl.DataValueField = val;
            ddl.DataSource = exedataset(sql);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---select---", "0"));
        }

        /*End of Disconnected Architecture*/
        public void filllistview(String sql, ListView lv)
        {
            lv.DataSource = exedataset(sql);
            lv.DataBind();

        }
    }
}