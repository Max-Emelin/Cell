let url = window.location;
let params = new URLSearchParams(url.search);
let id = params.get('id');

fetch(
    "http://localhost:5059/api/User/getUser?id="+id.toString()
)
    .then(res=>res.json())
    .then(data=>start(data.data));

function start(values){
    setInfo()
    insertUserToContainer(values);
    insertUserCommentsToContainer();
    insertUserAnnouncementsToContainer();
}

function insertUserToContainer(user){
    document.getElementById('container').insertAdjacentHTML(
        'afterbegin',
        `
        <div class="user-container">
            <p class="info-title"> Пользователь </p>
            <div class="user" id="user">
                <div class ="user-keys">
                    <p class="user-key">Имя:</p>
                    <p class="user-key">Фамилия:</p>
                    <p class="user-key">Телефон:</p>
                    <p class="user-key">Адрес:</p>
                    <p class="user-key">Дата регистрации:</p>
                </div>
                <div class="user-values">
                    <p class="user-valuename">${user.name}</p>
                    <p class="user-value">${user.lastName}</p>
                    <p class="user-value">${user.phoneNumber}</p>
                    <p class="user-value">${user.address}</p>
                    <p class="user-value">${getNormalDate(user.created)}</p>
                </div>
            </div>
        </div>
        
        `
    );
}
function insertUserCommentsToContainer(){
    fetch(
        "http://localhost:5059/api/Comment/getUserComments?userId="+id.toString()
    )
        .then(res=>res.json())
        .then(data=>commentProcessing(data.data))
}
function insertOneCommentToContainer(comment){
    let urlLook=new URL('http://localhost:63342/front/front/Html/User.html')
    urlLook.searchParams.append('id', comment.userToId);

    document.getElementById('comments').insertAdjacentHTML(
        'afterbegin',
        `
        <div class="comment">
            <a class="user-from-link" href="${urlLook}"> Автор коментария </a>
            <p class="comment-text">${comment.text}</p>
        </div>`
    );
}
function commentProcessing(comments){
    document.getElementById('container').insertAdjacentHTML(
        'beforeend',
        `
            <div class="comments-container">
                <p class="info-title comment-title"> Коментарии </p>
                <div class="comments" id="comments"></div>
            </div>
            `
    );

    for(let comment of comments){
        insertOneCommentToContainer(comment);
    }
}


function insertUserAnnouncementsToContainer(){
    fetch(
        "http://localhost:5059/api/Announcement/getUserAnnouncements?userId="+id.toString()
    )
        .then(res=>res.json())
        .then(data=>insertAnnouncements(data.data))
}

function insertAnnouncements(announcements){
    document.getElementById('container').insertAdjacentHTML(
        'beforeend',
        `
            <div class="announcements-container">
                <p class="announcements-title"> Объявления </p>
                <div class="announcements" id="announcements"></div>
            </div>
            `
    );


    for(let announcement of announcements){
        insertOneAnnouncementToContainer(announcement);
    }
}

function insertOneAnnouncementToContainer(announcement){
    let urlLook=new URL('http://localhost:63342/front/front/Html/Announcement.html')
    urlLook.searchParams.append('id', announcement.id);

    document.getElementById('announcements').insertAdjacentHTML(
        'afterbegin',
        `
        <div class="announcement">
            <a class="announcement-link" href="${urlLook}">
                ${getImageByPath(announcement.imagePaths)}
                <p class="announcement-title">${announcement.title}</p>
                <p class="announcement-price">${announcement.price} ₽</p>
            </a>
        </div>`
    );
}








function getNormalDate(date){
    const oldDate = new Date(date);

    return `${oldDate.getDate()}/${oldDate.getMonth() + 1}/${oldDate.getFullYear()}`;
}
function getImageByPath(path){
    const imgUrl='http://127.0.0.1:8080/';
    if (path[0] !== "" ) {
        return` <img class="announcement-image" src=${imgUrl}${path} >`
    } else {
        return` <img class="announcement-image" src="http://127.0.0.1:8080/empty.png">`
    }
}
