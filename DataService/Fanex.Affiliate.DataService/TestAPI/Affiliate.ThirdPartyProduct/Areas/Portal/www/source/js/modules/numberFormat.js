export function formatDecimal(value) {
    return format(value, 2)
}

export function format(value, digits = 0) {
    if (typeof value !== "number" || isNaN(value)) {
        return "";
    }

    const roundedValue = round(value, digits);

    return roundedValue.toFixed(digits).replace(/(\d)(?=(\d{3})+(\.|$))/g, "$1,");
}

export function formatDecimalWithCurrencySymbol(value, currencySymbol = "$") {
    return value < 0 ? `-${currencySymbol}${formatDecimal(Math.abs(value))}` : `${currencySymbol}${formatDecimal(value)}`;
}

export function round(value, digits) {
    const sign = value < 0 ? -1 : 1;
    const absoluteValue = Math.abs(value);
    const rounded = Number(Math.round(absoluteValue + 'e' + digits) + 'e-' + digits);

    return sign * rounded;
}