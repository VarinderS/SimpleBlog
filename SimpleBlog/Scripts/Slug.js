(function ($) {

	var $slugInput = $(".js-slug-input");

	var slugFromElement = $slugInput.attr("data-slugFrom");

	var $slugInputDisplay = $(".js-slug-input-display");

	var $slugFromElement = $(slugFromElement);

	$slugFromElement.on("keyup", function (e) {

		var $this = $(this);

		var value = $this.val();

		var slug = value.replace(/[^a-zA-Z0-9\s]/g, "");

		slug = slug.toLowerCase();

		slug = slug.replace(/\s+/g, "-");

		if (slug.charAt(slug.length - 1) == "-") {

			slug = slug.substr(0, slug.length - 1);
		}

		$slugInput.val(slug);

		$slugInputDisplay.text(slug);
	});

})(jQuery);