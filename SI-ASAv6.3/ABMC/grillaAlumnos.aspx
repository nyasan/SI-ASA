<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="grillaAlumnos.aspx.cs" Inherits="Informes_grillaAlumnos" %>
<%@ Register src="~/UserControl/busqueda.ascx" tagname="Busqueda" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
        <br />
        <asp:GridView ID="gv_busqueda" runat="server" AutoGenerateColumns="False" 
            onselectedindexchanged="gv_busqueda_SelectedIndexChanged" 
            AutoGenerateSelectButton="True">
        
        <Columns>
            <asp:BoundField HeaderText="Legajo" DataField="legajo" />
            <asp:BoundField HeaderText="Nombre" DataField="nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="apellido" />
            <asp:BoundField HeaderText="Tipo Documento" DataField="descripcion" />
            <asp:BoundField HeaderText="Número Documento" DataField="numDoc" />
        </Columns>
    </asp:GridView><br />
        <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" />
    </fieldset>
</asp:Content>

