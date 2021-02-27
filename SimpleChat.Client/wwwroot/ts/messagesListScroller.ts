const showScrollButtonHeight = 968;

function addPaginationEvent(dotNetObject) {
    let listElm = document.getElementById('messages-list');
    listElm.addEventListener('scroll', function() {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}

function addScrollButtonEvent(dotNetObject) {
    let listElm = document.getElementById('messages-list');
    let buttonElm = document.getElementById('scroll-button');
    listElm.addEventListener('scroll', function() {
        if (listElm.scrollHeight - listElm.scrollTop >= showScrollButtonHeight) {
            buttonElm.style.display = "block"
        }
        else{
            buttonElm.style.display = "none"
        }
    });
}

function scrollToBottom() {
    let listElm = document.getElementById('messages-list');
    listElm.scrollTop = listElm.scrollHeight;
}