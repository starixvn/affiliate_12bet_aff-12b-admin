const _rootUrl = document.getElementById('RootUrl').value;

const setItem = (index, dataList, newValue) => {
    return dataList.map((item, i) => {
        if (i === index) {
            return Object.assign({}, dataList[index], newValue);
        }

        return Object.assign({}, item);;
    });
}

const removeItem = (index, dataList) => {
    return dataList.filter((item, i) => {
        return i !== index;
    })
}

const deepClone = (data) => {
    return JSON.parse(JSON.stringify(data));
}

const resolveClientUrl = (relativeUrl) => {
    return _rootUrl + relativeUrl;
}

function formatUnit(value) {
    return value < 10 ? '0' + value : value;
}

const formatDateTime = (dateTimeValue, format) => {
    if (!dateTimeValue) {
        return "";
    }

    if (!format) {
        format = "MM/dd/yyyy";
    }

    var dateTime = typeof dateTimeValue === 'string' ? new Date(dateTimeValue) : dateTimeValue;
    var hours = dateTime.getHours();
    var minutes = dateTime.getMinutes();
    var seconds = dateTime.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';

    var shortedHours = hours % 12;
    shortedHours = shortedHours === 0 ? 12 : shortedHours; // the hour '0' should be '12'

    var date = dateTime.getDate();
    var month = dateTime.getMonth() + 1;

    var result = format.replace("MM", formatUnit(month))
        .replace("dd", formatUnit(date))
        .replace("yyyy", dateTime.getFullYear())
        .replace("hh", formatUnit(shortedHours))
        .replace("mm", formatUnit(minutes))
        .replace("ss", formatUnit(seconds))
        .replace("tt", ampm)
        .replace("HH", dateTime.getHours());

    return result;
}

const parseDateTime = (value) => {
    var reISO = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*))(?:Z|(\+|-)([\d|:]*))?$/;
    var reMsAjax = /^\/Date\((d|-|.*)\)[\/|\\]$/;

    if (typeof value === 'string') {
        var a = reISO.exec(value);
        if (a)
            return new Date(value);
        a = reMsAjax.exec(value);
        if (a) {
            var b = a[1].split(/[-+,.]/);
            return new Date(b[0] ? +b[0] : 0 - +b[1]);
        }
    }

    return value;
}

const updateObjectInArray = (array, indexItem, updatedItem) => {
    return array.map((item, index) => {
        if (index !== indexItem) {
            return item;
        }
        return {
            ...item,
            ...updatedItem
        };
    });
}

export {
    setItem,
    removeItem,
    deepClone,
    resolveClientUrl,
    formatDateTime,
    parseDateTime,
    updateObjectInArray
}