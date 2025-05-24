function showLogin() {
    const loginContainer = document.getElementById("login-container");
    if (loginContainer.style.display === "none" || loginContainer.style.display === "") {
        loginContainer.style.display = "block"; // Muestra el formulario
    } else {
        loginContainer.style.display = "none"; // Oculta el formulario
    }
}

function showLoginUsuario() {
    const loginContainer = document.getElementById("login-usuario-container");
    if (loginContainer.style.display === "none" || loginContainer.style.display === "") {
        loginContainer.style.display = "block"; // Muestra el formulario
    } else {
        loginContainer.style.display = "none"; // Oculta el formulario
    }
}

function ver(){
   navbar.style.backgroundColor = "black";
}


