using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace FoodFest
{
    public partial class Review : System.Web.UI.Page
    {

        DBConnect resConn = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["LoggedInUserName"] != null))
                {
                    LinkButton LinkButton1 = (LinkButton)Master.FindControl("LinkButton1");
                    LinkButton1.Visible = false;
                    LinkButton LinkButton2 = (LinkButton)Master.FindControl("LinkButton2");
                    LinkButton2.Visible = true;


                    Label Welcome_label = (Label)Master.FindControl("Welcome_label");
                    Welcome_label.Text = "Welcome " + Session["LoggedInUserName"];
                    Welcome_label.Visible = true;

                    LinkButton LinkButton3 = (LinkButton)Master.FindControl("LinkButton3");
                    LinkButton3.Visible = true;

                }
                else
                {

                    LinkButton LinkButton1 = (LinkButton)Master.FindControl("LinkButton1");
                    LinkButton1.Visible = true;
                    LinkButton LinkButton2 = (LinkButton)Master.FindControl("LinkButton2");
                    LinkButton2.Visible = false;
                    LinkButton LinkButton3 = (LinkButton)Master.FindControl("LinkButton3");
                    LinkButton3.Visible = false;

                }
                string RestaurantId = Request.QueryString["RestaurantId"];

               // string RestaurantId = "2";

                try
                {

                    resConn.SqlConnectionObj.Open();
                    string query2 = String.Format("Select RestaurantId, RestaurantName, RestaurantAddress, RestaurantPhone, RestaurantMinOrder, CONVERT (VARCHAR(8), OpeningHour, 108) AS OpeningHour, CONVERT (VARCHAR(8), ClosingHour, 108) AS ClosingHour,DeliveryFee from Restaurant_tbl where RestaurantId={0}", RestaurantId);
                    resConn.SqlCommandObj.CommandText = query2;
                    SqlDataReader reader = resConn.SqlCommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        Label5.Text = reader.GetString(1).ToString();
                        Label7.Text = reader.GetString(2).ToString();
                        Label10.Text = reader.GetString(3).ToString();

                        Label12.Text = reader.GetString(4).ToString() + "TK.";
                        Label14.Text = reader.GetInt32(7).ToString();
                        Label23.Text = " TK.";

                        Label18.Text = reader.GetString(5).ToString();
                        Label19.Text = reader.GetString(6).ToString();
                    }

                    resConn.SqlConnectionObj.Close();

                    Image2.ImageUrl = "~/HandlerImg.ashx?restId=" + RestaurantId;


                    //cheking if rest is closed
                    CheckStatus aCheck = new CheckStatus();

                    bool UserValid = aCheck.CheckUser(Request.QueryString["RestaurantId"]);

                    if (UserValid)
                    {
                        Label20.Text = "Restaurant is Open";
                    }

                    else
                    {
                        /*ei khane ei label ta thakbe plus restaurant j close tao ekta alert dia dekhabe, page e ashar shathe shathe*/
                        Label20.Text = "Restaurant Already Closed!!!";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred during user save operation. Try again", ex);
                }
                finally
                {
                    if (resConn.SqlConnectionObj != null && resConn.SqlConnectionObj.State == ConnectionState.Open)
                    {
                        resConn.SqlConnectionObj.Close();
                    }
                }



                GetReviews();

             
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int UserId;

            if ((Session["LoggedInUserName"] != null))
            {
                Food aFood = new Food();
                UserId = aFood.getUserId(Session["loggedInUserName"].ToString());
                string RestId = Request.QueryString["RestaurantId"];

                try
                {
                    resConn.SqlConnectionObj.Open();
                    string query = String.Format("INSERT INTO Review_tbl(Review,ReviewDate,UserId,RestaurantId) VALUES('{0}',GetDate(),'{1}',{2})",TextBox1.Text ,UserId,RestId);
                    resConn.SqlCommandObj.CommandText = query;
                    resConn.SqlCommandObj.ExecuteNonQuery();
                    resConn.SqlConnectionObj.Close();

                    msgLabel.Text = "Your post was successfully added";
                    TextBox1.Text = "";
                }

                catch (Exception ex)
                {
                    throw new Exception("Error occurred during user save operation. Try again", ex);
                }


                GetReviews();

               
            }
            else
            {
                string RestId = Request.QueryString["RestaurantId"];
                var url = String.Format("~/Login_Page.aspx?RestaurantId={0}&from=review", RestId);
                Response.Redirect(url);
            }
        }


        public void GetReviews()
        {
            string RestaurantId = Request.QueryString["RestaurantId"];


            try
            {

                resConn.SqlConnectionObj.Open();
                string query2 = String.Format("SELECT Review_tbl.Review, Review_tbl.ReviewDate, User_tbl.UserName FROM Review_tbl INNER JOIN User_tbl ON Review_tbl.UserId = User_tbl.UserId WHERE (Review_tbl.RestaurantId = {0})", RestaurantId);
                resConn.SqlCommandObj.CommandText = query2;
                //SqlDataReader reader = resConn.SqlCommandObj.ExecuteReader();
                Repeater1.DataSource = resConn.SqlCommandObj.ExecuteReader();
                Repeater1.DataBind();

                resConn.SqlConnectionObj.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during user save operation. Try again", ex);
            }
            finally
            {
                if (resConn.SqlConnectionObj != null && resConn.SqlConnectionObj.State == ConnectionState.Open)
                {
                    resConn.SqlConnectionObj.Close();
                }
            }
 
        }




    }
}