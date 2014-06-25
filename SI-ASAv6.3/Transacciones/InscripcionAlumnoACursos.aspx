<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InscripcionAlumnoACursos.aspx.cs" Inherits="Transacciones_InscripcionAlumnoACursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<fieldset><legend>Inscripcion a cursado</legend>
    
    <legend>Listado de alumnos</legend>
    <label>-Seleccione un alumno-</label>
    
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
        <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" 
            onclick="btn_Buscar_Click" />
        <br /><br />
        </fieldset>
        <asp:GridView ID="gv_busqueda" runat="server" AutoGenerateColumns="False" 
        AutoGenerateSelectButton="True" 
        onselectedindexchanged="gv_busqueda_SelectedIndexChanged">
        
        <Columns>
            <asp:BoundField HeaderText="Legajo" DataField="legajo" />
            <asp:BoundField HeaderText="Nombre" DataField="alumno.nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="alumno.apellido" />
            <asp:BoundField HeaderText="Tipo Documento" 
                DataField="alumno.tipoDoc.descripcion" />
            <asp:BoundField HeaderText="Número Documento" DataField="alumno.numDoc" />
        </Columns>
    </asp:GridView>
    

    <legend>Cursos</legend><br />
    <label>-Seleccione los cursos en los que se inscribe-</label><br />
    <label>Cursos</label>
    <asp:ListBox ID="list_cursos" runat="server" 
        onselectedindexchanged="list_cursos_SelectedIndexChanged" 
        SelectionMode="Multiple"></asp:ListBox><br />
</fieldset>
<asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
        onclick="btnRegistrar_Click" />

</asp:Content>

