$(function () {
    $("table").stickyTableHeaders();
});

	
//function UpdateTableHeaders() {
//    $(".persist-area").each(function () {

//        var el = $(this),
//            offset = el.offset(),
//            scrollTop = $(window).scrollTop(),
//             floatingHeader = $(".floatingHeader", this)


//        if ((scrollTop > offset.top) && (scrollTop < offset.top + el.height())) {
//            floatingHeader.css({
//                "visibility": "visible"
//            });
//        } else {
//            floatingHeader.css({
//                //"visibility": "hidden"
//                "visibility": "visible"
//            });
//        };

      
//    });
//}

//// DOM Ready      
//$(function () {

//    var clonedHeaderRow;

//    $(".persist-area").each(function () {
//        clonedHeaderRow = $(".persist-header", this);
//        clonedHeaderRow
//          .after(clonedHeaderRow.clone())
//          .css("width", clonedHeaderRow.width())
//          .css("height", clonedHeaderRow.height())
//          .addClass("floatingHeader");

//    });

//    $(window)
//     .scroll(UpdateTableHeaders)
//     .trigger("scroll");

   

//});