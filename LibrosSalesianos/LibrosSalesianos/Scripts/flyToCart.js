function animate(id_img, id_dest) {
    var imgX = $("#" + id_img).offset().left;
    var imgY = $("#" + id_img).offset().top;

    var iraX = $("#" + id_dest).offset().left;
    var iraY = $("#" + id_dest).offset().top;

    var gotoX = iraX - imgX;
    var gotoY = iraY - imgY;

    var newImageWidth = $("#" + id_img).width() / 3;
    var newImageHeight = $("#" + id_img).height() / 3;

    $("#" + id_img).clone().appendTo('#test')
        .stop(true)
        .css({
            'position': 'absolute',
            'left': imgX,
            'top': imgY
        })
        .animate({ opacity: 0.4 }, 100)
        .animate({ opacity: 0.1, marginLeft: gotoX, marginTop: gotoY, width: newImageWidth, height: newImageHeight }, 1200, function () {
            $(this).remove();
        });
};