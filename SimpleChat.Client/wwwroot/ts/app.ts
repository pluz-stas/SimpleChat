const themeAttribute: string = "data-theme";

const switchTheme = (theme: string): void => {
    document.documentElement.setAttribute(themeAttribute, theme);
};

function test(arg): void {
    console.log(arg);
}