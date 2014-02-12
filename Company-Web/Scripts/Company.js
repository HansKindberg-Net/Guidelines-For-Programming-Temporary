function fixServerAlert() {
	var serverAlert = $(".server-alert");
	
	if (serverAlert.length > 0) {
		var alertHeading = $(".server-alert .container h1");
		var alertHeadingHtml = $("<div>").append(alertHeading.clone()).html();
		alertHeading.remove();
		
		var alertInformationHtml = $(".server-alert .container").html();

		//serverAlert.removeClass("alert");
		serverAlert.removeClass("server-alert");
		var alertClasses = serverAlert.attr("class");
		serverAlert.remove();

		$("body").prepend('<div id="modal" class="modal fade"><div class="modal-dialog"><div class="modal-content ' + alertClasses + '"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' + alertHeadingHtml + '</div><div class="modal-body">' + alertInformationHtml + '</div><div class="modal-footer"><button type="button" class="btn btn-default" data-dismiss="modal">Close</button></div></div><!-- /.modal-content --></div><!-- /.modal-dialog --></div><!-- /.modal -->');
		$("#modal").modal("show");
	}
}

$(document).ready(function () {
	fixServerAlert();
});