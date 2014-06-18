<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InscripcionDocenteACursos.aspx.cs" Inherits="Transacciones_InscripcionDocenteACursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset><legend>Inscripcion de Docentes</legend>
    
    <label>Lista de Docentes</label><br />
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
        <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" />
        <br /><br />
        <asp:GridView ID="gv_busqueda" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True">
        
        <Columns>
            <asp:BoundField HeaderText="Legajo" DataField="legajo" />
            <asp:BoundField HeaderText="Nombre" DataField="nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="apellido" />
            <asp:BoundField HeaderText="Tipo Documento" DataField="descripcion" />
            <asp:BoundField HeaderText="Número Documento" DataField="numDoc" />
        </Columns>
    </asp:GridView>
    </fieldset>

    <label>Cursos</label>
    <asp:DropDownList ID="dropCurso" runat="server"></asp:DropDownList><br />
    </fieldset>
    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
        onclick="btnRegistrar_Click" />
</asp:Content>

