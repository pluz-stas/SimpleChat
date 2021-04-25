const getPicsCountPerPage = (): number => {
    const containerId: string = 'pot-avatar-container';
    const ImgClass: string = 'profile-pot-pic';

    let imgEl = document.getElementsByClassName(ImgClass)[0] as HTMLElement;
    let container = document.getElementById(containerId);

    let style = window.getComputedStyle(imgEl);
    let marginX = parseFloat(style.marginLeft) + parseFloat(style.marginRight);
    let marginY = parseFloat(style.marginTop) + parseFloat(style.marginBottom);
    let elWidth = imgEl.offsetWidth + marginX;
    let elHeight = imgEl.offsetHeight + marginY;

    let containerXVolume = ~~(container.offsetWidth / elWidth);
    let containerYVolume = ~~(container.offsetHeight / elHeight);

    return containerXVolume * containerYVolume;
}