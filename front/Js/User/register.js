start()

function getContainerInfo(){
    return `
        <div class="register" id="register">
            <p class="register-title" id="register-title">Регистрация</p>
             <form action="http://localhost:5059/api/User/registerUser" method="post" id="registerForm">
                <div class="register-info" id="register-info">
                    <p class="register-info-text">Логин</p>
                    <p class="register-info-text">Пароль</p>
                    <p class="register-info-text">Имя</p>
                    <p class="register-info-text">Фамилия</p>
                    <p class="register-info-text">Почта</p>
                    <p class="register-info-text">Телефон</p>
                    <p class="register-info-text">Адрес</p> 
                </div>
                <div class="register-input" id="register-input">
                    <input type="text" class="textInput" name="login" id="loginInput" placeholder="Введите логин..." required>
                    <input type="text" class="textInput" name="password" id="passwordInput" placeholder="Введите пароль..." required>    
                    <input type="text" class="textInput" name="name" id="nameInput" placeholder="Введите имя..." required>
                    <input type="text" class="textInput" name="lastName" id="lastNameInput" placeholder="Введите фамилию..." required>
                    <input type="text" class="textInput" name="email" id="emailInput" placeholder="Введите почту...">
                    <input type="text" class="textInput" name="phoneNumber" id="phoneNumberInput" placeholder="Введите телефон..." required>
                    <input type="text" class="textInput" name="address" id="addressInput" placeholder="Введите адрес...">  
                </div> 
                <input type="submit" class="submit-btn" value="Зарегистрироваться">
            </form>
        </div>    
    `
}
function start(){
    setInfo();

    document.getElementById('container').insertAdjacentHTML(
        'afterbegin',
        `
        ${getContainerInfo()}
        `
    )

    submittingForm();
}

function submittingForm(){
    const form = document.getElementById('registerForm');

    form.addEventListener('submit', function(event) {
        event.preventDefault();

        const formData = new FormData(form);

        const registerData = JSON.stringify({
                login: `${formData.get('login')}`,
                password: `${formData.get('password')}`,
                name: `${formData.get('name')}`,
                lastName: `${formData.get('lastName')}`,
                email: `${formData.get('email')}`,
                phoneNumber: `${formData.get('phoneNumber')}`,
                address: `${formData.get('address')}`
            });

        fetch(form.action, {
            method: 'POST',
            body: registerData,
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(res=>res.json())
            .then(data=>responseProcessing(data))
            .catch(error => {
                console.error('Ошибка:', error);
            });
    });
}
function responseProcessing(data){
    if(data.isSuccess){
        localStorage.removeItem('userId');
        localStorage.setItem('userId', `${data.data.id}`);
        window.location.href = `http://localhost:63342/front/front/Html/User.html?id=${localStorage.getItem('userId')}`;

    }
}