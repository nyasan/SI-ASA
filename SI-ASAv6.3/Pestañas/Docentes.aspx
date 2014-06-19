<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Docentes.aspx.cs" Inherits="Pestañas_Docentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="hl_nuevo" runat="server" NavigateUrl="~/ABMC/Docente.aspx">Nuevo Docente</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_modificar" runat="server" NavigateUrl="~/ABMC/Docente.aspx">Modificar Docente</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_eliminar" runat="server" NavigateUrl="~/ABMC/Docente.aspx">Eliminar Docente</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_consultar" runat="server" NavigateUrl="~/ABMC/grillaDocentes.aspx">Consultar Docente</asp:HyperLink>
</asp:Content>

