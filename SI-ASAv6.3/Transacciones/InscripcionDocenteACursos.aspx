<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InscripcionDocenteACursos.aspx.cs" Inherits="Transacciones_InscripcionDocenteACursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset><legend>Inscripcion de Docentes</legend>
    <asp:Label ID="lblDocente" runat="server" Text="Label">-Buscar docentes y seleccionar uno-</asp:Label>
   <br />
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
            <asp:BoundField HeaderText="Nombre" DataField="docente.nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="docente.apellido" />
            <asp:BoundField HeaderText="Tipo Documento" 
                DataField="docente.tipoDoc.descripcion" />
            <asp:BoundField HeaderText="Número Documento" DataField="docente.numDoc" />
        </Columns>
    </asp:GridView>

    <legend>Cursos</legend>
    <br />
    <label>-Seleccionar los cursos en donde el docente dara clases-</label><br />
    <br />
    <label>Cursos</label>
    <asp:ListBox ID="list_cursos" runat="server" 
            onselectedindexchanged="list_cursos_SelectedIndexChanged" 
            SelectionMode="Multiple"></asp:ListBox>
    </fieldset>
    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
        onclick="btnRegistrar_Click" />
</asp:Content>

