var showScrollButtonHeight = 968;
function addPaginationEvent(dotNetObject) {
    var listElm = document.getElementById('messagesList');
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}
function addScrollButtonEvent(dotNetObject) {
    var listElm = document.getElementById('messagesList');
    var buttonElm = document.getElementById('scrollButton');
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= showScrollButtonHeight) {
            buttonElm.style.display = "block";
        }
        else {
            buttonElm.style.display = "none";
        }
    });
}
function scrollToBottom() {
    var listElm = document.getElementById('messagesList');
    listElm.scrollTop = listElm.scrollHeight;
}
//# sourceMappingURL=messagesListScroller.js.map