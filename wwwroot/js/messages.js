$(document).on('click', '#sendMessageButton', function () {
    var messageText = $('#messageText').val();
    var userToId = event.target.getAttribute('userToId')
    $('#messageText').val("");
    messageText = encodeURIComponent(messageText);
    if (messageText != "") {
        $(".main-conversation-box").load("sendMessage/" + userToId + "/" + messageText, function () {
            $(".dialogs-list").load("dialogsList/", function () {
                $(".user-dialog:first").addClass('selected');
            })
        });
    }
});

$(document).on('click', ".user-dialog", function () {
    $('.user-dialog').removeClass('selected');
    $(this).addClass('selected');
    $(".main-conversation-box").load("dialog/" + event.target.getAttribute('userId'));
});