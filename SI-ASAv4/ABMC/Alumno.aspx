﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Alumno.aspx.cs" Inherits="ABMC_Alumno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/ABMC.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <fieldset class="Datos">
        <legend>Alumno</legend>
        <fieldset><legend>Datos Personales</legend>
            <label>Legajo</label>
            <asp:TextBox ID="txt_legajo" runat="server"></asp:TextBox> <br />
            
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
        <fieldset><legend>Datos de la Madre</legend>
            <label>Nombre</label>
            <asp:TextBox ID="txt_NombreMadre" runat="server"></asp:TextBox><br />

            <label>Apellido</label>
            <asp:TextBox ID="txt_ApellidoMadre" runat="server"></asp:TextBox><br />

            <label>Tipo Documento</label>
            <asp:DropDownList ID="ddl_TipoDocMadre" runat="server"></asp:DropDownList><br />

            <label>Documento</label>
            <asp:TextBox ID="txt_NumDocMadre" runat="server"></asp:TextBox><br />

            <label>Domicilio</label>
            <asp:TextBox ID="txt_DomicilioMadre" runat="server"></asp:TextBox><br />

            <label>Teléfono</label>
            <asp:TextBox ID="txt_TelefonoMadre" runat="server"></asp:TextBox><br />

            <label>Celular</label>
            <asp:TextBox ID="txt_CelularMadre" runat="server"></asp:TextBox><br />

            <label>E-mail</label>
            <asp:TextBox ID="txt_MailMadre" runat="server"></asp:TextBox><br />

            <label>Fecha de Nacimiento</label>
            <asp:TextBox ID="txt_FechaNacimientoMadre" runat="server"></asp:TextBox><br />
        </fieldset>
        <fieldset><legend>Datos del Padre</legend>
            <label>Nombre</label>
            <asp:TextBox ID="txt_NombrePadre" runat="server"></asp:TextBox><br />

            <label>Apellido</label>
            <asp:TextBox ID="txt_ApellidoPadre" runat="server"></asp:TextBox><br />

            <label>Tipo Documento</label>
            <asp:DropDownList ID="ddl_TipoDocPadre" runat="server"></asp:DropDownList><br />

            <label>Documento</label>
            <asp:TextBox ID="txt_NumDocPadre" runat="server"></asp:TextBox><br />

            <label>Domicilio</label>
            <asp:TextBox ID="txt_DomicilioPadre" runat="server"></asp:TextBox><br />

            <label>Teléfono</label>
            <asp:TextBox ID="txt_TelefonoPadre" runat="server"></asp:TextBox><br />

            <label>Celular</label>
            <asp:TextBox ID="txt_CelularPadre" runat="server"></asp:TextBox><br />

            <label>E-mail</label>
            <asp:TextBox ID="txt_MailPadre" runat="server"></asp:TextBox><br />

            <label>Fecha de Nacimiento</label>
            <asp:TextBox ID="txt_FechaNacimientoPadre" runat="server"></asp:TextBox><br />
        </fieldset>
        <fieldset id="informacion_extra"><legend>Información Extra</legend>
            <label>Conocimientos musicales</label>
            <asp:RadioButton ID="opt_Si" runat="server" Text="Sí" GroupName="ConocimientosMusicales" />
            <asp:RadioButton ID="opt_No" runat="server" Text="No" GroupName="ConocimientosMusicales" /><br />

            <label>Nivel de Estudio</label>
            <asp:DropDownList ID="ddl_NivelEstudio" runat="server"></asp:DropDownList>
        </fieldset>
        <asp:Button ID="btn_Eliminar" runat="server" Text="Eliminar" 
            onclick="btn_Eliminar_Click" />
        <asp:Button ID="btn_Guardar" runat="server" Text="Guardar" 
            onclick="btn_Guardar_Click" />
    </fieldset>
    </div>
</asp:Content>

