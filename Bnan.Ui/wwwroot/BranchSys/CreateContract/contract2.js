jQuery(document).ready(function () {

    ImgUpload();

});
function HideFirstImg() {
    var firstImg = document.getElementById('upload-img1');
    firstImg.style.display = "none";
}
function ImgUpload() {
    var imgWrap = "";
    var imgArray = [];

    $('.upload__inputfile').each(function () {
        $(this).on('change', function (e) {
            imgWrap = $(this).closest('.upload__box').find('.upload_img-wrap_inner');
            var maxLength = 12

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);
            var uploadBtnBox = document.getElementById('upload__btn-box');
            if (imgArray.length + filesArr.length > maxLength) {
                uploadBtnBox.style.display = "none";
                return;
            }

            var iterator = 0;
            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    return;
                }

                imgArray.push(f);

                var reader = new FileReader();
                reader.onload = function (e) {
                    var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div class='upload__img-close'><img src='~/BranchSys/CreateContract/img/delete.png'></div></div></div>";
                    imgWrap.append(html);
                    iterator++;
                }
                reader.readAsDataURL(f);
            });
        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}

/////////////////////////////////////////////////////////////////////////search-icon-payment///////////////////////////////////////////////////////////////////
const imagePay = document.getElementById('payment-extra-details');
const dropdownPay = document.getElementById('dropdown-content-payment');

imagePay.addEventListener('click', function () {
    if (dropdownPay.style.display === 'block') {
        dropdownPay.style.display = 'none';
    } else {
        dropdownPay.style.display = 'block';
    }
});

const close = document.getElementById("close");
close.addEventListener('click', function () {
    if (dropdownPay.style.display === 'block') {
        dropdownPay.style.display = 'none';
    }
})