////////////////////////////المؤجرة////////////////////////////
$(document).ready(function () {
    $('#toggleCarousel').click(function () {

        $('#chart1').hide();
        $('#chart2').hide();
        $('#chart3').hide();

        $('#cardBody').html('<div id="carousel" class="carousel">' + generateCarouselHTML() + '</div>');

        initCarousel();
    });
    ////////المتاحة
    $('#toggleCarousel2').click(function () {
        $('#chart1').hide();
        $('#chart2').hide();
        $('#chart3').hide();

        $('#cardBody').html('<div id="carousel2" class="carousel">' + generateCarouselHTML2() + '</div>');
        initCarousel2();
    });
    //////الغيرمتاحة
    $('#toggleCarousel3').click(function () {
        $('#chart1').hide();
        $('#chart2').hide();
        $('#chart3').hide();

        $('#cardBody').html('<div id="carousel3" class="carousel">' + generateCarouselHTML3() + '</div>');
        initCarousel3();
    });



});

// المؤجرة////////////////////
const products = [
    { type: "هوندا", color: "ابيض", year: "۲۰۲۱", image: "/BranchSys/img/car1.png", number: "ث ب ك 954", doors: "٤", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", contract_number: "۹۸۸٦۲۳٤٥۰۲۲۳", tenant: "أحمد إبراهيم عبدالله", decade_Beginning: "۲۰۲۳/۳/۱۲", decade_end: "۲۰۲٤/۱/۱۲" },
    { type: "كيا", color: "ازق", year: "۲۰۲۲", image: "/BranchSys/img/car2.png", number: "ت ا ك 954", doors: "٥", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", contract_number: "۹۸۸٦۲۳٤٥۰۲۲۳", tenant: "أحمد عبدالله", decade_Beginning: "۲۰۲۳/۳/۱۲", decade_end: "۲۰۲٤/۱/۱۲" },
    { type: "هوندا", color: "ابيض", year: "۲۰۲۱", image: "/BranchSys/img/car3.png", number: "ع ب ك 954", doors: "٤", people: "۲", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", contract_number: "۹۸۸٦۲۳٤٥۰۲۲۳", tenant: " إبراهيم أحمد عبدالله", decade_Beginning: "۲۰۲۳/۳/۱۲", decade_end: "۲۰۲٤/۱/۱۲" },
    { type: "كيا", color: "احمر", year: "۲۰۲۱", image: "/BranchSys/img/car4.png", number: "ث ب م 954", doors: "۲", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", contract_number: "۹۸۸٦۲۳٤٥۰۲۲۳", tenant: "علي محمد علي ", decade_Beginning: "۲۰۲۳/۳/۱۲", decade_end: "۲۰۲٤/۱/۱۲" },

];    
// Function to generate carousel HTML for the first carousel
function generateCarouselHTML() {
    let carouselHTML = '';
    for (let i = 0; i < products.length; i++) {
        const product = products[i];
        carouselHTML += `
        <div class="carousel-item ${i === 0 ? 'active' : ''}">
          <div class="product">
            <div class="row" id="rented_data1">
            <div class="col-md-7 col-lg-7">
            <div class="row product-data2">
                <div class="col-auto">
                    <img src="${product.transimission}">
                </div>
                <div class="col-auto">
                    <img src="${product.gas}">
        
                </div>
                <div class="col-auto">
                    ${product.lbag} <img src="/BranchSys/img/lbag2.png">
        
                </div>
                <div class="col-auto">
                    ${product.sbag} <img src="/BranchSys/img/sbag2.png">
        
                </div>
                <div class="col-auto">
                    ${product.people}<img src="/BranchSys/img/people2.png">
        
                </div>
                <div class="col-auto">
                    ${product.doors} <img src="/BranchSys/img/doors2.png">
        
                </div>
            </div>
        </div>
              <div class="col-md-5 col-lg-5">
                <p class="product-data1">${product.type} - ${product.color} - ${product.year} - ${product.number}</p>
              </div>
            </div>
            <div class="row car_img">
              <img class="product-image" src="${product.image}" alt="${product.name}">
            </div>
            <div class="row" id="rented_data2">
            <div class="col">
            <div class="row">
                <div class="col">
                    <div class="collapse" id="collapse-price-${i}">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p class="rented-p">  نهاية العقد</p>
                                <p>${product.decade_end}</p>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p  class="rented-p"> بداية العقد</p>
                                <p>${product.decade_Beginning}</p>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p  class="rented-p">المستأجر</p>
                                <p>${product.tenant}</p>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p  class="rented-p">رقم العقد</p>
                                <p>${product.contract_number}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapse-price-${i}"
                    aria-expanded="false" aria-controls="collapse-price-${i}" >
                    <img src="/BranchSys/img/vector.png">
                    مختصر العقد
                </button>
            </div>
            </div>
              </div>
            </div>
          </div>
        </div>
      `;
    }
    return carouselHTML;
}
function initCarousel() {
    const carousel = document.getElementById('carousel');
    carousel.innerHTML = generateCarouselHTML();

    const carouselItems = carousel.getElementsByClassName('carousel-item');
    let currentIndex = 0;

    function showItem(index) {
        for (let i = 0; i < carouselItems.length; i++) {
            carouselItems[i].classList.remove('active');
        }
        carouselItems[index].classList.add('active');
    }

    function rotateItems() {
        // Check if the mouse is over the carousel
        const isMouseOverCarousel = carousel.matches(':hover');
        if (!isMouseOverCarousel) {
            currentIndex = (currentIndex + 1) % carouselItems.length;
            showItem(currentIndex);
        }
    }

    setInterval(rotateItems, 2000);

    showItem(currentIndex);
}

// المتاحة////////////////////////

const products2 = [
    { type: "نيسان", color: "أسود", year: "۲۰۲۳", image: "/BranchSys/img/car2.png", number: "ث ب ك 123", doors: "٤", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", day_price: "۱۳۰,۰۰", week_price: "۱۱۰,۰۰", month_price: "۸۰,۰۰", extra_kilos: "۰,۱۰", extra_hours: "۱٥,۰۰", free_kilo: "۲۰۰", free_hours: "۱", extra_km: "۲٥۰", age: "٦۰ :: ۲۱",Rear_brake:"علي وشك الانتهاء",Front_brakes:"قائم",Periodic_maintenance:"منتهي", oil:"قائم",tires:"قائم" ,licence :"قائم",Insurance_document:"على وشك الانتهاء" ,operating_card:"منتهي" ,Periodic_inspection:"قائم"},
    { type: "فورد", color: "أسود", year: "۲۰۲۳", image: "/BranchSys/img/car1.png", number: "ث ب ك 123", doors: "٤", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", day_price: "۱۳۰,۰۰", week_price: "۱۱۰,۰۰", month_price: "۸۰,۰۰", extra_kilos: "۰,۱۰", extra_hours: "۱٥,۰۰", free_kilo: "۲۰۰", free_hours: "۱", extra_km: "۲٥۰", age: "٦۰ :: ۲۱" ,Rear_brake:"قائم",Front_brakes:"قائم",Periodic_maintenance:"قائم", oil:"قائم",tires:"قائم",licence :"قائم",Insurance_document:"على وشك الانتهاء" ,operating_card:"منتهي" ,Periodic_inspection:"قائم"},
    { type: "فورد", color: "أبيض", year: "۲۰۲۱", image: "/BranchSys/img/car2.png", number: "ع ب ك 789", doors: "٤", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", day_price: "55", week_price: "55", month_price: "55", extra_kilos: "55", extra_hours: "15", free_kilo: "3۰۰", free_hours: "5", extra_km: "300", age: "9۰ :: ۲۱" ,Rear_brake:"علي وشك الانتهاء",Front_brakes:"قائم",Periodic_maintenance:"منتهي", oil:"قائم",tires:"قائم",licence :"قائم",Insurance_document:"قائم" ,operating_card:"قائم" ,Periodic_inspection:"قائم"},
    { type: "شيفروليه", color: "أحمر", year: "۲۰۲۱", image: "/BranchSys/img/car4.png", number: "ث ب م 012", doors: "۲", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", day_price: "55", week_price: "55", month_price: "55", extra_kilos: "55", extra_hours: "15", free_kilo: "400", free_hours: "6", extra_km: "500", age: "٦۰ :: 30" ,Rear_brake:"قائم",Front_brakes:"قائم",Periodic_maintenance:"منتهي", oil:"قائم",tires:"قائم",licence :"قائم",Insurance_document:"قائم" ,operating_card:"قائم" ,Periodic_inspection:"قائم"},
];

function generateCarouselHTML2() {
    let carouselHTML2 = '';
    for (let i = 0; i < products2.length; i++) {
        const product = products2[i];
        carouselHTML2 += `

        <div class="carousel-item ${i === 0 ? 'active' : ''}">
        <div class="product">
            <div class="row" id="available_data1">
                <div class="col-md-7 col-lg-7">
                <div class="row product-data2">
                <div class="col-auto">
                    <img src="${product.transimission}">
                </div>
                <div class="col-auto">
                    <img src="${product.gas}">
        
                </div>
                <div class="col-auto">
                    ${product.lbag} <img src="/BranchSys/img/lbag2.png">
        
                </div>
                <div class="col-auto">
                    ${product.sbag} <img src="/BranchSys/img/sbag2.png">
        
                </div>
                <div class="col-auto">
                    ${product.people}<img src="/BranchSys/img/people2.png">
        
                </div>
                <div class="col-auto">
                    ${product.doors} <img src="/BranchSys/img/doors2.png">
        
                </div>
            </div>
                </div>
                <div class="col-md-5 col-lg-5">
                    <p class="product-data1">${product.type} - ${product.color} - ${product.year} - ${product.number}</p>
                </div>
            </div>
            <div class="row car_img img-car-avaliable">
                <img class="product-image" src="${product.image}" alt="${product.name}">
            </div>
            <div class="row" id="available_data2">
                <div class="col">
                    <div class="row">
                        <div class="col">
                            <div class="collapse" id="collapse-price-${i}">
                                <div class="row">
                                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3 ">
                                        <p> الساعات الاضافية</p>
                                        <p>${product.extra_hours}</p>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                        <p> الكيلو الاضافي</p>
                                        <p>${product.extra_kilos}</p>
                                    </div>
                                    <div class="col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                        <p> شهري</p>
                                        <p>${product.month_price}</p>
                                    </div>
                                    <div class="col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                        <p>اسبوعي</p>
                                        <p>${product.week_price}</p>
                                    </div>
                                    <div class="col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                        <p>يومي</p>
                                        <p>${product.day_price}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <button class="btn btn-link  pb-0" type="button" data-toggle="collapse" data-target="#collapse-price-${i}"
                            aria-expanded="false" aria-controls="collapse-price-${i}" >
                            <img src="/BranchSys/img/vector.png">
                            السعر
                        </button>
                    </div>
                    <div class="row">
                        <div class="col">
                            <div class="collapse" id="collapse-info-${i}">
                                <div class="row">
                                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                        <p>العمر</p>
                                        <p>${product.age}</p>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                        <p>كم اضافية </p>
                                        <p>${product.extra_km}</p>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                        <p> ساعات مجانية</p>
                                        <p>${product.free_hours}</p>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                        <p>كيلو مجاني</p>
                                        <p>${product.free_kilo} كم </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <button class="btn btn-link pb-0" type="button" data-toggle="collapse" data-target="#collapse-info-${i}"
                            aria-expanded="false" aria-controls="collapse-info-${i}" >
                            <img src="/BranchSys/img/vector.png">
                            بنود التأجير
                        </button>
                    </div>
                    <div class="row ">
                    <div class="col">
                        <div class="collapse" id="collapse-fixing-${i}">
                            <div class="row maintenance">
                                <div class="class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                    <p>الفرامل الخلفية</p>
                                    <p class="${product.Rear_brake === 'منتهي' || product.Rear_brake === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.Rear_brake}</p>
                                </div>
                                <div class="class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                    <p>الفرامل الأمامية</p>
                                    <p class="${product.Front_brakes === 'منتهي' || product.Front_brakes === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.Front_brakes}</p>
                                </div>
                                <div class="class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                    <p>الصيانة الدورية</p>
                                    <p class="${product.Periodic_maintenance === 'منتهي' || product.Periodic_maintenance === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.Periodic_maintenance}</p>
                                </div>
                                <div class="class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                    <p>الزيت</p>
                                    <p class="${product.oil === 'منتهي' || product.oil === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.oil}</p>
                                </div>
                                <div class="class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                    <p>الإطارات</p>
                                    <p class="${product.tires === 'منتهي' || product.tires === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.tires}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <button class="btn btn-link pb-0 ${product.Rear_brake === 'منتهي' || product.Rear_brake === 'علي وشك الانتهاء' || product.Front_brakes === 'منتهي' || product.Front_brakes === 'علي وشك الانتهاء' || product.Periodic_maintenance === 'منتهي' || product.Periodic_maintenance === 'علي وشك الانتهاء' || product.oil === 'منتهي' || product.oil === 'علي وشك الانتهاء' || product.tires === 'منتهي' || product.tires === 'علي وشك الانتهاء' ? 'red-button' : ''}" type="button" data-toggle="collapse" data-target="#collapse-fixing-${i}" aria-expanded="false" aria-controls="collapse-fixing-${i}">
                        <img src="/BranchSys/img/vector.png">
                        الصيانة
                    </button>
                </div>
                <div class="row">
                <div class="col">
                    <div class="collapse" id="collapse-doc-${i}">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p>الفحص الدوري</p>
                                <p class="${product.Periodic_inspection === 'منتهي' || product.Periodic_inspection === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.Periodic_inspection}</p>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p>بطاقة تشغيل</p>
                                <p class="${product.operating_card === 'منتهي' || product.operating_card === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.operating_card}</p>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p>وثيقة التأمين</p>
                                <p class="${product.Insurance_document === 'منتهي' || product.Insurance_document === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.Insurance_document}</p>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <p>رخصة السير</p>
                                <p class="${product.licence === 'منتهي' || product.licence === 'علي وشك الانتهاء' ? 'red-text' : ''}">${product.licence}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <button class="btn btn-link pb-0 ${product.Periodic_inspection === 'منتهي' || product.Periodic_inspection === 'علي وشك الانتهاء' || product.operating_card === 'منتهي' || product.operating_card === 'علي وشك الانتهاء' || product.Insurance_document === 'منتهي' || product.Insurance_document === 'علي وشك الانتهاء' || product.licence === 'منتهي' || product.licence === 'علي وشك الانتهاء' ? 'red-button' : ''}" type="button" data-toggle="collapse" data-target="#collapse-doc-${i}" aria-expanded="false" aria-controls="collapse-doc-${i}">
                    <img src="/BranchSys/img/vector.png">
                    الوثائق
                </button>
            </div>
                </div>
            </div>
    
        </div>
    </div>
`;
    }
    return carouselHTML2;
}


// Function to initialize the second carousel
function initCarousel2() {
    const carousel2 = document.getElementById('carousel2');
    carousel2.innerHTML = generateCarouselHTML2();

    const carouselItems2 = carousel2.getElementsByClassName('carousel-item');
    let currentIndex2 = 0;

    function showItem(index2) {
        for (let i = 0; i < carouselItems2.length; i++) {
            carouselItems2[i].classList.remove('active');
        }
        carouselItems2[index2].classList.add('active');
    }

    function rotateItems() {
        // Check if the mouse is over the carousel
        const isMouseOverCarousel = carousel2.matches(':hover');
        if (!isMouseOverCarousel) {
            currentIndex2 = (currentIndex2 + 1) % carouselItems2.length;
            showItem(currentIndex2);
        }
    }

    setInterval(rotateItems, 2000); // Auto-rotate every 5 seconds

    showItem(currentIndex2);
    const collapseButtons = carousel2.querySelectorAll('[data-toggle="collapse"]');
    collapseButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            const target = document.querySelector(button.dataset.target);
            const isExpanded = target.classList.contains('show');
            if (!isExpanded) {
                const openCollapseElements = carousel2.querySelectorAll('.collapse.show');
                openCollapseElements.forEach(function (collapse) {
                    if (collapse !== target) {
                        collapse.classList.remove('show');
                    }
                });
            }
        });
    });
}

///////////////////////////////


const products3 = [
    { type: "هوندا", color: "ابيض", year: "۲۰۲۱", image: "/BranchSys/img/car1.png", number: "ث ب ك 954", doors: "٤", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", reasons: [" عدم  توافر السعر", "  السيارة  بالصيانة"] },
    { type: "كيا", color: "ازق", year: "۲۰۲۲", image: "/BranchSys/img/car2.png", number: "ت ا ك 954", doors: "٥", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", reasons: [" المالك  موقوف "] },
    { type: "هوندا", color: "ابيض", year: "۲۰۲۱", image: "/BranchSys/img/car3.png", number: "ع ب ك 954", doors: "٤", people: "۲", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", reasons: [" الفرع  موقوف"] },
    { type: "كيا", color: "احمر", year: "۲۰۲۱", image: "/BranchSys/img/car4.png", number: "ث ب م 954", doors: "۲", people: "٥", sbag: "۱", lbag: "۲", gas: "/BranchSys/img/gas1.png", transimission: "/BranchSys/img/transimission2.png", reasons: ["  عدم  توافر السعر", " السيارة  بالصيانة", " الفرع موقوف"] },

];
function generateCarouselHTML3() {
    let carouselHTML3 = '';
    for (let i = 0; i < products3.length; i++) {
        const product = products3[i];
        carouselHTML3 += `
        <div class="carousel-item ${i === 0 ? 'active' : ''}">
          <div class="product">
            <div class="row" id="not_available_data1">
              <div class="col-md-7 col-lg-7">
              <div class="row product-data2">
              <div class="col-auto">
                  <img src="${product.transimission}">
              </div>
              <div class="col-auto">
                  <img src="${product.gas}">
      
              </div>
              <div class="col-auto">
                  ${product.lbag} <img src="/BranchSys/img/lbag2.png">
      
              </div>
              <div class="col-auto">
                  ${product.sbag} <img src="/BranchSys/img/sbag2.png">
      
              </div>
              <div class="col-auto">
                  ${product.people}<img src="/BranchSys/img/people2.png">
      
              </div>
              <div class="col-auto">
                  ${product.doors} <img src="/BranchSys/img/doors2.png">
      
              </div>
          </div>
              </div>
              <div class="col-md-5 col-lg-5">
                <p class="product-data1">${product.type} - ${product.color} - ${product.year} - ${product.number}</p>
              </div>
            </div>
            <div class="row car_img">
              <img class="product-image" src="${product.image}" alt="${product.name}">
            </div>
            <div class="row" id="not_available_data2">
              <div class="col">
                <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapse-${i}" aria-expanded="false" aria-controls="collapse-${i}">
                  <img src="/BranchSys/img/vector.png">
                  الاسباب
                 </button>
                 <div class="collapse" id="collapse-${i}">
                  <div class="row">
                    <div class="col">
                      <p class="product-reasons" >${product.reasons}
                      </p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      `;
    }
    return carouselHTML3;
}

function initCarousel3() {
    const carousel3 = document.getElementById('carousel3');
    carousel3.innerHTML = generateCarouselHTML3();

    const carouselItems3 = carousel3.getElementsByClassName('carousel-item');
    let currentIndex3 = 0;

    function showItem(index3) {
        for (let i = 0; i < carouselItems3.length; i++) {
            carouselItems3[i].classList.remove('active');
        }
        carouselItems3[index3].classList.add('active');
    }

    function rotateItems() {
        // Check if the mouse is over the carousel
        const isMouseOverCarousel = carousel3.matches(':hover');
        if (!isMouseOverCarousel) {
            currentIndex3 = (currentIndex3 + 1) % carouselItems3.length;
            showItem(currentIndex3);
        }
    }

    setInterval(rotateItems, 2000);

    showItem(currentIndex3);
}

