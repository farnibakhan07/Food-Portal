<%@ Page Title="" Language="C#" MasterPageFile="~/FoodFest_admin.Master" AutoEventWireup="true" CodeBehind="Restaurant_admin.aspx.cs" Inherits="FoodFest.Restaurant_admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="under_tell_us_header">
          <div class="button_align">
              <asp:Button ID="AddRestaurantButton" runat="server" style="margin-left: 69px; margin-top: 13px" Text="Add Restaurants" Width="280px" BackColor="#FF6600" Height="35px" BorderColor="Black" OnClick="AddRestaurnatButton_Click" PostBackUrl="~/Add_restaurant.aspx" />
                </div>
          <div class="button_align1">
               <asp:Button ID="Button1" runat="server" style="margin-left: 69px; margin-top: 13px" Text="Edit Restaurants" Width="280px" Height="35px" BackColor="#FF6600" BorderColor="Black" />
          </div>
      </div>
     <div class="under_tell_us_header">
          <div class="button_align">
              <asp:Button ID="Button2" runat="server" style="margin-left: 69px; margin-top: 13px" Height="35px" Text="Delete Restaurants" Width="280px" BackColor="#FF6600" BorderColor="Black" />
                </div>
          <div class="button_align1">
               <asp:Button ID="Button3" runat="server" style="margin-left: 69px; margin-top: 13px" Height="35px" Text="View All Restaurants" Width="280px" BackColor="#FF6600" BorderColor="Black" PostBackUrl="~/AdminViewAllRestaurants.aspx"/>
          </div>
      </div>
      
</asp:Content>
