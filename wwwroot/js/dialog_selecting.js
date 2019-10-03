$(".user-dialog").click(function () {
    $(".main-conversation-box").load("dialog/" + event.target.getAttribute('userId'));
});