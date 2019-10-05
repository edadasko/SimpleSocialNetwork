$(document).on('click', '#sendMessageButton', function () {
    var messageText = $('#messageText').val();
    var userToId = event.target.getAttribute('userToId')
    $('#messageText').val("");
    messageText = encodeURIComponent(messageText);
    if (messageText != "")
        $(".main-conversation-box").load("sendMessage/" + userToId +"/" + messageText);
});

$(document).on('click', ".user-dialog", function () {
    $(".main-conversation-box").load("dialog/" + event.target.getAttribute('userId'));
});