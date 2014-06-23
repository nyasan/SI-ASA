<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AsistenciaAlumnosACurso.aspx.cs" Inherits="Transacciones_AsistenciaAlumnosACurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<fieldset><legend>Asistencia a Cursado</legend>
    <label>Cursos</label>

    <asp:DropDownList ID="ddl_Curso" runat="server"></asp:DropDownList><br />

    <br /><label>Seleccionar los alumnos que no esten presentes.</label>

    <asp:GridView ID="gv_grillaAlumnos" runat="server" AutoGenerateColumns="False" 
        AutoGenerateSelectButton="True" 
        onselectedindexchanged="gv_grillaAlumnos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="legajo" HeaderText="Legajo" />
            <asp:BoundField DataField="alumno.nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="alumno.apellido" HeaderText="Apellido" />
        </Columns>
    </asp:GridView>
    <br />
    </fieldset>
    <asp:Button ID="btn_registrar" runat="server" Text="Registrar Asistencia" 
        onclick="btn_registrar_Click" />

</asp:Content>

