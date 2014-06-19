<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Alumnos.aspx.cs" Inherits="Pestañas_Alumnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="hl_nuevo" runat="server" NavigateUrl="~/Pestañas/Alumnos.aspx">Nuevo Alumno</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_modificar" runat="server" NavigateUrl="~/Pestañas/Alumnos.aspx">Modificar Alumno</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_eliminar" runat="server" NavigateUrl="~/Pestañas/Alumnos.aspx">Eliminar Alumno</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_consultar" runat="server" NavigateUrl="~/Pestañas/grillaAlumnos.aspx">Consultar Alumno</asp:HyperLink>
</asp:Content>

