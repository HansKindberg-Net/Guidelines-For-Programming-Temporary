<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SystemInformation.ascx.cs" Inherits="Company.WebApplication.UserControls.SystemInformation"
%><div class="alert <%# this.AlertCssClass %>">
	<asp:PlaceHolder Visible="<%# !string.IsNullOrEmpty(this.Heading) %>" runat="server">
		<h2><%# this.Heading %></h2>
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