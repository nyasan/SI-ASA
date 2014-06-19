<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cursos.aspx.cs" Inherits="Pestañas_Cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="hl_nuevo" runat="server" NavigateUrl="~/ABMC/Curso.aspx">Nuevo Curso</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_modificar" runat="server" NavigateUrl="~/ABMC/Curso.aspx">Modificar Curso</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_eliminar" runat="server" NavigateUrl="~/ABMC/Curso.aspx">Eliminar Curso</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_consultar" runat="server" NavigateUrl="~/ABMC/grillaCurso.aspx">Consultar Cursos</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_inscribirAlumnos" runat="server" NavigateUrl="~/Transacciones/InscripcionAlumnoACursos.aspx">Inscripcion de Alumnos</asp:HyperLink>
    <br /><asp:HyperLink ID="hl_asignarDocentes" runat="server" NavigateUrl="~/Transacciones/InscripcionDocenteACursos.aspx">Gestionar asignacion de Docentes</asp:HyperLink>
</asp:Content>

