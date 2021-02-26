
function addPaginationEvent(dotNetObject) {
    let listElm = document.getElementById('messagesList');
    listElm.scrollTop = listElm.clientHeight;
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}
