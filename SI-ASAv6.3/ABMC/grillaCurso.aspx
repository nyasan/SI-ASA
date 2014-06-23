<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="grillaCurso.aspx.cs" Inherits="grillaCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   
    <script language="javascript" type="text/javascript">
// <![CDATA[


// ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<legend>Consulta de Cursos</legend>
<fieldset><legend>Búsqueda</legend>
    <label>Nombre</label><asp:TextBox ID="txtNombre" runat="server"></asp:TextBox><br />
    <br /><asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
        onclick="btnBuscar_Click" /><br /><br /></fieldset>
 <asp:GridView ID="grillaCursos" runat="server" AutoGenerateColumns="False">
     <Columns>
         <asp:BoundField DataField="nombre" HeaderText="Nombre" />
         <asp:BoundField DataField="horario.desde" HeaderText="Horario Inicio" />
         <asp:BoundField DataField="horario.hasta" HeaderText="Horario Fin" />
         <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
     </Columns>
    </asp:GridView>

</asp:Content>

