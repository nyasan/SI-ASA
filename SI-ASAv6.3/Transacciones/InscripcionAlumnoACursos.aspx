<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InscripcionAlumnoACursos.aspx.cs" Inherits="Transacciones_InscripcionAlumnoACursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<fieldset><legend>Inscripcion a cursado</legend>
    
    <legend>Listado de alumnos</legend>
    <label>Seleccione un alumno</label>
    
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
        <asp:GridView ID="gv_busqueda" runat="server" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" 
            onselectedindexchanged="gv_busqueda_SelectedIndexChanged">
        
        <Columns>
            <asp:BoundField HeaderText="Legajo" DataField="legajo" />
            <asp:BoundField HeaderText="Nombre" DataField="nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="apellido" />
            <asp:BoundField HeaderText="Tipo Documento" DataField="descripcion" />
            <asp:BoundField HeaderText="Número Documento" DataField="numDoc" />
        </Columns>
    </asp:GridView>
    </fieldset>

    <legend>Cursos</legend><br />
    <label>Seleccione los cursos en los que se inscribe</label><br />
    <label>Cursos</label>
    <asp:ListBox ID="list_cursos" runat="server" 
        onselectedindexchanged="list_cursos_SelectedIndexChanged"></asp:ListBox><br />
</fieldset>
<asp:Button ID="btnRegistrar" runat="server" Text="Registrar" />

</asp:Content>

