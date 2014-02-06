<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="Database.aspx.cs" Inherits="Company.WebApplication.Pages.HardToTest.Database" %>
<%@ Import Namespace="Company.Data.Databases" %>
<%@ Import Namespace="Company.Data.Entities" %>
<%@ Import Namespace="Company.WebApplication.Business.Data.Databases" %>
<%@ Register TagPrefix="UserControls" TagName="SystemInformation" Src="~/UserControls/SystemInformation.ascx" %>
<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Database</asp:Content>
<asp:Content ContentPlaceHolderID="HeadingContentPlaceHolder" runat="server">Database</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<p>Read/write data from/to a database.</p>
	<UserControls:SystemInformation
		id="ConfirmationControl"
		Heading="Confirmation"
		Information="The data have been saved."
		Type="Confirmation"
		Visible="false"
		runat="server"
	/>
	<UserControls:SystemInformation
		id="ExceptionControl"
		Heading="Error"
		Information="Invalid input."
		Type="Exception"
		Visible="false"
		runat="server"
	/>
	<form role="form" runat="server">
		<div class="form-group">
			<asp:Label AssociatedControlID="KeyTextBox" runat="server">Key</asp:Label>
			<asp:TextBox id="KeyTextBox" CssClass="form-control" MaxLength="50" TextMode="SingleLine" runat="server" />
		</div>
		<div class="form-group">
			<asp:Label AssociatedControlID="ValueTextBox" runat="server">Value</asp:Label>
			<asp:TextBox id="ValueTextBox" CssClass="form-control" MaxLength="50" TextMode="SingleLine" runat="server" />
		</div>
		<asp:Button CssClass="btn btn-default" OnClick="OnSaveClick" Text="Save" runat="server" />
	</form>
	<asp:Repeater id="ExampleItemRepeater" DataSource="<%# this.ExampleItems %>" runat="server">
		<HeaderTemplate>
			<table>
				<thead>
					<tr>
						<th>Id</th>
						<th>Key</th>
						<th>Value</th>
					</tr>
				</thead>
				<tbody>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td><%# ((IExampleItem)Container.DataItem).Id %></td>
				<td><%# ((IExampleItem)Container.DataItem).Key %></td>
				<td><%# ((IExampleItem)Container.DataItem).Value %></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
				</tbody>
			</table>
		</FooterTemplate>
	</asp:Repeater>
</asp:Content>