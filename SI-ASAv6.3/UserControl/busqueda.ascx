<%@ Control Language="C#" AutoEventWireup="true" CodeFile="busqueda.ascx.cs" Inherits="User_Control_busqueda" %>


<fieldset>
    <legend>Búsqueda</legend><br />
    
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
<br /><br />

    <asp:GridView ID="grv_grillaResultante" runat="server" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="Legajo" />
            <asp:BoundField HeaderText="Nombre" />
            <asp:BoundField HeaderText="Apellido" />
            <asp:BoundField HeaderText="Tipo Documento" />
            <asp:BoundField HeaderText="Número Documento" />
        </Columns>
    </asp:GridView>
</fieldset>