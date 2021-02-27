const showScrollButtonHeight = 968;

function addPaginationEvent(dotNetObject) {
    let listElm = document.getElementById('messagesList');
    listElm.addEventListener('scroll', function() {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}

function addScrollButtonEvent(dotNetObject) {
    let listElm = document.getElementById('messagesList');
    let buttonElm = document.getElementById('scrollButton');
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
    let listElm = document.getElementById('messagesList');
    listElm.scrollTop = listElm.scrollHeight;
}