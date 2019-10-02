$('#addPostButton').click(function () {
    var postText = $('#postTextArea').val();
    $('#postTextArea').val("");
    postText = encodeURIComponent(postText);
    if (postText != "")
        $("#postsArea").load("createPost/" + postText);
});