<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InscripcionDocenteACursos.aspx.cs" Inherits="Transacciones_InscripcionDocenteACursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<label>Lista de docentes:</label><asp:ListBox ID="listaDocentes" runat="server"></asp:ListBox><br />
<label>Curso:</label><asp:DropDownList ID="dropCurso" runat="server">
    </asp:DropDownList><br />
    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
        onclick="btnRegistrar_Click" />
</asp:Content>

