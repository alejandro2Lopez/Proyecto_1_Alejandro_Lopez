
let listaTickets = [];
$(document).ready(function () {
    GetListaTicket();
});
$("#formAgregarTicket").on("submit", function (e) {

    e.preventDefault();

    const ticket = {
        titulo: $("#titulo").val().trim(),
        descripcion: $("#descripcion").val().trim(),
        fk_id_categoria: Number($("#categoria").val())
    };

    if (!ticket.titulo || !ticket.descripcion ||
        !ticket.fk_id_categoria) {
        alert("Debe completar todos los campos.");
        return;
    }

    $.ajax({
        url: '/Ticket/InsertarTicket',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(ticket),

        success: function (respuesta) {

            alert("Ticket agregado correctamente.");

            $("#formAgregarTicket")[0].reset();

            bootstrap.Modal.getInstance(
                document.getElementById("modalAgregarTicket")
            ).hide();

            // Actualizar la tabla de tickets
            GetListaTicket()
        },

        error: function (xhr) {
            console.error("Error:", xhr);
            alert("No se pudo agregar el ticket.");
        }
    });
});

function cargarCategorias() {

    $.ajax({
        url: '/Catalog/GetCategoriaList',
        type: 'GET',
        dataType: 'json',

        success: function (respuesta) {

            $("#categoria").empty();

            $("#categoria").append(
                '<option value="">Seleccione una categoría</option>'
            );

            respuesta.list.forEach(function (categoria) {

                $("#categoria").append(
                    $("<option>")
                        .val(categoria.id_categoria)
                        .text(categoria.descripcion)
                );

            });
        },

        error: function (xhr) {
            console.error("Error al cargar categorías:", xhr);
        }
    });
}
$("#modalAgregarTicket").on("show.bs.modal", function () {
    cargarCategorias();
});

function GetListaTicket() {

    $.ajax({
        url: '/Ticket/GetListaTicket',
        type: 'GET',
        dataType: 'json',

        success: function (respuesta) {

            listaTickets = respuesta.message;

            $("#tablaTickets").empty();

            listaTickets.forEach(function (ticket) {

                const fila = $("<tr>");

                $("<td>").text(ticket.id_ticket).appendTo(fila);

                $("<td>").text(
                    ticket.usuarioSolicitante
                ).appendTo(fila);

                $("<td>").text(ticket.titulo).appendTo(fila);

                $("<td>").text(
                    ticket.descripcion
                ).appendTo(fila);

                $("<td>").text(
                    formatearFecha(ticket.fecha_creacion)
                ).appendTo(fila);

                $("<td>").text(
                    ticket.UsuarioAsignado
                ).appendTo(fila);

                $("<td>").text(
                    ticket.fk_id_estado_ticket
                ).appendTo(fila);

                const boton = $("<button>")
                    .addClass("btn btn-primary btn-sm btnVerTicket")
                    .text("Ver más")
                    .attr("data-id", ticket.id_ticket)
                    .attr("data-bs-toggle", "modal")
                    .attr("data-bs-target", "#modalDetalleTicket");

                $("<td>").append(boton).appendTo(fila);

                $("#tablaTickets").append(fila);

            });
              actualizarIndicadores();
        },

        error: function (xhr) {
            console.error("Error al obtener tickets:", xhr);
        }
    });
}
function formatearFecha(fecha) {

    if (!fecha) {
        return "Sin fecha";
    }

    const resultado = /\/Date\((-?\d+)\)\//.exec(fecha);

    const fechaConvertida = resultado
        ? new Date(Number(resultado[1]))
        : new Date(fecha);

    if (isNaN(fechaConvertida.getTime())) {
        return "Fecha inválida";
    }

    return fechaConvertida.toLocaleString("es-CR");
}
$(document).on("click", ".btnVerTicket", function () {

    const idTicket = $(this).attr("data-id");

    $("#idTicketDetalle").val(idTicket);

    cargarDetalleTicket(idTicket);
    cargarComentarios(idTicket);
});

function cargarDetalleTicket(idTicket) {

    const ticket = listaTickets.find(
        t => t.id_ticket === Number(idTicket)
    );

    if (!ticket) {
        alert("No se encontró el ticket.");
        return;
    }

    $("#idTicketDetalle").val(ticket.id_ticket);
    $("#tituloDetalle").val(ticket.titulo);
    $("#descripcionDetalle").val(ticket.descripcion);

    $.when(
        cargarCategoriasDetalle(),
        cargarUsuariosAsignables(),
        cargarEstados()
    ).done(function () {

        $("#categoriaDetalle").val(
            String(ticket.fk_id_categoria)
        );

        $("#usuarioAsignadoDetalle").val(
            ticket.fk_id_usuario_asignado == null
                ? ""
                : String(ticket.fk_id_usuario_asignado)
        );

        $("#estadoDetalle").val(
            String(ticket.fk_id_estado_ticket)
        );

    }).fail(function () {
        console.error("Error al cargar los catálogos.");
    });
}
function cargarComentarios(idTicket) {

    $.ajax({
        url: '/Ticket/GetComentariosId',
        type: 'GET',
        data: { id_ticket: idTicket },
        dataType: 'json',

        success: function (respuesta) {

            const comentarios = respuesta.message;

            $("#listaComentarios").empty();

            if (comentarios.length === 0) {
                $("#listaComentarios").text(
                    "Este ticket no tiene comentarios."
                );
                return;
            }

            comentarios.forEach(function (comentario) {

                const contenedor = $("<div>")
                    .addClass("card mb-2 p-3");

                const fecha = $("<small>")
                    .addClass("text-muted mb-2")
                    .text(comentario.fecha);

                const texto = $("<p>")
                    .addClass("mb-0")
                    .text(comentario.texto);

                contenedor.append(fecha, texto);

                $("#listaComentarios").append(contenedor);
            });
        },

        error: function (xhr) {
            console.error("Error al obtener comentarios:", xhr);
            $("#listaComentarios").text(
                "No fue posible cargar los comentarios."
            );
        }
    });
}
function cargarCategoriasDetalle() {

    return $.ajax({
        url: '/Catalog/GetCategoriaList',
        type: 'GET',
        dataType: 'json',

        success: function (respuesta) {

            const select = $("#categoriaDetalle");
            select.empty();

            respuesta.list.forEach(function (categoria) {
                select.append(
                    $("<option>")
                        .val(categoria.id_categoria)
                        .text(categoria.descripcion)
                );
            });
        }
    });
}
function cargarUsuariosAsignables() {

        return $.ajax({
            url: '/Usuario/ObtenerUsuariosAsignables',
            type: 'GET',
            dataType: 'json',

            success: function (respuesta) {

                const select = $("#usuarioAsignadoDetalle");

                select.empty();

                select.append(
                    $("<option>")
                        .val("")
                        .text("Sin asignar")
                );

                respuesta.message.forEach(function (usuario) {

                    select.append(
                        $("<option>")
                            .val(usuario.id_usuario)
                            .text(usuario.nombre_usuario)
                    );

                });
            },

            error: function (xhr) {
                console.error("Error al cargar usuarios:", xhr);
            }
        });
    }

function cargarEstados() {

    return $.ajax({
        url: '/Catalog/GetEstadoTicketList',
        type: 'GET',
        dataType: 'json',

        success: function (respuesta) {

            const select = $("#estadoDetalle");

            select.empty();

            select.append(
                $("<option>")
                    .val("")
                    .text("Seleccione un estado")
            );

            respuesta.list.forEach(function (estado) {

                select.append(
                    $("<option>")
                        .val(estado.id_estado_ticket)
                        .text(estado.descripcion)
                );

            });
        },

        error: function (xhr) {
            console.error("Error al cargar estados:", xhr);
        }
    });
}

$("#formComentario").on("submit", function (e) {

    e.preventDefault();

    const texto = $("#textoComentario").val().trim();
    const idTicket = Number($("#idTicketDetalle").val());

    if (!texto) {
        alert("Debe escribir un comentario.");
        return;
    }

    const comentario = {
        id_ticket: idTicket,
        texto: texto
    };

    $.ajax({
        url: '/Ticket/AgregarComentarioTicket',
        type: 'POST',
        data: {
            ...comentario,
            __RequestVerificationToken:
                $("#formComentario input[name='__RequestVerificationToken']").val()
        },

        success: function (respuesta) {

            if (respuesta.status === 200) {

                alert("Comentario agregado correctamente.");

           
                $("#textoComentario").val("");

                cargarComentarios(idTicket);

            } else {
                alert(respuesta.message || "No se pudo agregar el comentario.");
            }
        },

        error: function (xhr) {
            console.error("Error:", xhr);
            alert("Ocurrió un error al agregar el comentario.");
        }
    });
});

$("#btnActualizarTicket").on("click", function () {

    const ticket = {
        id_ticket: Number($("#idTicketDetalle").val()),

        fk_id_usuario_asignado:
            $("#usuarioAsignadoDetalle").val() === ""
                ? null
                : Number($("#usuarioAsignadoDetalle").val()),

        fk_id_estado_ticket:
            Number($("#estadoDetalle").val())
    };

    if (!ticket.fk_id_estado_ticket) {
        alert("Debe seleccionar un estado.");
        return;
    }

    $.ajax({
        url: '/Ticket/ActualizarTicket',
        type: 'POST',
        data: ticket,

        success: function (respuesta) {

            if (respuesta.status === 200) {

                alert("Ticket actualizado correctamente.");

                // Cerrar modal
                bootstrap.Modal.getInstance(
                    document.getElementById("modalDetalleTicket")
                ).hide();

                // Actualizar tabla
                GetListaTicket();

            } else {
                alert(respuesta.message || "Error al actualizar.");
            }
        },

        error: function (xhr) {
            console.error("Error:", xhr);
            alert("No se pudo actualizar el ticket.");
        }
    });
});
function filtrarTickets() {

    const busqueda = $("#filtroBusqueda").val().toLowerCase().trim();
    const estado = $("#filtroEstado").val();

    const ticketsFiltrados = listaTickets.filter(function (ticket) {

        const coincideBusqueda =
            (ticket.titulo || "").toLowerCase().includes(busqueda) ||
            (ticket.usuarioSolicitante || "").toLowerCase().includes(busqueda);

        const coincideEstado =
            estado === "" ||
            String(ticket.fk_id_estado_ticket) === estado;

        return coincideBusqueda && coincideEstado;
    });

    mostrarTickets(ticketsFiltrados);
}
$("#filtroBusqueda").on("input", function () {
    filtrarTickets();
});

$("#filtroEstado").on("change", function () {
    filtrarTickets();
});

function actualizarIndicadores() {

  
    if ($("#pTotalTicket").length === 0) {
        return;
    }

    const abiertos = listaTickets.filter(
        t => Number(t.fk_id_estado_ticket) === 1
    ).length;

    const enProceso = listaTickets.filter(
        t => Number(t.fk_id_estado_ticket) === 2
    ).length;

    const resueltos = listaTickets.filter(
        t => Number(t.fk_id_estado_ticket) === 3
    ).length;

    const cerrados = listaTickets.filter(
        t => Number(t.fk_id_estado_ticket) === 4
    ).length;

    $("#pTotalTicket").text(abiertos);
    $("#pendingTotalTicket").text(enProceso);
    $("#totalTicket").text(resueltos);
    $("#solveTotalTicket").text(cerrados);
}
function mostrarTickets(tickets) {

    $("#tablaTickets").empty();

    tickets.forEach(function (ticket) {

        const fila = $("<tr>");

        $("<td>").text(ticket.id_ticket).appendTo(fila);
        $("<td>").text(ticket.usuarioSolicitante).appendTo(fila);
        $("<td>").text(ticket.titulo).appendTo(fila);
        $("<td>").text(ticket.descripcion).appendTo(fila);
        $("<td>").text(formatearFecha(ticket.fecha_creacion)).appendTo(fila);
        $("<td>").text(ticket.UsuarioAsignado).appendTo(fila);
        $("<td>").text(ticket.fk_id_estado_ticket).appendTo(fila);

        const boton = $("<button>")
            .addClass("btn btn-primary btn-sm btnVerTicket")
            .text("Ver más")
            .attr("data-id", ticket.id_ticket)
            .attr("data-bs-toggle", "modal")
            .attr("data-bs-target", "#modalDetalleTicket");

        $("<td>").append(boton).appendTo(fila);

        $("#tablaTickets").append(fila);
    });

    if (tickets.length === 0) {
        $("#tablaTickets").append(
            '<tr><td colspan="8" class="text-center">No se encontraron tickets.</td></tr>'
        );
    }
}