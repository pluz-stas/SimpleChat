
function addPaginationEvent(dotnetHelper) {
    let listElm = document.querySelector('#messagesList');

    listElm.scrollTop = listElm.clientHeight
    listElm.addEventListener('scroll', function() {
    if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
        dotnetHelper.invokeMethodAsync('SayHello');
    }
});
}
