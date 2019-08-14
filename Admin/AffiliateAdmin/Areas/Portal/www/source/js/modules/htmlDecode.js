const span = document.createElement("span");

export const htmlDecode = (text) => {
    span.innerHTML = text;
    return span.textContent;
}