﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAlert.aspx.cs" Inherits="FoodFest.TestAlert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="RestaurantName" DataValueField="RestaurantId"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FoodFestConn %>" SelectCommand="SELECT [RestaurantId], [RestaurantName] FROM [Restaurant_tbl]"></asp:SqlDataSource>
        <\br>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="FoodId" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="FoodId" HeaderText="FoodId" InsertVisible="False" ReadOnly="True" SortExpression="FoodId" />
                <asp:BoundField DataField="FoodName" HeaderText="FoodName" SortExpression="FoodName" />
                <asp:BoundField DataField="FoodPrice" HeaderText="FoodPrice" SortExpression="FoodPrice" />
                <asp:BoundField DataField="FoodType" HeaderText="FoodType" SortExpression="FoodType" />
                <asp:BoundField DataField="FoodSaleTotal" HeaderText="FoodSaleTotal" SortExpression="FoodSaleTotal" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
    
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:FoodFestConn %>" DeleteCommand="DELETE FROM [Food_tbl] WHERE [FoodId] = @original_FoodId AND [FoodName] = @original_FoodName AND [FoodPrice] = @original_FoodPrice AND (([FoodType] = @original_FoodType) OR ([FoodType] IS NULL AND @original_FoodType IS NULL)) AND (([FoodSaleTotal] = @original_FoodSaleTotal) OR ([FoodSaleTotal] IS NULL AND @original_FoodSaleTotal IS NULL))" InsertCommand="INSERT INTO [Food_tbl] ([FoodName], [FoodPrice], [FoodType], [FoodSaleTotal]) VALUES (@FoodName, @FoodPrice, @FoodType, @FoodSaleTotal)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [FoodId], [FoodName], [FoodPrice], [FoodType], [FoodSaleTotal] FROM [Food_tbl] WHERE ([RestaurantId] = @RestaurantId)" UpdateCommand="UPDATE [Food_tbl] SET [FoodName] = @FoodName, [FoodPrice] = @FoodPrice, [FoodType] = @FoodType, [FoodSaleTotal] = @FoodSaleTotal WHERE [FoodId] = @original_FoodId AND [FoodName] = @original_FoodName AND [FoodPrice] = @original_FoodPrice AND (([FoodType] = @original_FoodType) OR ([FoodType] IS NULL AND @original_FoodType IS NULL)) AND (([FoodSaleTotal] = @original_FoodSaleTotal) OR ([FoodSaleTotal] IS NULL AND @original_FoodSaleTotal IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_FoodId" Type="Int32" />
                <asp:Parameter Name="original_FoodName" Type="String" />
                <asp:Parameter Name="original_FoodPrice" Type="String" />
                <asp:Parameter Name="original_FoodType" Type="String" />
                <asp:Parameter Name="original_FoodSaleTotal" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="FoodName" Type="String" />
                <asp:Parameter Name="FoodPrice" Type="String" />
                <asp:Parameter Name="FoodType" Type="String" />
                <asp:Parameter Name="FoodSaleTotal" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="RestaurantId" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="FoodName" Type="String" />
                <asp:Parameter Name="FoodPrice" Type="String" />
                <asp:Parameter Name="FoodType" Type="String" />
                <asp:Parameter Name="FoodSaleTotal" Type="String" />
                <asp:Parameter Name="original_FoodId" Type="Int32" />
                <asp:Parameter Name="original_FoodName" Type="String" />
                <asp:Parameter Name="original_FoodPrice" Type="String" />
                <asp:Parameter Name="original_FoodType" Type="String" />
                <asp:Parameter Name="original_FoodSaleTotal" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
