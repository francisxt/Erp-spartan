﻿const GetAllClientOptions = async () => {
    await fetch("/ClientUser/GetAllOptionsClients").then(response => response.text()).then((result) => { console.log(result); });
};


Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
};

Date.prototype.addMonth = function (CantMonth) {
    var date = new Date(this.valueOf());
    date.setMonth(date.getMonth() + CantMonth);
    return date;
};

Date.prototype.toDateInputValue = function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
};

$('#fechaPrestamo').val(new Date().toDateInputValue());


/*START CALCULATE DEBS*/
function getValues() {

    //button click gets values from inputs
    const balance = parseFloat(document.getElementById("amount").value);
    const interestRate =
        parseFloat(document.getElementById("interest").value / 100.0);
    const cuotas =
        document.getElementById("cuotas").value;
    const typeOfTasa = parseInt(document.getElementById("typeOfTasa").value);

    const amortitationTypevalue = parseInt(document.getElementById("amortitationType").value);

    const paymentModality = parseInt(document.getElementById("paymentModality").value);

    //set the div string
    var div = document.getElementById("Result");

    //in case of a re-calc, clear out the div!
    div.innerHTML = "";

    //validate inputs - display error if invalid, otherwise, display table
    var balVal = validateInputs(balance);
    var intrVal = validateInputs(interestRate);

    if (balVal && intrVal) {

        //Returns div string if inputs are valid

        let arrayAmortizacion = calAmort(balance, interestRate, paymentModality, cuotas, amortitationTypevalue, typeOfTasa);
        MakeTable(arrayAmortizacion, 'Result');
        // console.log(arrayAmortizacion);
    }
    else {
        //returns error if inputs are invalid
        div.innerHTML += "Please Check your inputs and retry - invalid values.";
    }
}

function calAmort(balance, interestRate, paymentModality, cuotas, amortitationTypevalue, typeOfTasa) {
    let arrayobjectAmortizacion = [];
    switch (amortitationTypevalue) {
        case amortitationType.CUOTAFIJA:
            //Execute logic CUOTAFIJA
            arrayobjectAmortizacion = calAmortCuotaFija(balance, interestRate, cuotas, paymentModality, typeOfTasa);
            break;
        case amortitationType.INTERESFIJO:
            //Execute logic INTERESFIJO
            arrayobjectAmortizacion = calAmortInterestFixed(balance, interestRate, cuotas, paymentModality, typeOfTasa);
            break;
        case amortitationType.CAPITALFINAL:
            //Execute logic CAPITALFINAL
            arrayobjectAmortizacion = calAmortCapitalAlFinal(balance, interestRate, cuotas, paymentModality, typeOfTasa);
            break;
    }
    return arrayobjectAmortizacion;
}

function calAmortCuotaFija(balance, interestRate, cuotas, paymentModality, typeOfTasa) {
    var arrayAmortizaion = [];
    var monthlyRate = interestRate;

    if (typeOfTasa === enumTypeOfTasa.ANUAL) {
        monthlyRate = interestRate / 12;
    }
    var dateNextPayment = new Date();

    var payment = balance * (monthlyRate / (1 - Math.pow(
        1 + monthlyRate, -cuotas)));
    /**
     * Loop that calculates the monthly Loan amortization amounts then adds 
     * them to the return string 
     */
    for (var count = 0; count < cuotas; ++count) {
        let Amortizacion = { Cuota: "", Date: "", Balance: 0, Interest: 0, Payment: 0, Amortizacion: 0, Endbalance: 0 };
        //in-loop interest amount holder
        var interest = 0;

        //in-loop monthly principal amount holder
        var monthlyPrincipal = 0;

        //start a new table row on each loop iteration

        //display the month number in col 1 using the loop count variable
        Amortizacion.Cuota = count + 1;
        //Amortizacion.Date = hoy.addMonth(Amortizacion.Period).toJSON()
        dateNextPayment = addDateToAmortizacion(dateNextPayment, paymentModality);
        Amortizacion.Date = FormatDate(dateNextPayment);

        //code for displaying in loop balance
        Amortizacion.Balance = FormatMoney(balance);

        //calc the in-loop interest amount and display
        interest = balance * monthlyRate;
        Amortizacion.Interest = FormatMoney(interest);

        //calc the in-loop monthly principal and display
        monthlyPrincipal = payment - interest;
        Amortizacion.Amortizacion = FormatMoney(monthlyPrincipal);

        Amortizacion.Endbalance = FormatMoney(balance - monthlyPrincipal);
        Amortizacion.Payment = FormatMoney(payment);


        //update the balance for each loop iteration
        balance = balance - monthlyPrincipal;
        arrayAmortizaion.push(Amortizacion);
    }
    //Final piece added to return string before returning it - closes the table
    //returns the concatenated string to the page
    console.log(arrayAmortizaion);
    return arrayAmortizaion;
}
function calAmortInterestFixed(balance, interestRate, cuotas, paymentModality, typeOfTasa) {
    var arrayAmortizaion = [];
    var monthlyRate = interestRate;

    if (typeOfTasa === enumTypeOfTasa.ANUAL) {
        monthlyRate = interestRate / 12;
    }
    var dateNextPayment = new Date();
    var balanceFixed = balance;
    //var payment = balance * (monthlyRate / (1 - Math.pow(
    //    1 + monthlyRate, -cuotas)));
    var payment = 0;
    /**
     * Loop that calculates the monthly Loan amortization amounts then adds 
     * them to the return string 
     */
    for (var count = 0; count < cuotas; ++count) {
        let Amortizacion = { Cuota: "", Date: "", Balance: 0, Interest: 0, Payment: 0, Amortizacion: 0, Endbalance: 0 };
        //in-loop interest amount holder
        var interest = 0;

        //in-loop monthly principal amount holder
        var monthlyPrincipal = 0;

        //start a new table row on each loop iteration

        //display the month number in col 1 using the loop count variable
        Amortizacion.Cuota = count + 1;
        //Amortizacion.Date = hoy.addMonth(Amortizacion.Period).toJSON()
        dateNextPayment = addDateToAmortizacion(dateNextPayment, paymentModality);
        Amortizacion.Date = FormatDate(dateNextPayment);

        //code for displaying in loop balance
        Amortizacion.Balance = FormatMoney(balance);

        //calc the in-loop interest amount and display
        interest = balanceFixed * monthlyRate;
        Amortizacion.Interest = FormatMoney(interest);

        //calc the in-loop monthly principal and display
        monthlyPrincipal = balanceFixed / cuotas;


        Amortizacion.Amortizacion = FormatMoney(monthlyPrincipal);

        payment = monthlyPrincipal + interest;

        Amortizacion.Endbalance = FormatMoney(balance - monthlyPrincipal);

        Amortizacion.Payment = FormatMoney(payment);


        //update the balance for each loop iteration
        balance = balance - monthlyPrincipal;
        arrayAmortizaion.push(Amortizacion);
    }
    //Final piece added to return string before returning it - closes the table
    //returns the concatenated string to the page
    return arrayAmortizaion;
}
function calAmortCapitalAlFinal(balance, interestRate, cuotas, paymentModality, typeOfTasa) {
    var arrayAmortizaion = [];
    var monthlyRate = interestRate;

    if (typeOfTasa === enumTypeOfTasa.ANUAL) {
        monthlyRate = interestRate / 12;
    }
    var dateNextPayment = new Date();

    //var payment = balance * (monthlyRate / (1 - Math.pow(
    //    1 + monthlyRate, -cuotas)));
    var payment = 0;
    /**
     * Loop that calculates the monthly Loan amortization amounts then adds 
     * them to the return string 
     */
    for (var count = 0; count < cuotas; ++count) {
        let Amortizacion = { Cuota: "", Date: "", Balance: 0, Interest: 0, Payment: 0, Amortizacion: 0, Endbalance: 0 };
        //in-loop interest amount holder
        var interest = 0;

        //in-loop monthly principal amount holder
        var monthlyPrincipal = 0;

        //start a new table row on each loop iteration

        //display the month number in col 1 using the loop count variable
        Amortizacion.Cuota = count + 1;
        //Amortizacion.Date = hoy.addMonth(Amortizacion.Period).toJSON()
        dateNextPayment = addDateToAmortizacion(dateNextPayment, paymentModality);
        Amortizacion.Date = FormatDate(dateNextPayment);
        //calc the in-loop interest amount and display
        interest = balance * monthlyRate;
        payment = interest;
        Amortizacion.Interest = FormatMoney(interest);
        //code for displaying in loop balance
        Amortizacion.Balance = FormatMoney(balance);
        if (count === cuotas - 1) {
            monthlyPrincipal = balance;
            payment = balance + interest;
        }
        //calc the in-loop monthly principal and display
        Amortizacion.Amortizacion = FormatMoney(monthlyPrincipal);
        Amortizacion.Endbalance = FormatMoney(balance - monthlyPrincipal);
        Amortizacion.Payment = FormatMoney(payment);


        //update the balance for each loop iteration
        // balance = balance - monthlyPrincipal;
        arrayAmortizaion.push(Amortizacion);
    }
    //Final piece added to return string before returning it - closes the table
    //returns the concatenated string to the page
    console.log(arrayAmortizaion);
    return arrayAmortizaion;
}

function addDateToAmortizacion(datevalue, paymentModality) {
    let date = new Date(datevalue);
    switch (paymentModality) {
        case enumPaymentModality.DIARIO:
            //Execute logic enumPaymentModality.DIARIO
            date = date.addDays(1);
            break;
        case enumPaymentModality.SEMANAL:
            //Execute logic enumPaymentModality.SEMANAL
            date = date.addDays(7);
            break;
        case enumPaymentModality.QUINCENAL:
            //Execute logic enumPaymentModality.SEMANAL
            date = date.addDays(15);
            break;
        case enumPaymentModality.MENSUAL:
            //Execute logic  enumPaymentModality.MENSUAL
            date = date.addMonth(1);
            break;
        case enumPaymentModality.ANUAL:
            //Execute logic  enumPaymentModality.ANUAL
            date = date.addMonth(12);
            break;
    }
    return date;
}

function validateInputs(value) {
    //some code here to validate inputs
    if (value === null || value === "") {
        return false;
    }
    else {
        return true;
    }
}

function MakeTable(data, idElementoPadre) {
    let padre = document.getElementById(idElementoPadre);
    padre.innerHTML = "";
    let childTable = `
   <table  id ="tableData" class="table table-hover"  >
      <thead>
           <tr>
             <th> Cuota</th>
             <th> Fecha</th>
             <th> Capital restante</th>
             <th> Interes</th>
             <th> Total a Pagar</th>
             <th> Abono capital</th>
             <th> Balance final</th>

           </tr>
       </thead> <tbody id="ExportedTableFromObject"></tbody>
    </table>`;
    padre.innerHTML = childTable;
    let tbody = document.getElementById("ExportedTableFromObject");
    data.forEach(element => {
        let tr = document.createElement("tr");
        tr.innerHTML +=
            `
              ${AgregarFilas(element)}
                `;
        tbody.appendChild(tr);
    });
}

function AgregarColumnas(Columnas) {
    resultado = "";
    for (const prop in Columnas) {
        resultado += `<th>${prop}</th>`;
    }
    return resultado;
}

function AgregarFilas(Filas) {
    resultado = "";
    for (const prop in Filas) {
        resultado += `<th style="font-weight:500; color:#626567; font-size:15px;">${Filas[prop]}</th>`;
    }
    return resultado;
}

function FormatMoney(money) {
    return new Intl.NumberFormat().format(money.toFixed(2));
}

function FormatDate(date) {

    const ye = new Intl.DateTimeFormat('es', { year: 'numeric' }).format(date);
    const mo = new Intl.DateTimeFormat('es', { month: 'short' }).format(date);
    const da = new Intl.DateTimeFormat('es', { day: '2-digit' }).format(date);

    return `${da}-${mo}-${ye}`;
}


/*END CALCULATE DEBS*/



/**
 * Muestra un mensaje para preguntar si realmente quiere pagar y luego realiza el proceso.
 * @param {any} idLoan id del prestamo,
 * @param {any} idDeb id de la deuda,
 * @param {any} isDeb identifica si es de pago o no,
 * @param {any} amortizacion amortizacion,
 *
 */
const showPaymentDeb = (idLoan, idDeb, isDeb, amortizacion) => {

    let checkbox = '';
    let amortizationTotal = $('#amortizationTotal').val();
    //si es 3 es pago
    //&& Number(restcount) > 2
    if (isDeb !== 'Payment') checkbox = `<input type="checkbox" name="type" onclick="showOrHideElement('extraMount')"> <label>¿Abono extra?</label></br>`;
    const html = `<div class="container"><form class="form-group" action="/Loan/PaymentDeb" method='POST'>
            <input name="IdLoan" value=${idLoan} type="hidden" class="form-control" />
            <input name="AmortizationTotal" value=${amortizationTotal} type="hidden" class="form-control" />
            <input name="Amortization" value=${amortizacion} type="hidden" class="form-control" />
            <input name="IdDeb"  value=${idDeb}  type="hidden" class="form-control" /> ${checkbox}
            <input name="ExtraMount" id="extraMount" style="display:none;" type="number" class="form-control" placeholder="digite el monto extra" />
            <button class='btn btn-sm btn-primary'>ACEPTAR</button>
            </form></div>`;

    Swal.fire({
        title: "¿Seguro que desear realizar esta operación?",
        icon: 'warning',
        showCancelButton: false,
        showConfirmButton: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar',
        html: html
    });
};

/*GET AMORTIZATION**/

const GetAmortization = () => {
    const InitialCapital = document.getElementById("amount").value;
    const Interest = document.getElementById("interest").value;
    const Shares = document.getElementById("cuotas").value;
    const RateType = document.getElementById("typeOfTasa").value;
    const AmortitationType = document.getElementById("amortitationType").value;
    const PayM = document.getElementById("pay").value;

    $('#result').empty();
    const querystr = `?InitialCapital=${InitialCapital}&ActualCapital=${InitialCapital}&Interest=${Interest}
                        &Shares=${Shares}&RateType=${RateType}&AmortitationType=${AmortitationType}
                         &PaymentModality=${PayM}&`;
    fetch(`/Loan/GetAmortization${querystr}"`).then(result => result.text()).then((response) => {
        $('#result').html(response);
    });
};

const copyValue = (id) => {
    /* Get the text field */
    var copyText = document.getElementById('accessLink');

    /* Select the text field */
    copyText.select();
    copyText.setSelectionRange(0, 99999); /*For mobile devices*/
    document.execCommand('copy');

    $('#copyResult').show();
};

const getAccess = (accesslink, id) => {
    const html = `<div> <i class='text-muted'>Copie este link de acceso y entregueselo al su cliente:</i>
<input style='font-size:13px;' class='form-control mt-2 mb-2' value='${accesslink}/${id}' id='accessLink' />
<button class='btn btn-sm btn-primary' onclick='copyValue("accessLink")'>COPIAR</button></br><span style='display:none;' class='mt-2' id='copyResult'>Copiado</span></div>`;
    Swal.fire({
        title: "Nuevo Acceso",
        icon: 'info',
        showCancelButton: false,
        showConfirmButton: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar',
        html: html
    });
};