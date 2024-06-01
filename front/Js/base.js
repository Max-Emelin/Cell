let loginButton;
let loginPopup;

function getHeader() {
    let loginButtons =``;

    if(localStorage.getItem('userId') != null){
        loginButtons = `
            <button class="nav-button" onclick="goToCreateAnnouncement()">Создать объявление</button>
            <button class="nav-button" onclick="goToProfile()">Профиль</button>
            <button class="nav-button" onclick="logout()">Выйти из аккаунта</button>
            
        `
    }else{
        loginButtons = `
            <button class="nav-button" id="loginButton">Авторизация</button>
            <button class="nav-button" onclick="goToRegistration()">Регистрация</button>
        `
    }

    return ` 
        <div class="site-title" id="site-title">
            <span class="site-title-text">Cell</span>
            <div class="nav-buttons">
                    <button class="nav-button" onclick="goToAllAnnouncements()"> Посмотреть объявления </button>
                    ${loginButtons}
            </div>
        </div>
    `;
}
function goToAllAnnouncements() {
    window.location.href = `http://localhost:63342/front/front/Html/Announcements.html`;
}
function goToRegistration(){
    window.location.href = `http://localhost:63342/front/front/Html/Register.html`;
}
function logout(){
    localStorage.removeItem('userId');
    location.reload();
}
function goToProfile(){
    window.location.href = `http://localhost:63342/front/front/Html/User.html?id=${localStorage.getItem('userId')}`;
}
function goToCreateAnnouncement(){
    window.location.href = `http://localhost:63342/front/front/Html/CreateAnnouncement.html`;
}

function getContainer() {
    return `
        <div class="container" id="container"></div>    
    `
}
function setInfo() {
    document.body.insertAdjacentHTML(
        'afterbegin',
        `
        ${getHeader()}
        ${getContainer()}
        ${getLoginPopup()}
        `
    )

    setupLoginPopup();
}

function getLoginPopup() {
    return `
        <div id="loginPopup" class="popup">
            <div class="popup-content">
                <span class="close" onclick="closeLoginPopup()">&times;</span>
                <h2 class="login-h">Авторизация</h2>
                <form id="loginForm" action="http://localhost:5059/api/User/loginUser" method="post">
                    <label for="login">Логин:</label><br>
                    <input type="text" class="login-input" id="login" name="login" placeholder="Введите логин..." required><br><br>
                    <label for="password">Пароль:</label><br>
                    <input type="password" class="login-input" id="password" name="password" placeholder="Введите пароль..." required><br>
                    <input type="submit" class="login-submit-btn" value="Вход">
                </form>
            </div>
        </div>
    `;
}
function setupLoginPopup() {
    if (localStorage.getItem('userId') != null){
        return;
    }

    loginButton = document.getElementById('loginButton');
    loginPopup = document.getElementById('loginPopup');

    loginButton.addEventListener('click', function() {
        loginPopup.style.display = 'block';
    });

    const form = document.getElementById('loginForm');


    form.addEventListener('submit', function(event) {
        event.preventDefault();

        const formData = new FormData(form);

        const loginData = JSON.stringify({
            login: `${formData.get('login')}`,
            password: `${formData.get('password')}`
        });

        fetch(form.action, {
            method: 'POST',
            body: loginData,
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(res=>res.json())
            .then(data=>loginResponse(data))
            .catch(error => {
                console.error('Ошибка:', error);
            });
    });
}
function loginResponse(data) {
    if (data.isSuccess) {
        localStorage.removeItem('userId');
        localStorage.setItem('userId', `${data.data}`);
        closeLoginPopup();
        location.reload();
    }
}
function closeLoginPopup() {
    loginPopup.style.display = 'none';
}

