start()

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

function getContainerInfo(){
    return `
        <div class="createAnnouncement" id="createAnnouncement">
            <p class="createAnnouncement-title" id="createAnnouncement-title">Создание объявления</p>
             <form action="http://localhost:5059/api/Announcement/createAnnouncement" method="post" id="createAnnouncementForm">
                <div class="createAnnouncement" id="createAnnouncement-info">
                    <p class="createAnnouncement-info-text">Заголовок</p>
                    <p class="createAnnouncement-info-text">Описание</p>
                    <p class="createAnnouncement-info-text">Цена</p>
                    <p class="createAnnouncement-info-text">Адрес</p>
                </div>
                <div class="createAnnouncement-input" id="createAnnouncement-input">
                    <input type="text" class="textInput" name="title" id="titleInput" placeholder="Введите заголовок..." required>
                    <input type="text" class="textInput" name="description" id="descriptionInput" placeholder="Введите описание..." required>    
                    <input type="number" class="textInput" name="price" id="priceInput" placeholder="Введите цену..." required>
                    <input type="text" class="textInput" name="address" id="addressInput" placeholder="Введите адрес..." required>
                </div> 
                <input type="submit" class="submit-btn" value="Подать объявление">
            </form>
        </div>    
    `
}
function submittingForm(){
    const form = document.getElementById('createAnnouncementForm');

    form.addEventListener('submit', function(event) {
        event.preventDefault();

        const formData = new FormData(form);

        const registerData = JSON.stringify({
            userId: `${localStorage.getItem('userId')}`,
            title: `${formData.get('title')}`,
            description: `${formData.get('description')}`,
            price: `${formData.get('price')}`,
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
        window.location.href = `http://localhost:63342/front/front/Html/Announcement.html?id=${data.data.id}`;
    }
}