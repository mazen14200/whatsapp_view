////////////////////////////show main carouse items by bottons ( etc ,مؤجرة , احصائيات)////////////////////////////

function showCarouselItem(itemNumber) {
    const carouselItems = document.getElementsByClassName('main-carousel-item');
    for (let i = 0; i < carouselItems.length; i++) {
        carouselItems[i].classList.remove('active');
    }

    const carouselItem = document.getElementById(`carouselItem${itemNumber}`);
    carouselItem.classList.add('active');
}

$('#mainCarousel').carousel();

$('.carousel').carousel();
//////////////////collapse control///////////////////
$('.btn-link').click(function () {
    $('.collapse').collapse('hide');
    $($(this).data('target')).collapse('show');
});
///////////////////item - color -red////////////////////
$(document).ready(function () {
    $('.collapse').each(function () {
        var $collapseElement = $(this);
        $collapseElement.find('p').each(function () {
            if ($(this).text().trim() === 'منتهي' || $(this).text().trim() === 'علي وشك الانتهاء') {
                $(this).css('color', 'red');
                $collapseElement.closest('.row').find('.btn-link-collapse-fix').css('color', 'red');
                $collapseElement.closest('.row').find('.btn-link-collapse-docs').css('color', 'red');

            }
        });
    });
});
//////////////for المؤجرة////////////
function initCarousel() {
    const carousel = document.getElementById('carouselItem2');
    const carouselItems = carousel.getElementsByClassName('carousel-item');
    let currentIndex = 0;
    function showItem(index) {
        for (let i = 0; i < carouselItems.length; i++) {   //// carouselItems.length is the number of items  in the list you will display in the carsouel you can replace it with their number //////////// 
            carouselItems[i].classList.remove('active');
        }
        carouselItems[index].classList.add('active');
        carouselItems[index].classList.add('animate'); // Add animate class for animation
        setTimeout(() => {
            carouselItems[index].classList.remove('animate'); // Remove animate class after the animation duration
        }, 1000);
    }

    function rotateItems() {
        // Check if the mouse is over the carousel
        const isMouseOverCarousel = carousel.matches(':hover');
        if (!isMouseOverCarousel) {
            currentIndex = (currentIndex + 1) % carouselItems.length;
            showItem(currentIndex);
        }
    }

    setInterval(rotateItems, 3000);

    showItem(currentIndex);
}
//////////////for متاحة////////////
function initCarousel2() {
    const carousel = document.getElementById('carouselItem3');
    const carouselItems = carousel.getElementsByClassName('carousel-item');
    let currentIndex = 0;

    function showItem(index) {
        for (let i = 0; i < carouselItems.length; i++) {   //// carouselItems.length is the number of items  in the list you will display in the carsouel you can replace it with their number //////////// 
            carouselItems[i].classList.remove('active');
        }
        carouselItems[index].classList.add('active');
        carouselItems[index].classList.add('animate'); // Add animate class for animation
        setTimeout(() => {
            carouselItems[index].classList.remove('animate'); // Remove animate class after the animation duration
        }, 1000);
    }

    function rotateItems() {
        // Check if the mouse is over the carousel
        const isMouseOverCarousel = carousel.matches(':hover');
        if (!isMouseOverCarousel) {
            currentIndex = (currentIndex + 1) % carouselItems.length;
            showItem(currentIndex);
        }
    }

    setInterval(rotateItems, 3000);

    showItem(currentIndex);
}
/////////////////for غير المتاحة//////////////////////////////
function initCarousel3() {
    const carousel = document.getElementById('carouselItem4');
    const carouselItems = carousel.getElementsByClassName('carousel-item');
    let currentIndex = 0;

    function showItem(index) {
        for (let i = 0; i < carouselItems.length; i++) {   //// carouselItems.length is the number of items in the list you will display in the carsouel you can replace it with their number //////////// 
            carouselItems[i].classList.remove('active');
        }
        carouselItems[index].classList.add('active');
        carouselItems[index].classList.add('animate'); // Add animate class for animation
        setTimeout(() => {
            carouselItems[index].classList.remove('animate'); // Remove animate class after the animation duration
        }, 1000);
    }

    function rotateItems() {
        // Check if the mouse is over the carousel
        const isMouseOverCarousel = carousel.matches(':hover');
        if (!isMouseOverCarousel) {
            currentIndex = (currentIndex + 1) % carouselItems.length;
            showItem(currentIndex);
        }
    }

    setInterval(rotateItems, 3000);

    showItem(currentIndex);
}