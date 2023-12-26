//var current_fs, next_fs, previous_fs;


//$(".next").click(function () {

//	current_fs = $(this).closest("fieldset");
//	next_fs = $(this).closest("fieldset").next();

//	// Activate next step on progressbar using the index of next_fs
//	$("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

//	// Show the next fieldset
//	next_fs.show();
//	// Hide the current fieldset with style

//	current_fs.hide();


//});

//$(".previous").click(function () {


//	current_fs = $(this).closest("fieldset");
//	previous_fs = $(this).closest("fieldset").prev();

//	//de-activate current step on progressbar
//	$("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

//	//show the previous fieldset
//	previous_fs.show();

//	current_fs.hide();

//});




/////////////////////////////////////////////////the-Modal-6-digit-vaildation/////////////////////
//function openThirdPopup() {
//	// Hide the second popup modal
//	$('#checkModalToggle').modal('hide');

//	// Open the third popup modal
//	$('#thirdPopupModal').modal('show');

//	// Close the third popup modal after 5 seconds
//	setTimeout(function () {
//		$('#thirdPopupModal').modal('hide');
//	}, 5000); // Adjust the duration as needed (in milliseconds)
//}

//document.addEventListener('DOMContentLoaded', function () {
//	document.querySelector('#otc').addEventListener('submit', function (event) {
//		event.preventDefault();
//		var inputFieldValue = document.getElementById('otc-1');
//		var numericValue = parseInt(inputFieldValue.value);

//		if (isNaN(numericValue)) {
//			// Input value is not a number or is empty
//			console.log('Input value:', numericValue);
//			return;
//		}
//		$.ajax({
//			type: 'POST',
//			url: 'https://jsonplaceholder.typicode.com/posts', // Using JSONPlaceholder as a mock server
//			data: $(this).serialize(),
//			success: function (response) {
//				// Handle the response from the mock server
//				console.log('Form data submitted successfully:', response);
//				openThirdPopup()

//			},
//			error: function (error) {
//				// Handle any errors
//				console.error('Error submitting form data:', error);
//			}
//		});
//		document.getElementById('otc-1').value = '';
//		document.getElementById('otc-2').value = '';
//		document.getElementById('otc-3').value = '';
//		document.getElementById('otc-4').value = '';
//		document.getElementById('otc-5').value = '';
//		document.getElementById('otc-6').value = '';
//	});

//});

let in1 = document.getElementById('otc-1'),
	ins = document.querySelectorAll('input[type="number"]'),
	splitNumber = function (e) {
		let data = e.data || e.target.value; // Chrome doesn't get the e.data, it's always empty, fallback to value then.
		if (!data) return; // Shouldn't happen, just in case.
		if (data.length === 1) return; // Here is a normal behavior, not a paste action.

		popuNext(e.target, data);
	},
	popuNext = function (el, data) {
		el.value = data[0]; // Apply first item to first input
		data = data.substring(1); // remove the first char.
		if (el.nextElementSibling && data.length) {
			// Do the same with the next element and next data
			popuNext(el.nextElementSibling, data);
		}
	};

ins.forEach(function (input) {
	/**
	 * Control on keyup to catch what the user intent to do.
	 ... */
	input.addEventListener('keyup', function (e) {
		// Break if Shift, Tab, CMD, Option, Control.
		if (e.keyCode === 16 || e.keyCode == 9 || e.keyCode == 224 || e.keyCode == 18 || e.keyCode == 17) {
			return;
		}

		// On Backspace or left arrow, go to the previous field.
		if ((e.keyCode === 8 || e.keyCode === 37) && this.previousElementSibling && this.previousElementSibling.tagName === "INPUT") {
			this.previousElementSibling.select();
		} else if (e.keyCode !== 8 && this.nextElementSibling) {
			this.nextElementSibling.select();
		}

		// If the target is populated too quickly, value length can be > 1
		if (e.target.value.length > 1) {
			splitNumber(e);
		}
	});

	input.addEventListener('focus', function (e) {
		if (this === in1) return;

		if (in1.value == '') {
			in1.focus();
		}
		if (this.previousElementSibling.value == '') {
			this.previousElementSibling.focus();
		}
	});
	const B = document.querySelector('.check-btn.check');

});
in1.addEventListener('input', splitNumber);


// // //////////////////////choose-adriver-display////////////////
//document.addEventListener("DOMContentLoaded", function () {
//	var driverRadio1 = document.getElementById("RenterIsdriver");
//	var driverRadio2 = document.getElementById("PrivateDriver");
//	var dropdownContainer = document.getElementById("dropdown-container");

//	driverRadio1.addEventListener("click", function () {
//		dropdownContainer.style.display = "none";
//	});

//	driverRadio2.addEventListener("click", function () {
//		if (this.checked) {
//			dropdownContainer.style.display = "block";
//		} else {
//			dropdownContainer.style.display = "none";
//		}
//	});
//});

//////////////////////////serch-icon-list-for Tenant////////////////////////
const image = document.getElementById('hover-image');
const dropdown = document.getElementById('dropdown-content');

image.addEventListener('mouseover', function () {
	dropdown.style.display = 'block';
});

image.addEventListener('mouseout', function () {
	dropdown.style.display = 'none';
});
///////////////serch-icon-list-for driver/////////////////////
const imageDriver = document.getElementById('hover-image-driver');
const dropdown2 = document.getElementById('dropdown-content-driver');

imageDriver.addEventListener('mouseover', function () {
	dropdown2.style.display = 'block';
});

imageDriver.addEventListener('mouseout', function () {
	dropdown2.style.display = 'none';
});
/////////////////serch-icon-list-for add driver//////////////////////
const imageAddDriver = document.getElementById('hover-image-Add-driver');
const dropdown3 = document.getElementById('dropdown-content-Add-driver');

imageAddDriver.addEventListener('mouseover', function () {
	dropdown3.style.display = 'block';
});

imageAddDriver.addEventListener('mouseout', function () {
	dropdown3.style.display = 'none';
});


// // /////////////////////////////////////////
let isScrolling = false;
let startX, startY, scrollLeft, scrollTop;

const scrollContainer = document.getElementById('scrollContainer');

scrollContainer.addEventListener('mousedown', (e) => {
	isScrolling = true;
	startX = e.pageX - scrollContainer.offsetLeft;
	startY = e.pageY - scrollContainer.offsetTop;
	scrollLeft = scrollContainer.scrollLeft;
	scrollTop = scrollContainer.scrollTop;
});

scrollContainer.addEventListener('mouseleave', () => {
	isScrolling = false;
});

scrollContainer.addEventListener('mouseup', () => {
	isScrolling = false;
});

scrollContainer.addEventListener('mousemove', (e) => {
	if (!isScrolling) return;
	e.preventDefault();
	const x = e.pageX - scrollContainer.offsetLeft;
	const y = e.pageY - scrollContainer.offsetTop;
	const walkX = (x - startX) * 2;
	const walkY = (y - startY) * 2;
	scrollContainer.scrollLeft = scrollLeft - walkX;
	scrollContainer.scrollTop = scrollTop - walkY;
});
 // /////////////////////////////////////////////////////////////////////  
 // ////////////////////////////////////////////////////////////////////////////////
function openThirdPopuppp() {
	// Hide the second popup modal
	$('#paymentPopupModal').modal('hide');

	// Open the third popup modal
	$('#thirdPopupModal').modal('show');

	// Close the third popup modal after 5 seconds
	setTimeout(function () {
		$('#thirdPopupModal').modal('hide');
		$('#checkModalToggle').modal('show'); // Show the checkModalToggle modal
	}, 5000); // Adjust the duration as needed (in milliseconds)
}
document.addEventListener('DOMContentLoaded', function () {
	document.querySelector('#AuthorizationForm').addEventListener('submit', function (event) {
		event.preventDefault();

		$.ajax({
			type: 'POST',
			url: 'https://jsonplaceholder.typicode.com/posts', // Using JSONPlaceholder as a mock server
			data: $(this).serialize(),
			success: function (response) {
				// Handle the response from the mock server
				console.log('Form data submitted successfully:', response);
				openThirdPopuppp()

			},
			error: function (error) {
				// Handle any errors
				console.error('Error submitting form data:', error);
			}
		});

	});

});
 // ///////////////////////////////////////////////////////////////////////////
function OpenHandSignPopup() {
	// Hide the second popup modal
	$('#signaturePopupModal').modal('hide');

	// Open the third popup modal
	$('#handsignatureModal').modal('show');


}
// // ///////////////////////////////////////////////////////////////////////////////////
var canvas = document.getElementById('canvas');
var ctx = canvas.getContext('2d');

// Variables for drawing
var drawing = false;
var prevX = 0;
var prevY = 0;
var currX = 0;
var currY = 0;

// Draw a line between the previous point and the current point
function drawLine(x0, y0, x1, y1) {
	ctx.beginPath();
	ctx.moveTo(x0, y0);
	ctx.lineTo(x1, y1);
	ctx.stroke();
	ctx.closePath();
}

// Add event listeners to handle mouse events
canvas.addEventListener('mousedown', function (e) {
	drawing = true;
	prevX = e.clientX - canvas.getBoundingClientRect().left;
	prevY = e.clientY - canvas.getBoundingClientRect().top;
});

canvas.addEventListener('mousemove', function (e) {
	if (!drawing) return;
	currX = e.clientX - canvas.getBoundingClientRect().left;
	currY = e.clientY - canvas.getBoundingClientRect().top;
	drawLine(prevX, prevY, currX, currY);
	prevX = currX;
	prevY = currY;
});

canvas.addEventListener('mouseup', function () {
	drawing = false;
});

// Clear the canvas
// function clearCanvas() {
// 	ctx.clearRect(0, 0, canvas.width, canvas.height);
// }

// document.getElementById('clear').addEventListener('click', function () {
// 	clearCanvas();
// });

// Save the canvas as an image
function saveCanvas() {
	var dataURL = canvas.toDataURL();
	var link = document.createElement('a');
	link.download = 'signature.png';
	link.href = dataURL;
	link.click();
}

document.getElementById('save').addEventListener('click', function () {
	saveCanvas();
	$('#handsignatureModal').modal('hide');

	// Open the third popup modal
	$('#thirdPopupModal').modal('show');
	// Close the third popup modal after 5 seconds
	setTimeout(function () {
		$('#thirdPopupModal').modal('hide');
	}, 5000); // Adjust the duration as needed (in milliseconds)
});
// // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function OpenPicSignPopup() {
	// Hide the second popup modal
	$('#signaturePopupModal').modal('hide');

	// Open the third popup modal
	$('#PicsignatureModal').modal('show');


}
// // ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
const uploadContainer = document.querySelector('.upload-container');
const imageUpload = document.getElementById('imageUpload');
const imageSubmitBtn = document.getElementById('image-submit-Btn');

uploadContainer.addEventListener('click', function () {
	imageUpload.click();
});

imageUpload.addEventListener('change', function () {
	const file = imageUpload.files[0];
	if (file) {
		const reader = new FileReader();
		reader.onload = function (e) {
			const imageURL = e.target.result;
			const previewImage = document.createElement('img');
			previewImage.classList.add('preview-image');
			previewImage.src = imageURL;
			uploadContainer.innerHTML = '';
			uploadContainer.appendChild(previewImage);
			uploadContainer.classList.add('previewing');
		};
		reader.readAsDataURL(file);
	}
});

imageSubmitBtn.addEventListener('click', function (event) {
	event.preventDefault(); // Prevent the default form submission behavior
	if (uploadContainer.firstChild) {
		// Image is selected and previewed, you can perform further actions here
		console.log('Image submitted!');
		uploadContainer.innerHTML = ''; // Remove the image from the preview container
		uploadContainer.classList.remove('previewing'); // Reset the preview state
		uploadContainer.innerHTML = ' <img class="upload-icon" src="img/Rectangle 144.png" alt="Upload Icon"><p>ارفق صورة التوقيع</p>';

	} else {
		// No image selected or previewed
		console.log('Please select an image.');
	}
	$('#PicsignatureModal').modal('hide');

	// Open the third popup modal
	$('#thirdPopupModal').modal('show');
	// Close the third popup modal after 5 seconds
	setTimeout(function () {
		$('#thirdPopupModal').modal('hide');
	}, 5000); // Adjust the duration as needed (in milliseconds)
});
// ///////%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

