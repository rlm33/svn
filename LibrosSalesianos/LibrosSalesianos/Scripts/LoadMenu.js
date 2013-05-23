
$(document).ready(function () {
    // Create a jqxMenu
    $("#jqxMenu").jqxMenu({ width:'62%', height:30, theme: 'cole'});

    var centerItems = function () {
        var firstItem = $($("#jqxMenu ul:first").children()[0]);
        firstItem.css('margin-left', 0);
        var width = 0;
        var borderOffset = 2;
        $.each($("#jqxMenu ul:first").children(), function () {
            width += $(this).outerWidth(true) + borderOffset;
        });
        var menuWidth = $("#jqxMenu").outerWidth();
        firstItem.css('margin-left', (menuWidth / 2) - (width / 2));
    }
    centerItems();
    $(window).resize(function () {
        centerItems();
    });
});
