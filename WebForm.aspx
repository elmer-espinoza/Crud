<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="Crud.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:DataList ID="DataList1" runat="server" DataKeyField="Categoria_id" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                Categoria_id:
                <asp:Label ID="Categoria_idLabel" runat="server" Text='<%# Eval("Categoria_id") %>' />
                <br />
                Descripcion:
                <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>' />
                <br />
<br />
            </ItemTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cnStr %>" SelectCommand="SELECT * FROM [Categoria]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Categoria_id" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Categoria_id" HeaderText="Categoria_id" ReadOnly="True" SortExpression="Categoria_id" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
