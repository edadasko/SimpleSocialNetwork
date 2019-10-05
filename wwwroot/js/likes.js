$(document).on('click', ".post-like-button", function () {
    var postId = event.target.getAttribute('postId');
    var post = $('#post' + postId);
    if ($(this).hasClass("liked")) {
        $(post).load("/removeLike/" + postId);
        $(this).removeClass("liked");
        $(this).addClass("not-liked");
    }
    else {
        $(post).load("/addLike/" + postId);
        $(this).removeClass("not-liked");
        $(this).addClass("liked");
    }
});