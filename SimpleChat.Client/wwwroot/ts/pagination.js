function addPaginationEvent(dotnetHelper) {
    var listElm = document.querySelector('#messagesList');
    listElm.scrollTop = listElm.clientHeight;
    listElm.addEventListener('scroll', function () {
        if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
            dotnetHelper.invokeMethodAsync('SayHello');
        }
    });
}
//# sourceMappingURL=pagination.js.map