<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Curso.aspx.cs" Inherits="ABMC_Curso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/ABMC.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset class="Datos"><legend>Curso</legend>
        <label>ID Curso</label>
        <asp:Label ID="lbl_IdCurso" runat="server"></asp:Label><br />

        <label>Nombre</label>
        <asp:TextBox ID="txt_nombre" runat="server"></asp:TextBox><br />

        <fieldset><legend>Horario</legend>
            <label>Desde:</label>
            <asp:TextBox ID="txt_Desde" runat="server"></asp:TextBox><br />

            <label>Hasta:</label>
            <asp:TextBox ID="txt_Hasta" runat="server"></asp:TextBox><br />
        </fieldset>

        <label id="descripcion_curso">Descripción</label>
        <asp:TextBox ID="txt_Descripcion" runat="server" TextMode="MultiLine"></asp:TextBox><br /><br />
        
        <asp:Button ID="btn_Eliminar" runat="server" Text="Eliminar" />
        <asp:Button ID="btn_Guardar" runat="server" Text="Guardar" />
    </fieldset>
</asp:Content>

