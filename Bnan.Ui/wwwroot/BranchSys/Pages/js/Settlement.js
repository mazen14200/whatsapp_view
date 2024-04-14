
let current_fs, next_fs, previous_fs;
const ExpensesCheckbox = document.getElementById('expenses');
const CompensationCheckbox = document.getElementById('compensation-check');
var compensationArray = [];
var ExpensesArray= [];

jQuery(document).ready(function () {

    ExpensesImgUpload();
    compensationImgUpload();
    examinationImgUpload();
});

//=======================================â€™Moving Between Fieldsets=============================================================

//(() => {
//    "use strict";

//    // Assuming your "next" button has a class of "next"
//    const nextButtons = document.querySelectorAll(".next");
//    const ExpensesCheckbox = document.getElementById('expenses');
//    const CompensationCheckbox = document.getElementById('compensation-check');
//    const firsetfield = document.getElementById('firstFieldset');
//    const fieldsets = document.querySelectorAll("fieldset");


//    // Function to hide error message
//    //const hideErrorMessage = (field) => {
//    //    const errorMessage = field.parentNode.querySelector(".invalid-feedback");
//    //    if (errorMessage) {
//    //        errorMessage.style.display = "none";
//    //    }
//    //};


//    Array.from(nextButtons).forEach((nextBtn) => {
//        nextBtn.addEventListener("click", (event) => {
//            const fieldset = nextBtn.closest("fieldset");
//            let isValid = true;
//            Array.from(fieldset.elements).forEach((field) => {
//                //if (field.checkValidity() === false) {
//                //    isValid = false;
//                //    const errorMessage = field.parentNode.querySelector(".invalid-feedback");
//                //    if (errorMessage) {
//                //        errorMessage.style.display = "block";
//                //    }
//                //}
//            });


//            // Add input event listeners to all input fields within the fieldsets
//            fieldsets.forEach((fieldset) => {
//                Array.from(fieldset.elements).forEach((field) => {
//                    //field.addEventListener('input', () => {
//                    //    hideErrorMessage(field);
//                    //});
//                });
//            });
//            if (!isValid) {
//                event.preventDefault();
//                event.stopPropagation();

//            } else {

//                const current_fs = nextBtn.closest("fieldset");
//                let next_fs = current_fs.nextElementSibling;


//                if (current_fs.id === 'firstFieldset') {

//                    if (!ExpensesCheckbox.checked && CompensationCheckbox.checked) {
//                        next_fs = current_fs.nextElementSibling.nextElementSibling;
//                        $("#progressbar li").eq($("fieldset").index(current_fs.nextElementSibling)).addClass("active");

//                    } else if (!ExpensesCheckbox.checked && !CompensationCheckbox.checked) {
//                        next_fs = current_fs.nextElementSibling.nextElementSibling.nextElementSibling;
//                        $("#progressbar li").eq($("fieldset").index(current_fs.nextElementSibling)).addClass("active");
//                        $("#progressbar li").eq($("fieldset").index(current_fs.nextElementSibling.nextElementSibling)).addClass("active");


//                    }
//                    else {
//                        next_fs = current_fs.nextElementSibling;

//                    }
//                }

//                if (current_fs.id === 'SecondFieldset') {

//                    if (ExpensesCheckbox.checked && !CompensationCheckbox.checked) {
//                        next_fs = current_fs.nextElementSibling.nextElementSibling;
//                        $("#progressbar li").eq($("fieldset").index(current_fs.nextElementSibling)).addClass("active");

//                    }
//                }

//                Array.from(next_fs.elements).forEach((field) => {
//                    //field.addEventListener('input', () => {
//                    //    hideErrorMessage(field);
//                    //});
//                });


//                $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

//                next_fs.style.display = "flex";
//                current_fs.style.display = "none";
//            }
//        });
//    });

//    const previousButtons = document.querySelectorAll(".previous");

//    Array.from(previousButtons).forEach((prevBtn) => {
//        prevBtn.addEventListener("click", (event) => {
//            const current_fs = prevBtn.closest("fieldset");
//            let previous_fs = current_fs.previousElementSibling;
//            if (current_fs.id === 'FourthFieldset') {

//                if (ExpensesCheckbox.checked && CompensationCheckbox.checked) {
//                    previous_fs = current_fs.previousElementSibling;
//                } else if (!ExpensesCheckbox.checked && !CompensationCheckbox.checked) {
//                    previous_fs = current_fs.previousElementSibling.previousElementSibling.previousElementSibling;
//                    $("#progressbar li").eq($("fieldset").index(current_fs.previousElementSibling)).removeClass("active");
//                    $("#progressbar li").eq($("fieldset").index(current_fs.previousElementSibling.previousElementSibling)).removeClass("active");
//                } else if (ExpensesCheckbox.checked && !CompensationCheckbox.checked) {
//                    previous_fs = current_fs.previousElementSibling.previousElementSibling;
//                    $("#progressbar li").eq($("fieldset").index(current_fs.previousElementSibling)).removeClass("active");

//                }

//            }

//            if (current_fs.id === 'ThirdFieldset') {

//                if (!ExpensesCheckbox.checked && CompensationCheckbox.checked) {
//                    previous_fs = current_fs.previousElementSibling.previousElementSibling;
//                    $("#progressbar li").eq($("fieldset").index(current_fs.previousElementSibling)).removeClass("active");

//                }
//            }
//            Array.from(previous_fs.elements).forEach((field) => {
//                //field.addEventListener('input', () => {
//                //    hideErrorMessage(field);
//                //});
//            });
//            $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

//            previous_fs.style.display = "flex";
//            current_fs.style.display = "none";
//        });
//    });
//})();

//====================================================================================================
//========================================Upload Expenses Imgs Lists============================================================
//====================================================================================================
function ExpensesImgUpload() {
    var imgWrap = '';
    var uploadBtnBox = document.getElementById('Expenses-images');
    var errorMessageDiv = document.getElementById('ExpensesError');
    $('#Expenses-images').each(function () {
        $(this).on('change', function (e) {
            imgWrap = $(this).closest('.upload__box').find('.upload_img-wrap_inner');
            var maxLength = 5;

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);


            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    var html =
                        "<div class='upload__img-box'><div style='background-image: url(" +
                        e.target.result +
                        ")' data-number='" +
                        $('.upload__img-close1').length +
                        "' data-file='" +
                        f.name +
                        "' class='img-bg'><div class='upload__img-close1'><img src='/BranchSys/Pages/img/delete.png'></div></div></div>";
                    imgWrap.append(html);
                    ExpensesArray.push({
                        f, url: e.target.result
                    }
                    );
                    console.log(ExpensesArray);

                };
                reader.readAsDataURL(f);
            });
        });
    });

    $('body').on('click', '.upload__img-close1', function (e) {
        e.stopPropagation();
        var file = $(this).parent().data('file');
        for (var i = 0; i < ExpensesArray.length; i++) {
            if (ExpensesArray[i].f.name == file) {
                ExpensesArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
        console.log(ExpensesArray);
        var maxLength = 6;


    });


}
//====================================================================================================
//========================================Upload compensation Imgs Lists============================================================
//====================================================================================================
function compensationImgUpload() {
    var imgWrap = '';
    var uploadBtnBox = document.getElementById('compensation-images');
    var errorMessageDiv = document.getElementById('compensationError');
    $('#compensation-images').each(function () {
        $(this).on('change', function (e) {
            imgWrap = $(this).closest('.upload__box').find('.upload_img-wrap_inner');
            var maxLength = 5;

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);



            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    var html =
                        "<div class='upload__img-box'><div style='background-image: url(" +
                        e.target.result +
                        ")' data-number='" +
                        $('.upload__img-close2').length +
                        "' data-file='" +
                        f.name +
                        "' class='img-bg'><div class='upload__img-close2'><img class='close2' src='/BranchSys/Pages/img/delete.png'></div></div></div>";
                    imgWrap.append(html);
                    compensationArray.push({
                        f, url: e.target.result
                    });
                    console.log(compensationArray);

                };
                reader.readAsDataURL(f);
            });
        });
    });

    $('body').on('click', '.upload__img-close2', function (e) {
        e.stopPropagation();
        var file = $(this).parent().data('file');


        for (var i = 0; i < compensationArray.length; i++) {
            if (compensationArray[i].f.name == file) {
                compensationArray.splice(i, 1);
                console.log(compensationArray);

                break;
            }
        }

        $(this).parent().parent().remove();
        var maxLength = 6;


    });



}
//====================================================================================================
//========================================Upload examination Imgs Lists============================================================
//====================================================================================================
function examinationImgUpload() {
    var imgWrap = '';
    var examinationArray = [];
    var uploadBtnBox = document.getElementById('examination-images');
    var errorMessageDiv = document.getElementById('examinationError');
    $('#examination-images').each(function () {
        $(this).on('change', function (e) {
            imgWrap = $(this).closest('.upload__box').find('.upload_img-wrap_inner');
            var maxLength = 11;

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);




            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    var html =
                        "<div class='upload__img-box'><div style='background-image: url(" +
                        e.target.result +
                        ")' data-number='" +
                        $('.upload__img-close3').length +
                        "' data-file='" +
                        f.name +
                        "' class='img-bg'><div class='upload__img-close3'><img class='close3' src='~/BranchSys/Pages/img/delete.png'></div></div></div>";
                    imgWrap.append(html);
                    examinationArray.push({
                        f, url: e.target.result
                    });
                    console.log(examinationArray);

                };
                reader.readAsDataURL(f);
            });
        });
    });

    $('body').on('click', '.upload__img-close3', function (e) {
        e.stopPropagation();
        var file = $(this).parent().data('file');


        for (var i = 0; i < examinationArray.length; i++) {
            if (examinationArray[i].f.name == file) {
                examinationArray.splice(i, 1);
                console.log(examinationArray);

                break;
            }
        }
        $(this).parent().parent().remove();
        var maxLength = 12;


    });



}
//====================================================================================================

$('body').on('click', '.img-bg', function (e) {
    var imageUrl = $(this).css('background-image');
    imageUrl = imageUrl.replace(/^url\(['"](.+)['"]\)/, '$1');
    var newTab = window.open();
    newTab.document.body.innerHTML = '<img src="' + imageUrl + '">';

    $(newTab.document.body).css({
        'background-color': 'black',
        display: 'flex',
        'align-items': 'center',
        'justify-content': 'center',
    });
});
//====================================================================================================
//====================================================================================================
const image = document.getElementById('hover-image-Settlement');
const dropdown = document.getElementById('dropdown-content-Settlement');

image.addEventListener('click', function () {
    if (dropdown.style.display === 'block') {
        dropdown.style.display = 'none';
    } else {
        dropdown.style.display = 'block';
        dropdown2.style.display = 'none';

    }
});
//====================================================================================================
//====================================================================================================
const image2 = document.getElementById('contract-value-Settlement2');
const dropdown2 = document.getElementById('dropdown-content-Settlement2');

image2.addEventListener('click', function () {
    if (dropdown2.style.display === 'block') {
        dropdown2.style.display = 'none';

    } else {
        dropdown2.style.display = 'block';
        dropdown.style.display = 'none';

    }
});
//====================================================================================================
//====================================================================================================
const image3 = document.getElementById('contract-value-Settlement3');
const dropdown3 = document.getElementById('dropdown-content-Settlement3');

image3.addEventListener('click', function () {
    if (dropdown3.style.display === 'block') {
        dropdown3.style.display = 'none';

    } else {
        dropdown3.style.display = 'block';
        dropdown4.style.display = 'none';

    }
});
//====================================================================================================
//====================================================================================================
const image4 = document.getElementById('contract-value-Settlement4');
const dropdown4 = document.getElementById('dropdown-content-Settlement4');

image4.addEventListener('click', function () {
    if (dropdown4.style.display === 'block') {
        dropdown4.style.display = 'none';

    } else {
        dropdown4.style.display = 'block';
        dropdown3.style.display = 'none';

    }
});
//====================================================================================================
$('#Expenses-images').click(function () {
    $('.upload__img-box').eq(0).hide();
    var x = $('.upload__img-box')
})
$('#compensation-images').click(function () {
    $('#FirstUpload-img2').hide()
})
$('#examination-images').click(function () {
    $('#FirstUpload-img3').hide()
})



