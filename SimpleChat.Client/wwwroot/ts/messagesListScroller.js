var showScrollButtonHeight = 968;
function addPaginationEvent(dotNetObject) {
    var listElm = document.getElementById('messages-list');
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}
function addScrollButtonEvent(dotNetObject) {
    var listElm = document.getElementById('messages-list');
    var buttonElm = document.getElementById('scroll-button');
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
    var listElm = document.getElementById('messages-list');
    listElm.scrollTop = listElm.scrollHeight;
}
//# sourceMappingURL=messagesListScroller.js.map