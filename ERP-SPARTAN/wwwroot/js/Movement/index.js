const PayAll = (id) => {
    Swal.fire({
        title: "SALDAR CUENTA",
        text: "SEGURO QUE DESEA REALIZAR ESTA OPERACION",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar'
    }).then((result) => {
        if (result.value) {
            fetch(`/MOVEMENT/PAYALL?id=${id}`, {
                method: 'post',
                body: JSON.stringify(id)
            }).then((response) => {
                if (response.status === 200) {
                    Swal.fire("Operación realizada con exito", '', 'success').then(result => {
                        location.reload();
                    });
                } else {
                    Swal.fire("Ocurrio un error", "intente de nuevo", 'error');
                }
            });
        }
    });
};