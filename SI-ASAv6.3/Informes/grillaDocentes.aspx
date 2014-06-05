<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="grillaDocentes.aspx.cs" Inherits="grillaDocentes" %>
<%@ Register src="~/UserControl/busqueda.ascx" tagname="Busqueda" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:Busqueda ID="busquedaPersona" runat="server" /><br />
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" /><br />
    <asp:GridView ID="grillaDocente" runat="server">
    </asp:GridView>
</asp:Content>

