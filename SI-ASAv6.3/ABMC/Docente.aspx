<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Docente.aspx.cs" Inherits="ABMC_Docente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/ABMC.css" rel="stylesheet" type="text/css" />
<link href="../Styles/ABMC.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<fieldset class="Datos"><legend>Docente</legend>
    <fieldset><legend>Datos del Docente</legend>
        <label>Legajo: </label>
        <asp:TextBox ID="txt_legajo" runat="server" Enabled="False"></asp:TextBox><br />
        
        <fieldset> <legend>Horarios de Trabajo</legend><br />
            <label>Desde: </label><asp:TextBox ID="txt_horaDesde" runat="server"></asp:TextBox><br />
            <label>Hasta: </label><asp:TextBox ID="txt_horaHasta" runat="server"></asp:TextBox><br />
        </fieldset>
        
        <label>Salario: </label>
        <asp:TextBox ID="txt_salario" runat="server"></asp:TextBox><br />
    </fieldset>

    <fieldset><legend>Datos Personales</legend>
            <label>Nombre</label>
            <asp:TextBox ID="txt_Nombre" runat="server"></asp:TextBox><br />

            <label>Apellido</label>
            <asp:TextBox ID="txt_Apellido" runat="server"></asp:TextBox><br />

            <label>Tipo Documento</label>
            <asp:DropDownList ID="ddl_TipoDoc" runat="server"></asp:DropDownList><br />

            <label>Documento</label>
            <asp:TextBox ID="txt_NumDoc" runat="server"></asp:TextBox><br />

            <label>Domicilio</label>
            <asp:TextBox ID="txt_Domicilio" runat="server"></asp:TextBox><br />

            <label>Teléfono</label>
            <asp:TextBox ID="txt_Telefono" runat="server"></asp:TextBox><br />

            <label>Celular</label>
            <asp:TextBox ID="txt_Celular" runat="server"></asp:TextBox><br />

            <label>E-mail</label>
            <asp:TextBox ID="txt_mail" runat="server"></asp:TextBox><br />

            <label>Fecha de Nacimiento</label>
            <asp:TextBox ID="txt_FechaNacimiento" runat="server"></asp:TextBox><br />

        </fieldset>

        <asp:Button ID="btn_Eliminar" runat="server" Text="Eliminar" 
        onclick="btn_Eliminar_Click" />
        <asp:Button ID="btn_Guardar" runat="server" Text="Guardar" 
        onclick="btn_Guardar_Click" />

        </fieldset>
</asp:Content>

