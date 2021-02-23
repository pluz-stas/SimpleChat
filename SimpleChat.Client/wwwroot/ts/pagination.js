function addPaginationEvent(dotNetObject) {
    var listElm = document.querySelector('#messagesList');
    listElm.scrollTop = listElm.clientHeight;
    console.log(typeof (dotNetObject));
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
        }
    });
}
//# sourceMappingURL=pagination.js.map