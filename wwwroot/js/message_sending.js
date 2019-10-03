$('#sendMessageButton').click(function () {
    var messageText = $('#messageText').val();
    var userToId = event.target.getAttribute('userToId')
    $('#messageText').val("");
    messageText = encodeURIComponent(messageText);
    if (messageText != "")
        $(".main-conversation-box").load("sendMessage/" + userToId +"/" + messageText);
});