var showScrollButtonHeight = 968;
var messagesListId = 'messages-list';
var scrollProp = 'scroll';
function addPaginationEvent(dotNetObject) {
    var listElm = document.getElementById(messagesListId);
    listElm.addEventListener(scrollProp, function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync", listElm.scrollHeight);
        }
    });
}
function addScrollButtonEvent() {
    var listElm = document.getElementById(messagesListId);
    var buttonElm = document.getElementById('scroll-button');
    listElm.addEventListener(scrollProp, function () {
        buttonElm.style.display = listElm.scrollHeight - listElm.scrollTop >= showScrollButtonHeight ? "block" : "none";
    });
}
function scrollToHeight(height) {
    var listElm = document.getElementById(messagesListId);
    var scrollValue = height > 0 ? listElm.scrollHeight - height : listElm.scrollHeight;
    listElm.scrollTop = scrollValue;
}
//# sourceMappingURL=messagesListScroller.js.map