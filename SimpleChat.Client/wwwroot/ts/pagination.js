function addPaginationEvent(dotNetObject) {
    var listElm = document.getElementById('messagesList');
    listElm.scrollTop = listElm.clientHeight;
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}
//# sourceMappingURL=pagination.js.map