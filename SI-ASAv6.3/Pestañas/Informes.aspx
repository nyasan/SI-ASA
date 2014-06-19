<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Informes.aspx.cs" Inherits="Pestañas_Informes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="hl_alumnos" runat="server" NavigateUrl="~/Informes/ListadoAlumnos.aspx">Alumnos</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_cursos" runat="server" NavigateUrl="~/Informes/ListadoCursos.aspx">Cursos</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_docentes" runat="server" NavigateUrl="~/Informes/ListadoDocentes.aspx">Docentes</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_ausentismoAlumnos" runat="server" NavigateUrl="~/Informes/Listado_faltas_x_curso.rdlc">Ausentismo de Alumnos</asp:HyperLink>
</asp:Content>

