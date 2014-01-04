<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="false" CodeBehind="EmailForm.aspx.cs" Inherits="Company.WebApplication.Pages.HardToTest.EmailForm" %>
<%@ Register TagPrefix="UserControls" TagName="SystemInformation" Src="~/UserControls/SystemInformation.ascx" %>
<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Email-form</asp:Content>
<asp:Content ContentPlaceHolderID="HeadingContentPlaceHolder" runat="server">Email-form</asp:Content>
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<p>Send an email to a friend.</p>
	<UserControls:SystemInformation
		id="ConfirmationControl"
		Heading="Confirmation"
		Information="The email have been sent."
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
			<asp:Label AssociatedControlID="ToTextBox" runat="server">To</asp:Label>
			<asp:TextBox id="ToTextBox" CssClass="form-control" TextMode="Email" runat="server" />
		</div>
		<div class="form-group">
			<asp:Label AssociatedControlID="CopyTextBox" runat="server">Copy</asp:Label>
			<asp:TextBox id="CopyTextBox" CssClass="form-control" TextMode="Email" runat="server" />
		</div>
		<div class="form-group">
			<asp:Label AssociatedControlID="SubjectTextBox" runat="server">Subject</asp:Label>
			<asp:TextBox id="SubjectTextBox" CssClass="form-control" runat="server" />
		</div>
		<div class="form-group">
			<asp:Label AssociatedControlID="MessageTextBox" runat="server">Message</asp:Label>
			<asp:TextBox id="MessageTextBox" CssClass="form-control" Rows="10" TextMode="MultiLine" runat="server" />
		</div>
		<asp:Button CssClass="btn btn-default" OnClick="OnSendClick" Text="Send" runat="server" />
	</form>
</asp:Content>