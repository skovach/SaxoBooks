var controller = "Home";
var action = "GetBooks";
var BlockNumber = 1;
var HasBooks = true;
var inProgress = false;
var dataRequested = false;
    
$(window).scroll(function () {
    if ($(window).scrollTop() == ($(document).height() - $(window).height()) && dataRequested) {
        getBooks(true);
    }
});

var getBooks = function (isPageScroll) {
    if (!isPageScroll) {
        HasBooks = true;
        BlockNumber = 1;
    }
    if (HasBooks && !inProgress) {
        inProgress = true;
        $("#loading-img").show();
        var data = {
            BlockNumber: BlockNumber,
            IsbnNumbers: $("#isbns").val()
        };
        var url = "/" + controller + "/" + action;
        $.post(url, data,
            function (data) {
                HasBooks = data.HasBooks;
                BlockNumber = BlockNumber + 1;
                if (!isPageScroll) {
                    dataRequested = true;
                    $("#book-results").empty();
                    BlockNumber = 2;
                }
                $("#book-results").append(data.HtmlString);
                $("#loading-img").hide();
                inProgress = false;
            });
    }
}