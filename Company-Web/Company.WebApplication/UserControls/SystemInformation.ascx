<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SystemInformation.ascx.cs" Inherits="Company.WebApplication.UserControls.SystemInformation"
%><div class="alert <%# this.AlertCssClass %> server-alert">
	<div class="container">
		<asp:PlaceHolder Visible="<%# !string.IsNullOrEmpty(this.Heading) %>" runat="server">
			<h1><%# this.Heading %></h1>
		</asp:PlaceHolder>
		<asp:PlaceHolder Visible="<%# !string.IsNullOrEmpty(this.Information) %>" runat="server">
			<p><%# this.Information %></p>
		</asp:PlaceHolder>
		<asp:Repeater DataSource="<%# this.InformationList %>" EnableViewState="false" runat="server">
			<HeaderTemplate>
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
					<li><%# Container.DataItem %></li>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
		</asp:Repeater>
	</div>
</div>