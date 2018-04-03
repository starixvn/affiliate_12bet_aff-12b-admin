var Aff12BetTesting = (function ($) {
    function render() {
        var rootAction = $("#rootAction").val();
        var format = $('input[name=Format]:checked').val() || "cshtml";
        var language = $('input[name=Language]:checked').val() || "en-US";
        var functionName = $('input[name=Function]:checked').val();
        var url = rootAction + "Test/Email?language=" + language + "&function=" + functionName + "&format=" + format;
        console.log(url);

        $.ajax({
            type: "GET",
            dataType: "html",
            cache: false,
            url: url,
            success: function (response) {
                $("#resultContent").html(response);
            }
        });
    }

    return {
        initialize: function () {
            Aff12BetTesting.initEvent();
        },

        initEvent: function () {
            $("#btnSubmit").on("click", function () {
                $("#resultContent").html("");
                render();
            });
        }
    };
})(jQuery);

Aff12BetTesting.initialize();