export function getNewFilterDate(selectedFromDate, toDateString) {
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