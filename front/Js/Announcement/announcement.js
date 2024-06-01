let url = window.location;
let params = new URLSearchParams(url.search);
let id = params.get('id');

fetch(
    "http://localhost:5059/api/Announcement/getAnnouncement?id="+id.toString()
    )
    .then(res=>res.json())
    .then(data=>start(data.data));

function start(data){
    setInfo()
    insertAnnouncementToContainer(data)
}

function insertAnnouncementToContainer(announcement){
    document.getElementById('container').insertAdjacentHTML(
        'afterbegin',
        `
        <div class="announcement" id="announcement">
            <div class = "textInfo">
                <p class="announcement-title">${announcement.title}</p>
                <p class="announcement-info">Описание:</p>
                <p class="announcement-description">${announcement.description}</p>
                <p class="announcement-price">Цена: ${announcement.price} ₽</p>
                <p class="announcement-address">Адрес: ${announcement.address}</p>      
                <p class="announcement-created">Создано: ${getNormalDate(announcement.created)}</p>     
                <button class="announcement-user-link" onclick="goToUser('${announcement.userId.toString()}')"> Перейти в профиль создателя</button> 
            </div>     
        </div>`
    );

    makeGallery(announcement);
}
function getNormalDate(date){
    const oldDate = new Date(date);

    return `${oldDate.getDate()}/${oldDate.getMonth() + 1}/${oldDate.getFullYear()}`;
}


let slideIndex = 1;
function makeGallery(announcement) {

    let bigImages=``;

    for(let imagePath of announcement.imagePaths){
        bigImages += `${makeBigImages(imagePath)}`;
    }

    let prevImages=``;

    for(let imagePath of announcement.imagePaths){
        prevImages += `${makePrevImages(imagePath)}`;
    }

    let txt =`
        <div class="container-gallery">
            ${bigImages}
            <a class="prev" onclick="plusSlides(-1)">&#10094;</a>
            <a class="next" onclick="plusSlides(1)">&#10095;</a>
          <div class="row">
            ${prevImages}
          </div>
        </div>
    `
    document.getElementById('announcement').insertAdjacentHTML(
        'afterbegin',
        `
            ${txt}
        `
    );
    showSlides(slideIndex);

}
function makeBigImages(path){
    const imgUrl='http://127.0.0.1:8080/';
    if (path[0] !== "" ) {
        return` 
            <div class="mySlides">
              <img src=${imgUrl}${path} class="big-img">
            </div>
            `
    } else {
        return` 
            <div class="mySlides">
              <img src="http://127.0.0.1:8080/empty.png" class="big-img">
            </div>`
    }
}
function makePrevImages(path){
    const imgUrl='http://127.0.0.1:8080/';
    if (path[0] !== "" ) {
        return` 
            <div class="column">
                <img class="demo cursor" src=${imgUrl}${path} style="width:100%">
            </div>
            `
    } else {
        return` 
             <div class="column">
                <img class="demo cursor" src="http://127.0.0.1:8080/empty.png" style="width:100%">
             </div>
            `
    }
}
function plusSlides(n) {
    showSlides(slideIndex += n);
}
function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("demo");
    if (n > slides.length) {slideIndex = 1}
    if (n < 1) {slideIndex = slides.length}
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex-1].style.display = "block";
    dots[slideIndex-1].className += " active";
}



function goToUser(userId){
    window.location.href = `http://localhost:63342/front/front/Html/User.html?id=${userId}`;
}













