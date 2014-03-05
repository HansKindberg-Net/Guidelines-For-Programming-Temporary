<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="DatabaseInteraction.aspx.cs" Inherits="Company.WebApplication.Pages.HardToTest.DatabaseInteraction" %>
<%@ Import Namespace="Company.Data.Entities" %>
<%@ Register TagPrefix="UserControls" TagName="SystemInformation" Src="~/UserControls/SystemInformation.ascx" %>
<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Database-interaction</asp:Content>
<asp:Content ContentPlaceHolderID="HeadingContentPlaceHolder" runat="server">Database-interaction</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<p>Read/write data from/to a database.</p>
	<asp:PlaceHolder id="AddNewItemPlaceHolder" runat="server">
		<p><a href="<%# this.AddNewItemUrl %>">Add new item</a></p>
	</asp:PlaceHolder>
	<UserControls:SystemInformation
		id="ConfirmationControl"
		Heading="Confirmation"
		Information="The data have been saved."
		Type="Confirmation"
		Visible="false"
		runat="server"
	/>
	<UserControls:SystemInformation
		id="WarningControl"
		Heading="Warning"
		Information="There are no items matching the search-criterias."
		Type="Warning"
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
	<asp:PlaceHolder id="EditPlaceHolder" Visible="<%# this.Edit %>" runat="server">
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
	</asp:PlaceHolder>
	<asp:PlaceHolder id="SearchPlaceHolder" Visible="<%# !this.Edit %>" runat="server">
		<h2>Search</h2>
		<form role="form" runat="server">
			<div class="form-group">
				<asp:Label AssociatedControlID="KeyCriteriaTextBox" runat="server">Key</asp:Label>
				<asp:TextBox id="KeyCriteriaTextBox" CssClass="form-control" MaxLength="50" TextMode="SingleLine" runat="server" />
			</div>
			<div class="form-group">
				<asp:Label AssociatedControlID="ValueCriteriaTextBox" runat="server">Value</asp:Label>
				<asp:TextBox id="ValueCriteriaTextBox" CssClass="form-control" MaxLength="50" TextMode="SingleLine" runat="server" />
			</div>
			<asp:Button CssClass="btn btn-default" OnClick="OnSearchClick" Text="Search" runat="server" />
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
					<td><%# ((IExampleItem) Container.DataItem).Id %></td>
					<td><%# ((IExampleItem) Container.DataItem).Key %></td>
					<td><%# ((IExampleItem) Container.DataItem).Value %></td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
					</tbody>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</asp:PlaceHolder>
</asp:Content>