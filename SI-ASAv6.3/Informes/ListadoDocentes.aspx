<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListadoDocentes.aspx.cs" Inherits="Informes_ListadoDocentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<fieldset>
<legend>Filtrado</legend>
<label>Inscriptos desde: </label><asp:TextBox ID="txt_inscDesde" runat="server"></asp:TextBox><label>Hasta:</label><asp:TextBox
        ID="txt_inscHasta" runat="server"></asp:TextBox><br />
    <label>Curso</label><asp:ListBox ID="lb_cursos" runat="server" SelectionMode="Multiple"></asp:ListBox><br />
    <label>Legajo desde: </label>
    <asp:TextBox ID="txt_legDesde" runat="server"></asp:TextBox><label>Hasta</label><asp:TextBox
        ID="txt_legHasta" runat="server"></asp:TextBox><br /><br />
    <asp:Button ID="bt_filtrar" runat="server" Text="Filtrar" 
        onclick="bt_filtrar_Click" />
</fieldset>
    <asp:GridView ID="grillaDocentes" runat="server">
    </asp:GridView>
</asp:Content>

