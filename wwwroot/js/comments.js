$(document).on('click', '.add-comment-button', function () {
    var postId = event.target.getAttribute('postId');
    var commentText = $('[commentForPost=' + postId + ']').val();
    $('[commentForPost=' + postId + ']').val("");
    var commentsArea = $('#comments' + postId);
    commentText = encodeURIComponent(commentText);
    if (commentText != "")
        $(commentsArea).load("/addComment/" + commentText + "/" + postId);
});

$(document).on('click', ".delete-comment-button", function () {
    var commentId = event.target.getAttribute('commentId');
    var postId = event.target.getAttribute('postId');
    var commentsArea = $('#comments' + postId);
    $(commentsArea).load("/removeComment/" + commentId);
});