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
    public partial class Login : System.Web.UI.Page
    {
        DBConnect resConn = new DBConnect();
        
        public List<Order> Orders = new List<Order>();

       //public int total = 0;

        protected void Page_Load(object sender, EventArgs e)
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
            string message = "";



            //Label6.Text = " Helo " + Session["LoggedInUserName"].ToString();

            if (!IsPostBack)
            {
                string RestaurantId = Request.QueryString["RestaurantId"];

                try
                {
                    resConn.SqlConnectionObj.Open();
                    string query = String.Format("Select * from food_tbl where RestaurantId={0}", RestaurantId);
                    resConn.SqlCommandObj.CommandText = query;
                    //SqlDataReader reader = resConn.SqlCommandObj.ExecuteReader();
                    Repeater1.DataSource = resConn.SqlCommandObj.ExecuteReader();
                    Repeater1.DataBind();

                    resConn.SqlConnectionObj.Close();

                    resConn.SqlConnectionObj.Open();
                    string query2 = String.Format("Select RestaurantId, RestaurantName, RestaurantAddress, RestaurantPhone, RestaurantMinOrder, CONVERT (VARCHAR(8), OpeningHour, 108) AS OpeningHour, CONVERT (VARCHAR(8), ClosingHour, 108) AS ClosingHour,DeliveryFee from Restaurant_tbl where RestaurantId={0}", RestaurantId);
                    resConn.SqlCommandObj.CommandText = query2;
                    SqlDataReader reader = resConn.SqlCommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        Label5.Text = reader.GetString(1).ToString();
                        Label7.Text = reader.GetString(2).ToString();
                        Label10.Text = reader.GetString(3).ToString();

                        Label12.Text = reader.GetString(4).ToString();
                        Label25.Text="TK.";
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
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Restaurant Is Open')", true);
                    }

                    else
                    {
                        /*ei khane ei label ta thakbe plus restaurant j close tao ekta alert dia dekhabe, page e ashar shathe shathe*/
                        Label20.Text = "Restaurant Already Closed!!!";
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Restaurant is Closed')", true);
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


                string restId= Request.QueryString["RestaurantId"];
                updateTopItems(restId);
            }
            

        }


        public void updateTopItems(string id)
        {


            try
            {
                resConn.SqlConnectionObj.Open();
                string query = "";
                query = String.Format("Select top (5) FoodName from Food_tbl where RestaurantId='{0}' order by FoodSaleTotal desc;", id);
                resConn.SqlCommandObj.CommandText = query;
                //SqlDataReader reader = resConn.SqlCommandObj.ExecuteReader();
                Repeater2.DataSource = resConn.SqlCommandObj.ExecuteReader();
                Repeater2.DataBind();
                resConn.SqlConnectionObj.Close();
             



            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during DB operation. Try again", ex);
            }
            finally
            {
                if (resConn.SqlConnectionObj != null && resConn.SqlConnectionObj.State == ConnectionState.Open)
                {
                    resConn.SqlConnectionObj.Close();
                }
            }

        }



        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {

            CheckStatus aCheck = new CheckStatus();

            bool restOpen = aCheck.CheckUser(Request.QueryString["RestaurantId"]);

            string rId = Request.QueryString["RestaurantId"];

            if (restOpen)
            {
                if ((Session["LoggedInUserName"] != null))
                {
                    String FoodName = e.CommandArgument.ToString();
                    Food item = new Food();
                    string Cost = item.getPrice(FoodName,rId);
                    double cost = Convert.ToDouble(Cost);
                    double total = 0;
                    if (Convert.ToInt32(NoOfItem.Text) == 0)
                    {
                        I1.Text = FoodName;
                        P1.Text = cost.ToString();
                        tk1.Text = "tk";
                        NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) + 1);
                        //n1.Text = "1";
                        //s1.Text = "X";
                        //total = cost;
                        //Total.Text = total+"";
                        item1DropdownList.Visible = true;
                        DeletItem1.Visible = true;
                    }

                    else if (Convert.ToInt32(NoOfItem.Text) == 1)
                    {
                        if (I1.Text == FoodName)
                        {
                            //n1.Text = Convert.ToString(Convert.ToInt32(n1.Text) + 1);
                            Label3.Text = "Food Item alresdy Exists";

                        }
                        else
                        {
                            I2.Text = FoodName;
                            P2.Text = cost.ToString();
                            tk2.Text = "tk";
                            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) + 1);
                            //n2.Text = "1";
                            //s2.Text = "X";
                            DeletItem2.Visible = true;
                            item2DropdownList.Visible = true;
                        }
                        //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text)+cost);
                        //total = total + cost;
                        //Total.Text = total + " tk";


                    }
                    else if (Convert.ToInt32(NoOfItem.Text) == 2)
                    {
                        if (I1.Text == FoodName)
                        {
                            Label3.Text = "Food Item alresdy Exists";
                            //n1.Text = Convert.ToString(Convert.ToInt32(n1.Text) + 1);
                        }
                        else if (I2.Text == FoodName)
                        {
                            Label3.Text = "Food Item alresdy Exists";
                            //n2.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                        }
                        else
                        {
                            I3.Text = FoodName;
                            P3.Text = Cost.ToString();
                            tk3.Text = "tk";
                            //n3.Text = "1";
                            //s3.Text = "X";
                            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) + 1);
                            DeletItem3.Visible = true;
                            item3DropdownList.Visible = true;
                        }

                        //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        //total = total + cost;
                        //Total.Text = total + " tk";

                    }
                    else if (Convert.ToInt32(NoOfItem.Text) == 3)
                    {
                        if (I1.Text == FoodName)
                        {
                            Label3.Text = "Food Item alresdy Exists";
                            //n1.Text = Convert.ToString(Convert.ToInt32(n1.Text) + 1);
                        }
                        else if (I2.Text == FoodName)
                        {
                            Label3.Text = "Food Item alresdy Exists";
                            //n2.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                        }
                        else if (I3.Text == FoodName)
                        {
                            Label3.Text = "Food Item alresdy Exists";
                            //n3.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                        }
                        else
                        {
                            I4.Text = FoodName;
                            P4.Text = Cost.ToString();
                            tk4.Text = "tk";
                            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) + 1);
                            //n4.Text = "1";
                            //s4.Text = "X";
                            DeletItem4.Visible = true;
                            item4DropdownList.Visible = true;
                        }

                        //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        //total = total + cost;
                        //Total.Text = total + " tk";

                    }
                    else if (Convert.ToInt32(NoOfItem.Text) == 4)
                    {
                        if (I1.Text == FoodName)
                        {
                            //n1.Text = Convert.ToString(Convert.ToInt32(n1.Text) + 1);
                        }
                        else if (I2.Text == FoodName)
                        {
                            //n2.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                        }
                        else if (I3.Text == FoodName)
                        {
                            //n3.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                        }
                        else if (I4.Text == FoodName)
                        {
                            //n4.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                        }
                        else
                        {
                            I5.Text = FoodName;
                            P5.Text = cost.ToString();
                            tk5.Text = "tk";
                            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) + 1);
                            //n5.Text = "1";
                            //s5.Text = "X";
                            DeletItem5.Visible = true;
                            item5DropdownList.Visible = true;

                        }
                        //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        //total = total + cost;
                        //Total.Text = total + " tk";

                    }
                    else if (Convert.ToInt32(NoOfItem.Text) == 5)
                    {
                        if (I1.Text == FoodName)
                        {
                            //n1.Text = Convert.ToString(Convert.ToInt32(n1.Text) + 1);
                            //Label3.Text = "Food Item alresdy Exists";
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Food Item alresdy Exists')", true);
                            Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        }
                        else if (I2.Text == FoodName)
                        {
                            //n2.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                            //Label3.Text = "Food Item alresdy Exists";
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Food Item alresdy Exists')", true);
                            Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        }
                        else if (I3.Text == FoodName)
                        {
                            //n3.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                            //Label3.Text = "Food Item alresdy Exists";
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Food Item alresdy Exists')", true);
                            Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        }
                        else if (I4.Text == FoodName)
                        {
                            //n4.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Food Item alresdy Exists')", true);
                            //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        }
                        else if (I5.Text == FoodName)
                        {
                            //n5.Text = Convert.ToString(Convert.ToInt32(n2.Text) + 1);
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Food Item alresdy Exists')", true);
                            //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) + cost);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Too many items selected')", true);

                        }
                    }

                    CalculateTotal();
                }

                else
                {

                    /*ei khane ekta alert dia dekhabe j user logged in na and alert dekhanor por login page e pathai dibe(pathanor ta niche kora ase)*/

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have to Login to Order')", true);

                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

                    //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

                   // ShowMsg();

                   // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('You have to login to order')", true);

                    string RestId = Request.QueryString["RestaurantId"];
                    var url = String.Format("~/Login_Page.aspx?RestaurantId={0}", RestId);
                    Response.Redirect(url);
                }
            }

            else
            {
                /*ei khane restaurant j close, linkbutton e press korai alert dibe ekta alert dia dekhabe, page e ashar shathe shathe*/
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), "alert('Restaurant already closed!!!')", true);
            }

           



            

            
        }



        public void ShowMsg()
        {
            string message = "Do you want to Submit?";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("return confirm('");
            sb.Append(message);
            sb.Append("');");
            ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
        }

        /**************************DELETE ITEM BUTTONS***********************************/



        protected void DeletItem1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(NoOfItem.Text) == 1)
            {
                I1.Text = "";
                P1.Text = "";
                //s1.Text = "";
                //n1.Text = "";
                tk1.Text = "";
                item1DropdownList.Visible = false;
                DeletItem1.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 2)
            {
                I1.Text = I2.Text;
                P1.Text = P2.Text;
                //s1.Text = s2.Text;
                //n1.Text = n2.Text;
                I2.Text = "";
                P2.Text = "";
                //s2.Text = "";
                //n2.Text = "";
                tk2.Text = "";
                DeletItem2.Visible = false;
                item2DropdownList.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 3)
            {
                I1.Text = I2.Text;
                P1.Text = P2.Text;
                //s1.Text = s2.Text;
                //n1.Text = n2.Text;
                I2.Text = I3.Text;
                P2.Text = P3.Text;
                //s2.Text = s3.Text;
                //n2.Text = n3.Text;
                I3.Text = "";
                P3.Text = "";
                //s3.Text = "";
                //n3.Text = "";
                tk3.Text = "";
                DeletItem3.Visible = false;
                item3DropdownList.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 4)
            {
                I1.Text = I2.Text;
                P1.Text = P2.Text;
                //s1.Text = s2.Text;
                //n1.Text = n2.Text;
                I2.Text = I3.Text;
                P2.Text = P3.Text;
                //s2.Text = s3.Text;
                //n2.Text = n3.Text;
                I3.Text = I4.Text;
                P3.Text = P4.Text;
                //s3.Text = s4.Text;
                //n3.Text = n4.Text;
                I4.Text = "";
                P4.Text = "";
                //s4.Text = "";
                //n4.Text = "";
                tk4.Text = "";
                DeletItem4.Visible = false;
                item4DropdownList.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 5)
            {
                I1.Text = I2.Text;
                P1.Text = P2.Text;
                //s1.Text = s2.Text;
                //n1.Text = n2.Text;
                I2.Text = I3.Text;
                P2.Text = P3.Text;
                //s2.Text = s3.Text;
                //n2.Text = n3.Text;
                I3.Text = I4.Text;
                P3.Text = P3.Text;
                //s3.Text = s4.Text;
                //n3.Text = n4.Text;
                I4.Text = I5.Text;
                P4.Text = P5.Text;
                //s4.Text = s5.Text;
                //n4.Text = n5.Text;
                I5.Text = "";
                P5.Text = "";
                //s5.Text = "";
                //n5.Text = "";
                tk5.Text = "";
                DeletItem5.Visible = false;
                item5DropdownList.Visible = false;
            }
            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) - 1);
            //double amount = Convert.ToDouble(P1.Text) * Convert.ToDouble(n1.Text);
            //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) - amount);

            CalculateTotal();

        }

        protected void DeletItem2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(NoOfItem.Text) == 2)
            {
                I2.Text = "";
                P2.Text = "";
                ////s2.Text = "";
                ////n2.Text = "";
                tk2.Text = "";
                DeletItem2.Visible = false;
                item2DropdownList.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 3)
            {
                I2.Text = I3.Text;
                P2.Text = P3.Text;
                //s2.Text = s3.Text;
                //n2.Text = n3.Text;
                I3.Text = "";
                P3.Text = "";
                //s3.Text = "";
                //n3.Text = "";
                tk3.Text = "";

 //-----> erokom kore shob gula te add korte hobe
                item2DropdownList.SelectedValue = item3DropdownList.SelectedValue;
                item3DropdownList.Visible = false;
                DeletItem3.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 4)
            {
                P2.Text = P3.Text;
                //s2.Text = s3.Text;
                //n2.Text = n3.Text;
                I3.Text = I4.Text;
                P3.Text = P3.Text;
                //s3.Text = s4.Text;
                //n3.Text = n4.Text;
                I4.Text = "";
                P4.Text = "";
                //s4.Text = "";
                //n4.Text = "";
                tk4.Text = "";
                item4DropdownList.Visible = false;
                DeletItem4.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 5)
            {
                I2.Text = I3.Text;
                P2.Text = P3.Text;
                //s2.Text = s3.Text;
                //n2.Text = n3.Text;
                I3.Text = I4.Text;
                P3.Text = P3.Text;
                //s3.Text = s4.Text;
                //n3.Text = n4.Text;
                I4.Text = I5.Text;
                P4.Text = P5.Text;
                //s4.Text = s5.Text;
                //n4.Text = n5.Text;
                I5.Text = "";
                P5.Text = "";
                //s5.Text = "";
                //n5.Text = "";
                tk5.Text = "";
                item5DropdownList.Visible = false;
                DeletItem5.Visible = false;
            }

            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) - 1);

            //double amount = Convert.ToDouble(P2.Text) * Convert.ToDouble(n2.Text);

            // Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) - amount);

            CalculateTotal();
        }

        protected void DeletItem3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(NoOfItem.Text) == 3)
            {
                I3.Text = "";
                P3.Text = "";
                //s3.Text = "";
                //n3.Text = "";
                tk3.Text = "";
                item3DropdownList.Visible = false;
                DeletItem3.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 4)
            {
                I3.Text = I4.Text;
                P3.Text = P3.Text;
                ////s3.Text = s4.Text;
                ////n3.Text = n4.Text;
                I4.Text = "";
                P4.Text = "";
                ////s4.Text = "";
                ////n4.Text = "";
                tk4.Text = "";
                item4DropdownList.Visible = false;
                DeletItem4.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 5)
            {
                I3.Text = I4.Text;
                P3.Text = P3.Text;
                //s3.Text = s4.Text;
                //n3.Text = n4.Text;
                I4.Text = I5.Text;
                P4.Text = P5.Text;
                //s4.Text = s5.Text;
                //n4.Text = n5.Text;
                I5.Text = "";
                P5.Text = "";
                //s5.Text = "";
                //n5.Text = "";
                tk5.Text = "";
                item5DropdownList.Visible = false;
                DeletItem5.Visible = false;
            }
            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) - 1);
            //  double amount = Convert.ToDouble(P3.Text) * Convert.ToDouble(n3.Text);
            //   Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) - amount);
            CalculateTotal();
        }

        protected void DeletItem4_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(NoOfItem.Text) == 4)
            {
                I4.Text = "";
                P4.Text = "";
                //s4.Text = "";
                //n4.Text = "";
                tk4.Text = "";
                item4DropdownList.Visible = false;
                DeletItem4.Visible = false;
            }
            else if (Convert.ToInt32(NoOfItem.Text) == 5)
            {
                I4.Text = I5.Text;
                P4.Text = P5.Text;
                //s4.Text = s5.Text;
                //n4.Text = n5.Text;
                I5.Text = "";
                P5.Text = "";
                //s5.Text = "";
                //n5.Text = "";
                tk5.Text = "";
                item5DropdownList.Visible = false;
                DeletItem5.Visible = false;
            }

            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) - 1);
            //double amount = Convert.ToDouble(P4.Text) * Convert.ToDouble(n4.Text);
            //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) - amount);
        }

        protected void DeletItem5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(NoOfItem.Text) == 5)
            {
                I5.Text = "";
                P5.Text = "";
                //s5.Text = "";
                //n5.Text = "";
                tk5.Text = "";
                item5DropdownList.Visible = false;
                DeletItem5.Visible = false;
            }
            NoOfItem.Text = Convert.ToString(Convert.ToInt32(NoOfItem.Text) - 1);
            //double amount = Convert.ToDouble(P5.Text) * Convert.ToDouble(n5.Text);
            //Total.Text = Convert.ToString(Convert.ToDouble(Total.Text) - amount);
            CalculateTotal();

        }

        //int total = 0;


        //protected void Button1_Click(object sender, EventArgs e)
        //{

        //    int N = Convert.ToInt32(NoOfItem.Text);
        //    int total = 0;
        //    for (int i = 1; i <= N; i++)
        //    {
        //        if (i == 1)
        //        {
        //            total = (Convert.ToInt32(item1DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P1.Text);
        //        }
        //        else if (i == 2)
        //        {
        //            total = total + (Convert.ToInt32(item2DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P2.Text);
        //        }
        //        else if (i == 3)
        //        {
        //            total = total + (Convert.ToInt32(item3DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P3.Text);
        //        }
        //        else if (i == 4)
        //        {
        //            total = total + (Convert.ToInt32(item4DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P4.Text);
        //        }
        //        else if (i == 5)
        //        {
        //            total = total + (Convert.ToInt32(item5DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P5.Text);
        //        }
        //    }
        //    Total.Text = total + "";
        //    //Label3.Text = item1DropdownList.SelectedValue.ToString();
        //}






        protected void GetFoodButton_Click(object sender, EventArgs e)
        {
            string FoodName = "";
            int Id;
            int quantity;
            int UserId;
            string BkAccNo = "";
            string BkTrxId = "";



            if (RadioButtonList1.SelectedValue == "Cash on Delivery")
            {
                bkAccNoTextBox.Text = "";
                BkTrxTextBox.Text = "";
            }
            else if (RadioButtonList1.SelectedValue == "Pay by BKash")
            {
                BkAccNo = bkAccNoTextBox.Text;
                BkTrxId = BkTrxTextBox.Text;
            }




            if (Convert.ToInt32(Total.Text)==0)
            {
                //Response.Redirect("~/Login_page.aspx");
                Alert.Text = "No food added to cart!!!";
            }
            else if (Convert.ToInt32(Total.Text) < Convert.ToInt32(Label12.Text))
            {
                Alert.Text = "You have add more to cart!!!";
            }
            else
            {
                
                string DeliveryAddress;
                double TotalAmount = Convert.ToDouble(Total.Text);
                Order anOrder = new Order();
                Food aFood = new Food();
                UserId = aFood.getUserId(Session["loggedInUserName"].ToString());
                DeliveryAddress = aFood.getDeliveryAddress(Session["loggedInUserName"].ToString());

                try
                {
                    resConn.SqlConnectionObj.Open();
                    string query = String.Format("INSERT INTO Order_tbl(UserId,TotalAmount,DeliveryAddress,OrderDate,BKashNum,BKashTrxId) VALUES('{0}','{1}','{2}',GetDate(),'{3}','{4}')", UserId, TotalAmount, DeliveryAddress,BkAccNo,BkTrxId);
                    resConn.SqlCommandObj.CommandText = query;
                    resConn.SqlCommandObj.ExecuteNonQuery();

                    Label2.Text = "Successfully Done!!!!";



                    resConn.SqlConnectionObj.Close();
                }

                catch (Exception Ex)
                {

                }

                int OrderId = 0;
                try
                {
                    resConn.SqlConnectionObj.Open();
                    string query2 = String.Format("Select Max(OrderId) from Order_tbl");
                    resConn.SqlCommandObj.CommandText = query2;
                    SqlDataReader reader = resConn.SqlCommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderId = reader.GetInt32(0);
                    }
                    resConn.SqlConnectionObj.Close();
                }
                catch (Exception Ex)
                {

                }


                //resConn.SqlConnectionObj.Open();
                for (int i = 0; i < Convert.ToInt32(NoOfItem.Text); i++)
                {
                    if (i == 0)
                    {

                        resConn.SqlConnectionObj.Open();
                        FoodName = I1.Text;
                        Id = aFood.getId(FoodName);
                        quantity = Convert.ToInt32(item1DropdownList.SelectedIndex)+1;
                        //quantity = Convert.ToInt32(n1.Text);
                        string query = String.Format("INSERT INTO Sold_tbl(OrderId,FoodId,UserId,Quantity) VALUES('{0}','{1}','{2}','{3}')", OrderId, Id, UserId, quantity);
                        resConn.SqlCommandObj.CommandText = query;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();

                        /* to update food table for top items*/
                        resConn.SqlConnectionObj.Open();

                        int a = 0;
                        string query2 = String.Format("Select FoodSaleTotal from Food_tbl where FoodId='{0}'", Id);
                        resConn.SqlCommandObj.CommandText = query2;
                        SqlDataReader reader1 = resConn.SqlCommandObj.ExecuteReader();
                        while (reader1.Read())
                        {
                            //if (reader1.GetString(0) == "{}")
                            //    a = 0;
                            //else
                                a = Convert.ToInt32(reader1.GetString(0));
                        }

                       
                        a = a + quantity;

                        resConn.SqlConnectionObj.Close();
                        resConn.SqlConnectionObj.Open();


                        string query3 = String.Format("Update Food_tbl Set FoodSaleTotal='{0}' where FoodId='{1}'", a, Id);
                        resConn.SqlCommandObj.CommandText = query3;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();







                    }
                    else if (i == 1)
                    {
                        resConn.SqlConnectionObj.Open();
                        FoodName = I2.Text;
                        Id = aFood.getId(FoodName);
                        //quantity = Convert.ToInt32(n2.Text);
                        quantity = Convert.ToInt32(item2DropdownList.SelectedIndex)+1;
                        string query = String.Format("INSERT INTO Sold_tbl(OrderId,FoodId,UserId,Quantity) VALUES('{0}','{1}','{2}','{3}')", OrderId, Id, UserId, quantity);
                        resConn.SqlCommandObj.CommandText = query;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();

                        /* to update food table for top items*/
                        resConn.SqlConnectionObj.Open();
                        int a = 0;
                        string query2 = String.Format("Select FoodSaleTotal from Food_tbl where FoodId='{0}'", Id);
                        resConn.SqlCommandObj.CommandText = query2;
                        SqlDataReader reader1 = resConn.SqlCommandObj.ExecuteReader();
                        while (reader1.Read())
                        {
                            //if (reader1.GetString(0) == "NULL")
                            //    a = 0;
                            //else
                                a = Convert.ToInt32(reader1.GetString(0));
                        }

                        a = a + quantity;

                        resConn.SqlConnectionObj.Close();
                        resConn.SqlConnectionObj.Open();

                        string query3 = String.Format("Update Food_tbl Set FoodSaleTotal='{0}' where FoodId='{1}'", a, Id);
                        resConn.SqlCommandObj.CommandText = query3;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();




                    }
                    else if (i == 2)
                    {
                        resConn.SqlConnectionObj.Open();
                        FoodName = I3.Text;
                        Id = aFood.getId(FoodName);
                        quantity = Convert.ToInt32(item3DropdownList.SelectedIndex)+1;
                        //quantity = Convert.ToInt32(n3.Text);
                        string query = String.Format("INSERT INTO Sold_tbl(OrderId,FoodId,UserId,Quantity) VALUES('{0}','{1}','{2}','{3}')", OrderId, Id, UserId, quantity);
                        resConn.SqlCommandObj.CommandText = query;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();


                        /* to update food table for top items*/
                        resConn.SqlConnectionObj.Open();
                        int a = 0;
                        string query2 = String.Format("Select FoodSaleTotal from Food_tbl where FoodId='{0}'", Id);
                        resConn.SqlCommandObj.CommandText = query2;
                        SqlDataReader reader1 = resConn.SqlCommandObj.ExecuteReader();
                        while (reader1.Read())
                        {
                            //if (reader1.GetString(0) == "NULL")
                            //    a = 0;
                            //else
                                a = Convert.ToInt32(reader1.GetString(0));
                        }

                        resConn.SqlConnectionObj.Close();
                        resConn.SqlConnectionObj.Open();
                        a = a + quantity;

                        string query3 = String.Format("Update Food_tbl Set FoodSaleTotal='{0}' where FoodId='{1}'", a, Id);
                        resConn.SqlCommandObj.CommandText = query3;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();


                    }
                    else if (i == 3)
                    {
                        resConn.SqlConnectionObj.Open();
                        FoodName = I4.Text;
                        Id = aFood.getId(FoodName);
                        quantity = Convert.ToInt32(item4DropdownList.SelectedIndex)+1;
                        //quantity = Convert.ToInt32(n4.Text);
                        string query = String.Format("INSERT INTO Sold_tbl(OrderId,FoodId,UserId,Quantity) VALUES('{0}','{1}','{2}','{3}')", OrderId, Id, UserId, quantity);
                        resConn.SqlCommandObj.CommandText = query;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();


                        /* to update food table for top items*/
                        resConn.SqlConnectionObj.Open();
                        int a = 0;
                        string query2 = String.Format("Select FoodSaleTotal from Food_tbl where FoodId='{0}'", Id);
                        resConn.SqlCommandObj.CommandText = query2;
                        SqlDataReader reader1 = resConn.SqlCommandObj.ExecuteReader();
                        while (reader1.Read())
                        {
                            //if (reader1.GetString(0) == "NULL")
                            //    a = 0;
                            //else
                            a = Convert.ToInt32(reader1.GetString(0));
                        }
                        resConn.SqlConnectionObj.Close();
                        a = a + quantity;
                        resConn.SqlConnectionObj.Open();
                        string query3 = String.Format("Update Food_tbl Set FoodSaleTotal='{0}' where FoodId='{1}'", a, Id);
                        resConn.SqlCommandObj.CommandText = query3;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();



                    }
                    else if (i == 4)
                    {
                        resConn.SqlConnectionObj.Open();
                        FoodName = I5.Text;
                        Id = aFood.getId(FoodName);
                        quantity = Convert.ToInt32(item5DropdownList.SelectedIndex)+1;
                        //quantity = Convert.ToInt32(n5.Text);
                        string query = String.Format("INSERT INTO Sold_tbl(OrderId,FoodId,UserId,Quantity) VALUES('{0}','{1}','{2}','{3}')", OrderId, Id, UserId, quantity);
                        resConn.SqlCommandObj.CommandText = query;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();


                        /* to update food table for top items*/
                        resConn.SqlConnectionObj.Open();
                        int a = 0;
                        string query2 = String.Format("Select FoodSaleTotal from Food_tbl where FoodId='{0}'", Id);
                        resConn.SqlCommandObj.CommandText = query2;
                        SqlDataReader reader1 = resConn.SqlCommandObj.ExecuteReader();
                        while (reader1.Read())
                        {
                            //if (reader1.GetString(0) == "NULL")
                            //    a = 0;
                            //else
                            a = Convert.ToInt32(reader1.GetString(0));
                        }
                        resConn.SqlConnectionObj.Close();
                        a = a + quantity;
                        resConn.SqlConnectionObj.Open();
                        string query3 = String.Format("Update Food_tbl Set FoodSaleTotal='{0}' where FoodId='{1}'", a, Id);
                        resConn.SqlCommandObj.CommandText = query3;
                        resConn.SqlCommandObj.ExecuteNonQuery();
                        resConn.SqlConnectionObj.Close();



                    }

                }
                //resConn.SqlConnectionObj.Close();

                string RestId = Request.QueryString["RestaurantId"];
                var url = String.Format("~/MyOrders.aspx?userId={0}&orderId={1}&RestId={2}&tAmt={3}", UserId, OrderId, RestId, Total.Text);
                Response.Redirect(url);


            }
        }

        protected void Button2_Command(object sender, CommandEventArgs e)
        {
            string a = e.CommandArgument.ToString();
        }

        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            string a = e.CommandArgument.ToString();
        }



        protected void item1DropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int N = Convert.ToInt32(NoOfItem.Text);
            int total = 0;
            Delifeelabel.Text = "Delivery Fee: ";
            DeliFeetk.Text = Label14.Text;

            int deliveryFee = Convert.ToInt32(DeliFeetk.Text);

            for (int i = 1; i <= N; i++)
            {
                if (i == 1)
                {
                    total = ((Convert.ToInt32(item1DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P1.Text)) + deliveryFee;
                }
                else if (i == 2)
                {
                    total = total + (Convert.ToInt32(item2DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P2.Text) + deliveryFee;
                }
                else if (i == 3)
                {
                    total = total + (Convert.ToInt32(item3DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P3.Text) + deliveryFee;
                }
                else if (i == 4)
                {
                    total = total + (Convert.ToInt32(item4DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P4.Text) + deliveryFee;
                }
                else if (i == 5)
                {
                    total = total + (Convert.ToInt32(item5DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P5.Text) + deliveryFee;
                }
            }
            Total.Text = total + "";
        }

        
        public void CalculateTotal()
        {
            int N = Convert.ToInt32(NoOfItem.Text);
            int total = 0;
            Delifeelabel.Text = "Delivery Fee: ";
            DeliFeetk.Text = Label14.Text;

            int deliveryFee = Convert.ToInt32(DeliFeetk.Text);

            for (int i = 1; i <= N; i++)
            {
                if (i == 1)
                {
                    total = ((Convert.ToInt32(item1DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P1.Text)) + deliveryFee;
                }
                else if (i == 2)
                {
                    total = total + (Convert.ToInt32(item2DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P2.Text) + deliveryFee;
                }
                else if (i == 3)
                {
                    total = total + (Convert.ToInt32(item3DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P3.Text) + deliveryFee;
                }
                else if (i == 4)
                {
                    total = total + (Convert.ToInt32(item4DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P4.Text) + deliveryFee;
                }
                else if (i == 5)
                {
                    total = total + (Convert.ToInt32(item5DropdownList.SelectedIndex) + 1) * Convert.ToInt32(P5.Text) + deliveryFee;
                }
            }
            Total.Text = total + "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string RestId = Request.QueryString["RestaurantId"];
            var url = String.Format("~/Review.aspx?RestaurantId={0}", RestId);
            Response.Redirect(url);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bkashAccNoLabel.Visible = true;
            bkAccNoTextBox.Visible = true;
            BkTrxLabel.Visible = true;
            BkTrxTextBox.Visible = true;
        }


    }
}