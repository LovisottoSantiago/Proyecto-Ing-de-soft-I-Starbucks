function showLogin(){
    const login = document.getElementsByClassName("login-container");
    if (login.style.display === "none") {
        login.style.display = "block";
    } else {
        login.style.display = "none";
    }
}