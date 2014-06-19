<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Transacciones.aspx.cs" Inherits="Pestañas_Transacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="hl_asistenciaAlumnos" runat="server" NavigateUrl="~/Transacciones/AsistenciaAlumnosACurso.aspx">Gestionar asistencia de Alumnos</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_inscribirAlumnos" runat="server" NavigateUrl="~/Transacciones/InscripcionAlumnoACursos.aspx">Inscripcion de Alumnos a Cursos</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_asignarDocentes" runat="server" NavigateUrl="~/Transacciones/InscripcionDocenteACursos.aspx">Gestionar asignacion de Docentes</asp:HyperLink>
</asp:Content>

