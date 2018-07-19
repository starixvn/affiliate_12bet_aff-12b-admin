(function ($) {
    $.validator.setDefaults({
        highlight: function (element) {
            var $container = $(element).closest(".form-group");
            $container.addClass("has-error");
            $container.find(".help-block").removeClass("hide");
        },
        unhighlight: function (element) {
            var $container = $(element).closest(".form-group");
            $container.removeClass("has-error");
            $container.find(".help-block").addClass("hide");
        },
        errorElement: 'span',
        errorClass: 'help-block hide'
    });

    $.validator.unobtrusive.adapters.add("requiredbychecked", ["dependentprop", "checked"],
    function (options) {
        options.rules["requiredbychecked"] = {
            dependentprop: options.params.dependentprop,
            checked: options.params.checked === "True"
        };
        options.messages["requiredbychecked"] = options.message;
    });

    $.validator.addMethod("requiredbychecked",
        function (value, element, params) {
            var isValid = true;

            var isChecked = $("[name='" + params.dependentprop + "']:checked").val().trim() === "True";
            if (isChecked === params.checked && $.trim(value).length === 0) {
                isValid = false;
            }

            return isValid;
        }
    );

    $.validator.unobtrusive.adapters.add("requiredifnullorempty", ["dependentprop"],
    function (options) {
        options.rules["requiredifnullorempty"] = {
            dependentprop: options.params.dependentprop
        };
        options.messages["requiredifnullorempty"] = options.message;
    });

    $.validator.addMethod("requiredifnullorempty",
        function (value, element, params) {
            var isValid = true;

            var dependenPropList = params.dependentprop.split(',').filter(p => !!p);

            for (var i = 0; i < dependenPropList.length; i++) {
                var propName = dependenPropList[i];
                var dependentValue = $("[name='" + propName + "']").val().trim();
                console.log(propName, dependentValue, value);
                if (!$.trim(value) && !$.trim(dependentValue)) {
                    return false;
                }
            }

            return isValid;
        }
    );
})(jQuery)