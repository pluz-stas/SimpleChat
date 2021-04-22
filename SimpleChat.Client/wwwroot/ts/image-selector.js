var getPicsCountPerPage = function () {
    var containerId = 'pot-avatar-container';
    var ImgClass = 'profile-pot-pic';
    var imgEl = document.getElementsByClassName(ImgClass)[0];
    var container = document.getElementById(containerId);
    var style = window.getComputedStyle(imgEl);
    var marginX = parseFloat(style.marginLeft) + parseFloat(style.marginRight);
    var marginY = parseFloat(style.marginTop) + parseFloat(style.marginBottom);
    var elWidth = imgEl.offsetWidth + marginX;
    var elHeight = imgEl.offsetHeight + marginY;
    var containerXVolume = ~~(container.offsetWidth / elWidth);
    var containerYVolume = ~~(container.offsetHeight / elHeight);
    return containerXVolume * containerYVolume;
};
//# sourceMappingURL=image-selector.js.map