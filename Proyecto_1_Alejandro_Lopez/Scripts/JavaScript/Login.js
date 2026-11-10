$("#formLogin").on("submit", function (e) {
    e.preventDefault();

    var email = $("#email").val().trim();
    var password = $("#password").val();

    $.ajax({
        url: '/Login/ValidarUsuario',
        type: 'POST',
        dataType: 'json',
        data: {
            email: email,
            password: password
        },
        success: function (respuesta) {

            if (respuesta.status === 200) {
                alert(respuesta.message);

                window.location.href = '/Ticket/Index';
            }
            else if (respuesta.status === 403) {
                alert(respuesta.message);
            }

        },
        error: function (xhr) {
            console.error("Error:", xhr);
            alert("Ocurrió un error al iniciar sesión.");
        }
    });
});