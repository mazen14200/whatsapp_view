////////////////////////////show main carouse items by bottons ( etc ,مؤجرة , احصائيات)////////////////////////////
function showCarouselItem(itemNumber) {
  const carouselItems = document.getElementsByClassName('main-carousel-item');
  for (let i = 0; i < carouselItems.length; i++) {
    carouselItems[i].classList.remove('active');
  }

  const carouselItem = document.getElementById(`carouselItem${itemNumber}`);
  carouselItem.classList.add('active');
}

