<%@ Page Title="" Language="C#" MasterPageFile="~/foodfest.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="FoodFest.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="under_tell_us">
        <div id="restaurant_logo">
            <asp:Image ID="Image2" runat="server" />
        </div>
        <div id="restaurant_address">

            <asp:Label ID="Label5" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label9" runat="server" Text="Phone: "></asp:Label>
            <asp:Label ID="Label10" runat="server" Text=""></asp:Label>

        </div>
        <div id="restaurant_status">
            <asp:Label ID="Label11" runat="server" Text="Minimum Order value: "></asp:Label>
            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label13" runat="server" Text="Delivery Fee: "></asp:Label>
            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="Label15" runat="server" Text="Delivery Time: "></asp:Label>
            <asp:Label ID="Label16" runat="server" Text="1 hour"></asp:Label>
            <br />
            <asp:Label ID="Label17" runat="server" Text="Open From"></asp:Label>
            <asp:Label ID="Label18" runat="server" Text=""></asp:Label> -
            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="under_tell_us_1">
    <div id="cont_img_restaurant"><div id="title_gallery_food">Reviews</div>
          

                <%--<div class="menu_name">--%>  
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FoodFestConn %>" SelectCommand="SELECT Review_tbl.Review, Review_tbl.ReviewDate, User_tbl.UserName FROM Review_tbl INNER JOIN User_tbl ON Review_tbl.UserId = User_tbl.UserId WHERE (Review_tbl.RestaurantId = 2)"></asp:SqlDataSource>
                      <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                             <div class="menu_name"> 
                            
                                 <%-- <div class="repeat_content">--%>
                                      <div class="restaurant_top_2"></div>
                    <div class="image_show">
                      <asp:Label ID="Label3" runat="server" Text='<%#Eval("UserName") %>'></asp:Label><asp:Label ID="Label1" runat="server" Text=" said:"></asp:Label>
                    </div>
                    <div class="restaurant_review_show_our" style="font-size: medium; font-family: Calibri">
                       
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Review") %>'></asp:Label>

                    
                </div>
                     <%--<div class="goToMenuButton">
                      

                     </div>--%>
              <%--  </div>--%>


                                 </div>
                        </ItemTemplate>
                    </asp:Repeater>

                <%--</div>--%>
                 
        </div>
         </div>
    <div id="cont_add_to_cart">

    
     <div id="your_order_title1">
                    Post a Review

                 </div>
    <div id="calculate_price1">
        <asp:TextBox ID="TextBox1" runat="server" Height="146px" TextMode="MultiLine"></asp:TextBox><br/>
        <asp:Button ID="Button1" runat="server" Text="Post" OnClick="Button1_Click" BackColor="GreenYellow" BorderColor="Black"  Font-Names="Calibri" Font-Bold="true" Font-Size="Medium" width="63%" />

                    <%--  <div class="startMyOrderButton3">Get Your Food</div>--%>
                    <br />
        <asp:Label ID="msgLabel" runat="server"></asp:Label>
                    <br />
             </div>
</asp:Content>
