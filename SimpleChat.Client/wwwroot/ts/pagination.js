function addPaginationEvent(event) {
    var listElm = document.querySelector('#messagesList');
    listElm.scrollTop = listElm.clientHeight;
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            event.invokeMethodAsync("GetHistoryMessages");
        }
    });
}
//# sourceMappingURL=pagination.js.map