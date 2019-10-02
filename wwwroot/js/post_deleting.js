$(".delete-post-button").click(function () {
    $("#postsArea").load("removePost/" + event.target.getAttribute('postId'));
});