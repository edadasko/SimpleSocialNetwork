$(document).on('click', ".delete-post-button", function () {
    $("#postsArea").load("removePost/" + event.target.getAttribute('postId'));
});
