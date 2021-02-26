
function addPaginationEvent(dotNetObject) {
    let listElm = document.querySelector('#messagesList');
    listElm.scrollTop = listElm.clientHeight;
    console.log(typeof(dotNetObject));
    listElm.addEventListener('scroll', function() {
    if (listElm.scrollHeight - listElm.scrollTop >= listElm.scrollHeight) {
        dotNetObject.invokeMethodAsync("UpdateMessagesHistoryAsync");
    }
});
}
