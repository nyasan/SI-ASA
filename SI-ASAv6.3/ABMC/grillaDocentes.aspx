<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="grillaDocentes.aspx.cs" Inherits="grillaDocentes" %>
<%@ Register src="~/UserControl/busqueda.ascx" tagname="Busqueda" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <legend>Consulta de Docentes</legend>
    <fieldset><legend>Busqueda</legend>
        <label>Tipo de Documento</label>
        <asp:DropDownList ID="ddl_tipoDoc" runat="server"></asp:DropDownList><br />
        <label>Número de Documento</label>
        <asp:TextBox ID="txt_numeroDoc" runat="server"></asp:TextBox><br />
        <label>Nombre</label>
        <asp:TextBox ID="txt_nombre" runat="server"></asp:TextBox><br />
        <label>Apellido</label>
        <asp:TextBox ID="txt_apellido" runat="server"></asp:TextBox><br />
        <label>Legajo</label>
        <asp:TextBox ID="txt_legajo" runat="server"></asp:TextBox><br /><br />
        <br /><asp:Button ID="btn_Buscar" runat="server" Text="Buscar" 
            onclick="btn_Buscar_Click" />
        </fieldset>
        <asp:GridView ID="grillaDocente" runat="server" AutoGenerateColumns="False" 
            onselectedindexchanged="grillaDocente_SelectedIndexChanged" 
            AutoGenerateSelectButton="True">
        <Columns>
            <asp:BoundField HeaderText="Legajo" DataField="legajo" />
            <asp:BoundField HeaderText="Nombre" DataField="docente.nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="docente.apellido" />
            <asp:BoundField HeaderText="Tipo Documento" 
                DataField="docente.tipoDoc.descripcion" />
            <asp:BoundField HeaderText="Número Documento" DataField="docente.numDoc" />
        </Columns>
    </asp:GridView><br />
    </asp:Content>

