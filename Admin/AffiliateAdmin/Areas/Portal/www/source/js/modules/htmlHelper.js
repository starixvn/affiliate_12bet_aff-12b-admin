export const htmlDecode = (text) => {
    const span = document.createElement("span");
    span.innerHTML = text;

    return span.textContent;
}

export const trimText = (text) => {
    if (typeof(text) === "undefined" || text === null)
        return "";
    else 
        return text;
} 