$(document).ready(function () {
	$("#server-alert").remove();
	$("#modal-alert").modal("show");
	window.setTimeout(function () {
		$("#modal-alert").modal('hide');
	}, 5000);
});