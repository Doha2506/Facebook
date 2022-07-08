


const openModelButtons = document.getElementById("buttonR");
const closeModelButtons = document.getElementById("close");


// openModelButtons.addEventListener('click',alert("asd"));


function openPopup() {
    var popup = document.getElementById("modal");
    popup.classList.toggle("active");
    var overlayer = document.getElementById("overlay");
    overlayer.classList.add('active2');
}

function closePopup() {
    var popup = document.getElementById("modal");
    popup.classList.remove('show');
    var overlayer = document.getElementById("overlay");
    overlayer.classList.remove('active2');

}

function openPopup1() {
    var popup = document.getElementById("serchPopup");
    popup.classList.add("show");
    var overlayer = document.getElementById("overlay");
    overlayer.classList.add('active2');
}

function closePopup1() {
    var popup = document.getElementById("serchPopup");
    popup.classList.remove('show');
    var overlayer = document.getElementById("overlay");
    overlayer.classList.remove('active2');

}
function openPopup2() {
    var popup = document.getElementById("new-post-popup");
    popup.classList.add("new-post-popup-show");
    var overlayer = document.getElementById("overlay2");
    overlayer.classList.add('active3');
}

function closePopup2() {
    var popup = document.getElementById("new-post-popup");
    popup.classList.remove('new-post-popup-show');
    var overlayer = document.getElementById("overlay2");
    overlayer.classList.remove('active3');

}


function openPopupPhoto1() {
    var popup = document.getElementById("change-personal-photo");
    popup.classList.add("change-personal-photo");
}
function closePopupPhoto1() {
    var popup = document.getElementById("change-personal-photo");
    popup.classList.remove("change-personal-photo");
}
function openPopupPhoto2() {
    var popup = document.getElementById("change-cover-photo");
    popup.classList.add("change-cover-photo");
}
function closePopupPhoto2() {
    var popup = document.getElementById("change-cover-photo");
    popup.classList.remove("change-cover-photo");
}

function openEditForm() {
    var popup = document.getElementById("edit-form");
    popup.classList.add("edit-form-show");
    var overlayer = document.getElementById("overlay-form");
    overlayer.classList.add('active-form');
}

function closeEditForm() {
    var popup = document.getElementById("edit-form");
    popup.classList.remove("edit-form-show");
    var overlayer = document.getElementById("overlay-form");
    overlayer.classList.remove('active-form');
}

function buttonBlue(like) {
        var buttonB= document.getElementById(like);
        buttonB.classList.add("colorBlue");
}

function bodycommentShow(common) {
    var buttonc = document.getElementById(common);
    buttonc.classList.add("bodycommentShow");
}

