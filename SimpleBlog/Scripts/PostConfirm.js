(function ($) {

	var $confirmItems = $("[data-confirm]");

	var $body = $("body");

	$confirmItems.on("click", function (e) {

		e.preventDefault();

		var $this = $(this);

		var url = $this.attr("href");

		var confirmation = $this.attr("data-confirm");

		var confirmed = true;

		if (confirmation) {

			confirmed = window.confirm(confirmation);
		}

		if (confirmed) {

			var $antiForgeryTokenElement = $(".js-anti-forgery-token").clone();

			$("<form/>").attr("action", url).attr("method", "post").append($antiForgeryTokenElement).appendTo($body).submit();
		}
	});

})(jQuery);
