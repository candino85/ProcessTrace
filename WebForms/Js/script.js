document.addEventListener("DOMContentLoaded", search)
const URL_API = 'https://localhost:7157/api/'
function init() {
    search()
}
async function search() {

    var url = URL_API + 'usuario'

    var response = await fetch(url, {
        "method": 'GET',
        "headers": {
            "Content-Type": "application/json"
        }
    })

    var resultado = await response.json();

    var html = ''

    for (usuario of resultado) {

        var row = `
                            <tr>
                                <th scope="row">${usuario.id}</th>
                                <td>${usuario.nombre}</td>
                                <td>${usuario.apellido}</td>
                                <td>${usuario.email}</td>
                                <td class="text-end">
                                    <button type="button" class="btn btn-primary">Editar</button>
                                    <button onclick="remove(${usuario.id})" type="button" class="btn btn-secondary">Eliminar</button>
                                </td>
                            </tr>
        `
        html = html + row;
    }

    document.querySelector('#form1 > div:nth-child(3) > section > div > div > table > tbody').outerHTML = html
}

async function remove(id) {
    respuesta = confirm('¿Está seguro de eliminar el usuario ' + usuario.nombre + ' ' + usuario.apellido + '?')

    if (respuesta) {
        var url = URL_API + 'usuario/' + id

        await fetch(url, {
            "method": 'DELETE',
            "headers": {
                "Content-Type": "application/json"
            }
        })
        window.location.reload();
    }
}

/*(async() => {*/
//  const result = await b_confirm('IT WILL BE DELETED')
//  alert(result)
//})();

//async function b_confirm(msg) {
//    const modalElem = document.createElement('div')
//    modalElem.id = "modal-confirmdelete"
//    modalElem.className = "modal"
//    modalElem.innerHTML = `
//    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
//      <div class="modal-content">
//        <div class="modal-body fs-6">
//          <p>${msg}</p>
//          <p>Are you sure?</p>
//      </div>    <!-- modal-body -->
//      <div class="modal-footer" style="border-top:0px">
//        <button id="modal-btn-descartar" type="button" class="btn btn-secondary">Cancel</button>
//        <button id="modal-btn-aceptar" type="button" class="btn btn-primary">Accept</button>
//      </div>
//    </div>
//  </div>
//  `
//    const myModal = new bootstrap.Modal(modalElem, {
//        keyboard: false,
//        backdrop: 'static'
//    })
//    myModal.show()

//    return new Promise((resolve, reject) => {
//        document.body.addEventListener('click', response)

//        function response(e) {
//            let bool = false
//            if (e.target.id == 'modal-btn-descartar') bool = false
//            else if (e.target.id == 'modal-btn-aceptar') bool = true
//            else return

//            document.body.removeEventListener('click', response)
//            document.body.querySelector('.modal-backdrop').remove()
//            modalElem.remove()
//            resolve(bool)
//        }
//    })
/*}*/