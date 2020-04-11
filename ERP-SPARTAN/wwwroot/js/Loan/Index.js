

const GetAllClientOptions = async () => {
    await fetch("/ClientUser/GetAllOptionsClients").then(response => response.text()).then((result) => { console.log(result); });
};

//$(document).ready(function () {
//    $('#fechaPrestamo').val(new Date().toDateInputValue());
//});​
Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}
Date.prototype.addMonth = function (CantMonth) {
    var date = new Date(this.valueOf());
    date.setMonth(date.getMonth() + CantMonth)
    return date;
}

Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});
    $('#fechaPrestamo').val(new Date().toDateInputValue());
