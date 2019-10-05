$(document).on('click', '#addPostButton', function () {
    var postText = $('#postTextArea').val();
    $('#postTextArea').val("");
    postText = encodeURIComponent(postText);
    if (postText != "")
        $("#postsArea").load("createPost/" + postText);
});

$(document).on('click', ".delete-post-button", function () {
    $("#postsArea").load("removePost/" + event.target.getAttribute('postId'));
});