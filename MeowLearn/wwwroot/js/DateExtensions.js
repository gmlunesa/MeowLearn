function computeMonths(date, numMonths) {
    var month = date.getMonth();
    var computedMonth = new Date(date).setMonth(month + numMonths);
    return new Date(computedMonth);
}