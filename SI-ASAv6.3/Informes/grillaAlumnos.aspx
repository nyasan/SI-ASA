<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="grillaAlumnos.aspx.cs" Inherits="Informes_grillaAlumnos" %>
<%@ Register src="~/UserControl/busqueda.ascx" tagname="Busqueda" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc1:Busqueda ID="busquedaPersona" runat="server" /><br />
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
        onclick="btnBuscar_Click" />
    <asp:GridView ID="grillaAlumnos" runat="server">
    </asp:GridView>
</asp:Content>

