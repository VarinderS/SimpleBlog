(function ($) {

	var $tagSelectorInput = $(".js-tag-selector-input");

	var $tagSelectorSubmit = $(".js-tag-selector-submit");


	var $tagSelector = $(".js-tag-selector");

	var $tagSelectorBody = $tagSelector.find(".tag-selector-body");


	var $tagSelectorTemplate = $(".js-tag-selector-template");


	$tagSelectorInput.on("keydown", function (e) {

		if (e.which === 13) {

			e.preventDefault();

			var $input = $(this);

			var value = $input.val();

			console.log("keydown value: ", value);

			addTag(value);
		}
	});


	$tagSelectorInput.on("keyup", function (e) {

		e.preventDefault();

		var $input = $(this);

		var value = $input.val();

		if (e.which != 13) {

			if (value) {

				$tagSelectorSubmit.removeClass("disabled");

			} else {

				$tagSelectorSubmit.addClass("disabled");
			}

		}
	});


	$tagSelectorSubmit.on("click", function (e) {

		e.preventDefault();

		var value = $tagSelectorInput.val();

		addTag(value);
	});


	function addTag(name) {

		var $tagTemplate = $tagSelectorTemplate.find(".checkbox").clone();

		var tagIndex = $tagSelectorBody.children().length;

		var tagNameBase = "Tags[" + tagIndex + "].";

		$tagTemplate.attr("data-tag-id", tagIndex);

		$tagTemplate.find(".js-tag-id").attr("name", tagNameBase + "Id").val("");

		$tagTemplate.find(".js-tag-name").attr("name", tagNameBase + "Name").val(name);

		$tagTemplate.find(".js-tag-is-checked").attr("name", tagNameBase + "IsChecked").attr("checked", true).val(true);

		$tagTemplate.find(".js-tag-name-text").text(name);

		$tagSelectorBody.append($tagTemplate);

		console.log("tag selector body ", $tagSelectorBody, " tag template ", $tagTemplate);

		$tagSelectorInput.val("");

		$tagSelectorSubmit.addClass("disabled");
	}


	$tagSelectorBody.on("click", "label", function (e) {

		var $this = $(this);

		var $checkbox = $this.children(".js-tag-is-checked");

		var isChecked = $checkbox.is(":checked");

		$checkbox.attr("value", isChecked);

		// console.log("is checked ", $checkbox.is(":checked"));
	});

})(jQuery);
