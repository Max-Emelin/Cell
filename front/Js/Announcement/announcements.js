fetch('http://localhost:5059/api/Announcement/getAllAnnouncements')
    .then(res=>res.json())
    .then(data=>start(data.data));

let userId = localStorage.getItem('userId');

function start(data){
    setInfo()
    insertAnnouncementsToContainer(data)
}



function insertAnnouncementsToContainer(announcements){
    for(let announcement of announcements){
        insertOneAnnouncementToContainer(announcement);
    }
}
function insertOneAnnouncementToContainer(announcement){
    let urlLook=new URL('http://localhost:63342/front/front/Html/Announcement.html')
    urlLook.searchParams.append('id', announcement.id);

    document.getElementById('container').insertAdjacentHTML(
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
function getImageByPath(path){
    const imgUrl='http://127.0.0.1:8080/';
    if (path[0] !== "" ) {
        return` <img class="announcement-image" src=${imgUrl}${path} >`
    } else {
        return` <img class="announcement-image" src="http://127.0.0.1:8080/empty.png">`
    }
}