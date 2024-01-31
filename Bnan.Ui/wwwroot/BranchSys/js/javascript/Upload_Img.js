//const signature_content  = document.getElementById("signature_content ")
//const Img_signture = document.getElementById("Img_signture")
//const written_signture = document.getElementById("written_signture")
//const signture_img = document.getElementById("signture_img");

//var imageRemoved = false;



//function previewFile() {
//  var preview = document.querySelector('#upload_icon');
//  var file = document.querySelector('input[type=file]').files[0];
//  var reader = new FileReader();
//  var Profile = document.querySelector('#User_Profile_img');


//  reader.addEventListener("load", function () {
//      preview.src = reader.result;
//      Profile.src = reader.result;

//      preview.style.borderRadius = '50%';
//      preview.style.width = '100%';
//      preview.style.height = '100%';
//      imageRemoved = false;
//  }, false);

//  if (file) {
//      reader.readAsDataURL(file);
//  }
//}

//document.addEventListener('DOMContentLoaded', function () {
//  var uploadIcon = document.getElementById('upload_icon');
//  var profileImageUpload = document.getElementById('profile-image-upload');

//  uploadIcon.addEventListener('click', function () {
//      profileImageUpload.click();
//  });
//});

//document.addEventListener('DOMContentLoaded', function () {
//  var removeProfileIcon = document.querySelector('.remove_profile');
//  removeProfileIcon.addEventListener('click', function () {
//      var preview = document.querySelector('#upload_icon');
//      var Profile = document.querySelector('#User_Profile_img');

//      preview.src = 'img/Img_load_box_fill.svg';
//      Profile.src="img/User 07a.svg";
//      preview.style.borderRadius = '0%';
//      preview.style.width = 'fit-content';
//      preview.style.height = 'fit-content';
//      imageRemoved = true; 
//  });
//});

//document.querySelector('#profile_form').addEventListener('submit', function (e) {
//  if (imageRemoved) {
//    e.preventDefault();
//    document.querySelector('#profile-image-upload').value = null;
//    document.querySelector('form').submit();
//  }
//});

//// ////////////////////////////////////////////////////////////////////////////////////////////
//function canvasBehavoiur() {
//  signature_content.innerHTML += `
//    <canvas id="canvas" class="canvas_signture"  height="100"></canvas>
//  `;
//  var canvas = document.getElementById('canvas');
//  var ctx = canvas.getContext('2d');
//  canvas.style.width = '100%';

//  // Variables for drawing
//  var drawing = false;
//  var prevX = 0;
//  var prevY = 0;
//  var currX = 0;
//  var currY = 0;

//  // Draw a line between the previous point and the current point
//  function drawLine(x0, y0, x1, y1) {
//    ctx.beginPath();
//    ctx.moveTo(x0, y0);
//    ctx.lineTo(x1, y1);
//    ctx.stroke();
//    ctx.closePath();
//  }

//  // Add event listeners to handle mouse events
//  canvas.addEventListener('mousedown', function (e) {
//    drawing = true;
//    prevX = e.clientX - canvas.getBoundingClientRect().left;
//    prevY = e.clientY - canvas.getBoundingClientRect().top;
//  });

//  canvas.addEventListener('mousemove', function (e) {
//    if (!drawing) return;
//    currX = e.clientX - canvas.getBoundingClientRect().left;
//    currY = e.clientY - canvas.getBoundingClientRect().top;
//    drawLine(prevX, prevY, currX, currY);
//    prevX = currX;
//    prevY = currY;
//    previewCanvas();
//  });

//  canvas.addEventListener('mouseup', function () {
//    drawing = false;
//  });
 
//}

//// Preview the canvas image
//function previewCanvas() {
//  var canvas = document.getElementById('canvas');
//  var dataURL = canvas.toDataURL();

//  var previewContainer = document.getElementById('signture_img');
//  var image = document.createElement('img');
//  image.src = dataURL;
//  image.style.width = '100px';

//  previewContainer.innerHTML = '';
//  previewContainer.appendChild(image);
//}

//// Upload the canvas image and submit it
//function uploadCanvas() {
//  var canvas = document.getElementById('canvas');
//  var dataURL = canvas.toDataURL();

//  // Create a form data object
//  var formData = new FormData();
//  formData.append('signatureImage', dataURL);

//  // Submit the form data to the server
//  var xhr = new XMLHttpRequest();
//  xhr.open('POST', '/upload', true);   // replace  /upload with the real server link

//  xhr.onreadystatechange = function () {
//    if (xhr.readyState === 4 && xhr.status === 200) {
//      // Handle the server response
//      console.log(xhr.responseText);
//    }
//  };
//  xhr.send(formData);
//}

//document.getElementById('save').addEventListener('click', function () {
//  uploadCanvas();
//});

//// /////////////////////////////////////////////////////////////////////////////////////
//Img_signture.addEventListener("click", function() {
//  written_signture.classList.remove("activee");
//  Img_signture.classList.add("activee");
//  signature_content.innerHTML = ``;
//  signature_content.innerHTML += `
//    <div class="upload-btn-wrapper">
//      <svg xmlns="http://www.w3.org/2000/svg" width="59" height="59" viewBox="0 0 59 59" fill="none">
//        <path d="M29.3337 44.3912C27.6715 44.3912 26.4004 43.1201 26.4004 41.4579V17.1112C26.4004 15.449 27.6715 14.1779 29.3337 14.1779C30.9959 14.1779 32.2671 15.449 32.2671 17.1112V41.4579C32.2671 43.1201 30.9959 44.3912 29.3337 44.3912Z" fill="#39629C"/>
//        <path d="M41.5554 32.2667C40.7732 32.2667 40.0887 31.9734 39.5021 31.3867L29.3332 21.3156L19.2621 31.3867C18.0887 32.5601 16.2309 32.5601 15.1554 31.3867C13.9821 30.2134 13.9821 28.3556 15.1554 27.2801L27.3776 15.0579C28.5509 13.8845 30.4087 13.8845 31.4843 15.0579L43.7065 27.2801C44.8798 28.4534 44.8798 30.3112 43.7065 31.3867C43.0221 31.9734 42.2398 32.2667 41.5554 32.2667Z" fill="#39629C"/>
//        <path d="M29.3333 58.6667C13.2 58.6667 0 45.4667 0 29.3333C0 13.2 13.2 0 29.3333 0C45.4667 0 58.6667 13.2 58.6667 29.3333C58.6667 45.4667 45.4667 58.6667 29.3333 58.6667ZM29.3333 5.86667C16.4267 5.86667 5.86667 16.4267 5.86667 29.3333C5.86667 42.24 16.4267 52.8 29.3333 52.8C42.24 52.8 52.8 42.24 52.8 29.3333C52.8 16.4267 42.24 5.86667 29.3333 5.86667Z" fill="#39629C"/>
//      </svg>               
//      <input type="file" name="mySignature" id="signature-file" />
//    </div>
//  `;
//  const fileInput = document.getElementById("signature-file");

//  fileInput.addEventListener("change", function() {
//    const file = fileInput.files[0];
//    const reader = new FileReader();

//    reader.onload = function() {
//      const image = document.createElement("img");
//      image.src = reader.result;
//      image.style.width = '95px';

//      signture_img.innerHTML =``;
//      signture_img.appendChild(image);
//    };

//    if (file) {
//      reader.readAsDataURL(file);
//    }
//  });
//});


//written_signture.addEventListener("click" ,function(){
//   written_signture.classList.add("activee");
//   Img_signture.classList.remove("activee"); 
//   signature_content.innerHTML=``
//  canvasBehavoiur()
//})
//canvasBehavoiur()
