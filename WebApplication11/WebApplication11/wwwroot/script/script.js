


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
    popup.classList.remove('active');
    var overlayer = document.getElementById("overlay");
    overlayer.classList.remove('active2');

}

