const _rootUrl = document.getElementById('RootUrl') ? document.getElementById('RootUrl').value : '/';

export const resolveClientUrl = (relativeUrl) => {
    return _rootUrl + relativeUrl;
}

export const getVirtualUrl = () => {
    if (!window.virtualUrl)
        return '/';
    return window.virtualUrl;
}

export const formatUnit = (value) => {
    return value < 10 ? '0' + value : value;
}

const formatMilisecond = (value) => {
    const greaterThanTenFormat = value < 100 ? ('0' + value) : value;

    return value < 10 ? ('00' + value) : greaterThanTenFormat;
}

export const formatDateTime = (dateTimeValue, format) => {
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
    var miliseconds = dateTime.getMilliseconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';

    var shortedHours = hours % 12;
    shortedHours = shortedHours === 0 ? 12 : shortedHours; // the hour '0' should be '12'

    var date = dateTime.getDate();
    var month = dateTime.getMonth() + 1;

    var result = format.replace("MM", formatUnit(month))
        .replace("dd", formatUnit(date))
        .replace("yyyy", dateTime.getFullYear());

    return result.replace("hh", formatUnit(shortedHours))
        .replace("mm", formatUnit(minutes))
        .replace("ss", formatUnit(seconds))
        .replace("tt", ampm)
        .replace("HH", formatUnit(dateTime.getHours()))
        .replace("SSS", formatMilisecond(miliseconds));
}

export const parseDateTime = (value) => {
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

export const convertToTimeOffset = (date, offset) => {
    let localDate = new Date(date);
    let localTime = localDate.getTime();
    let localOffset = localDate.getTimezoneOffset() * 60000;
    let utcTime = localTime + localOffset;
    let convertTime = utcTime + (3600000 * offset);
    return new Date(convertTime);
}

export function getDateRange(selectedFromDate, toDateString) {
    const selectedFromDateString = selectedFromDate.format("MM/DD/YYYY");
    const fromDate = new Date(selectedFromDateString);
    const toDate = new Date(toDateString);

    const isOutsideRange = fromDate.getTime() > toDate.getTime();
    const newToDate = isOutsideRange ? selectedFromDateString : toDateString;

    return {
        fromDate: selectedFromDateString,
        toDate: newToDate
    };
}

export function handleAfterChangeFromDate(selectedFromDate, toDateString, maxRangeDate) {
    const selectedFromDateString = selectedFromDate.format("MM/DD/YYYY");
    const fromDate = new Date(selectedFromDateString);
    const toDate = new Date(toDateString);

    const isOutsideRange = fromDate.getTime() > toDate.getTime();
    const newToDate = isOutsideRange ? selectedFromDateString : getToDate(toDate, selectedFromDateString, toDateString, maxRangeDate);

    return {
        fromDate: selectedFromDateString,
        toDate: newToDate
    };
}

function getToDate(toDate, selectedFromDateString, toDateString, maxRangeDate) {
    if (!maxRangeDate) {
        return toDateString;
    }

    const nextSixMonth = new Date(selectedFromDateString);
    nextSixMonth.setMonth(nextSixMonth.getMonth() + maxRangeDate);

    return toDate > nextSixMonth ? `${formatUnit(nextSixMonth.getMonth() + 1)}/${formatUnit(nextSixMonth.getDate())}/${nextSixMonth.getFullYear()}` : toDateString;
}

export function handleAfterChangeToDate(selectedToDate, fromDateString, maxRangeDate) {
    const selectedToDateString = selectedToDate.format("MM/DD/YYYY");
    const fromDate = new Date(fromDateString);
    const toDate = new Date(selectedToDateString);

    const isOutsideRange = fromDate.getTime() > toDate.getTime();
    const newFromDate = isOutsideRange ? selectedToDateString : getFromDate(fromDate, selectedToDateString, fromDateString, maxRangeDate);

    return {
        fromDate: newFromDate,
        toDate: selectedToDateString
    };
}

function getFromDate(fromDate, selectedToDateString, fromDateString, maxRangeDate) {
    if (!maxRangeDate) {
        return fromDateString;
    }

    const sixMonthAgo = new Date(selectedToDateString);
    sixMonthAgo.setMonth(sixMonthAgo.getMonth() - maxRangeDate);

    return sixMonthAgo < fromDate ? fromDateString : `${formatUnit(sixMonthAgo.getMonth() + 1)}/${formatUnit(sixMonthAgo.getDate())}/${sixMonthAgo.getFullYear()}`;
}

export const isValidNumber = (inputValue) => {
    const regex = /^\d*$/;
    return regex.test(inputValue);
}

export const validateUserName = (userName) => {
    if (!userName) {
        return {
            IsValid: false,
            Message: 'User Name is required!'
        };
    }

    const regexCheckSpecialChar = new RegExp("^[a-zA-Z0-9]{1,20}$");
    if (!regexCheckSpecialChar.test(userName)) {
        return {
            IsValid: false,
            Message: 'User Name can not be contained the special characters. Please try again!'
        };
    }

    return {
        IsValid: true,
        Message: ''
    }
}

const regExp = {
    CheckPassword: "^(?=.*[0-9]+.*[0-9]+)(?=.*[a-zA-Z].*[a-zA-Z]).{6,}$"
}

export const validateNewPassword = (newPassword) => {
    const result = {
        IsValid: false,
        Message: "New Password is required!"
    }

    if (newPassword) {
        const regexCheckPassword = new RegExp(regExp.CheckPassword);
        const isValid = regexCheckPassword.test(newPassword);

        result.IsValid = isValid;
        result.Message = isValid ? "" : 'Password must have at least 6 characters, including at least 2 letters and at least 2 digits.';
    }

    return result;
}

export const convertArrayToObjectProps = (array, key, value) => {
    if(!Array.isArray(array))
        return {};
    let result = {};
    array.forEach((item) =>{
        let keyValue = item[key];
        result[keyValue] = item[value];
    })
    return result;
}

export const removeUndifineItem = (obj) => {
    return JSON.parse(JSON.stringify(obj));
}

export const validateFileExtension = (fileName, fileExtensions, message) => {
    const result = {
        isOK: false,
        message: message || `Sorry! We just support these file types : ${fileExtensions.map(item => "'." + item + "'").join(', ')}`,
    }

    if (!!fileName) {
        const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).toLowerCase();

        const isOK = fileExtensions.includes(fileExtension);

        if (isOK) {
            result.isOK = true;
            result.message = '';
        }
    }

    return result;
}