const showScrollButtonHeight: number = 968;
const messagesListId: string = 'messages-list';
const scrollProp: string = 'scroll';

function addPaginationEvent(dotNetObject) {
    let listElm = document.getElementById(messagesListId);
    listElm.addEventListener(scrollProp, function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync", listElm.scrollHeight);
        }
    });
}

function addScrollButtonEvent() {
    let listElm = document.getElementById(messagesListId);
    let buttonElm = document.getElementById('scroll-button');
    listElm.addEventListener(scrollProp, function () {
        buttonElm.style.display = listElm.scrollHeight - listElm.scrollTop >= showScrollButtonHeight ? "block" : "none";
    });
}

function scrollToHeight(height: number) {
    let listElm = document.getElementById(messagesListId);
    let scrollValue = height > 0 ? listElm.scrollHeight - height : listElm.scrollHeight;

    listElm.scrollTop = scrollValue;
}