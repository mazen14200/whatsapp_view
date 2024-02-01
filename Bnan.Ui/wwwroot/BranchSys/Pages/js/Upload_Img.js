// =============
// == variables==============================================================================================================
// =============

const signture_img = document.getElementById("signture_img");
var profileImageUpload = document.getElementById('profile-image-upload');
var signature_img_input =document.getElementById('signature_img_input');
var svgContainer =document.getElementById('svg-container')
var imageRemoved = false;


// =============
// == Profile Photo ==============================================================================================================
// =============

function previewFile() {
  var file = document.querySelector('input[type=file]').files[0];
  var reader = new FileReader();
  var Profile = document.querySelector('#User_Profile_img');


  reader.addEventListener("load", function () {
      Profile.src = reader.result;
      imageRemoved = false;
  }, false);

  if (file) {
      reader.readAsDataURL(file);
  }
}

document.addEventListener('DOMContentLoaded', function () {
  var uploadIcon = document.getElementById('upload_icon');
  uploadIcon.addEventListener('click', function () {
      profileImageUpload.click();
  });
});

profileImageUpload.addEventListener('change' ,function(){
  previewFile()
})

// =============
// == signature Photo ==============================================================================================================
// =============
document.addEventListener('DOMContentLoaded', function () {
svgContainer.addEventListener('click' ,function(){
  signature_img_input.click()
})
});

signature_img_input.addEventListener('click' ,function(){
  const fileInput = document.getElementById("signature_img_input");

  fileInput.addEventListener("change", function() {
      const file = fileInput.files[0];
      const reader = new FileReader();
  
      reader.onload = function() {
        const image = document.createElement("img");
        image.src = reader.result;
        image.style.width = '95px';
  
        signture_img.innerHTML =``;
        signture_img.appendChild(image);
      };
  
      if (file) {
        reader.readAsDataURL(file);
      }
  })
  
})

