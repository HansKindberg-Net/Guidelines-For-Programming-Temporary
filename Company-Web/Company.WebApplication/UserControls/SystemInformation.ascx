<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SystemInformation.ascx.cs" Inherits="Company.WebApplication.UserControls.SystemInformation"
%><div id="server-alert" class="alert <%# this.AlertCssClass %>">
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
<div id="modal-alert" class="modal fade">
	<div class="modal-dialog">
		<div class="modal-content alert <%# this.AlertCssClass %>">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<asp:PlaceHolder Visible="<%# !string.IsNullOrEmpty(this.Heading) %>" runat="server">
					<h1><%# this.Heading %></h1>
				</asp:PlaceHolder>
			</div>
			<div class="modal-body">
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
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
			</div>
		</div><!-- /.modal-content -->
	</div><!-- /.modal-dialog -->
</div><!-- /.modal -->