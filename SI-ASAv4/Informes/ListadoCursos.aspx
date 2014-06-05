<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListadoCursos.aspx.cs" Inherits="Informes_ListadoCursos" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<fieldset>
<legend>Filtrado</legend>
<label>Cursos del Alumno (legajo): </label>
    <asp:TextBox ID="txt_alumno" runat="server"></asp:TextBox><br />
    <label>Horario desde: </label>
    <asp:TextBox ID="txt_horarioDesde" runat="server"></asp:TextBox>
    <label>Hasta: </label><asp:TextBox ID="txt_horarioHasta" runat="server"></asp:TextBox><br />
    <label>Cursos del Docente (legajo): </label>
    <asp:TextBox ID="txt_docente" runat="server"></asp:TextBox><br /><br />
    <asp:Button ID="bt_filtrar" runat="server" Text="Filtrar" 
        onclick="bt_filtrar_Click" />
</fieldset>
    <asp:GridView ID="grillaCursos" runat="server">
    </asp:GridView>
</asp:Content>

