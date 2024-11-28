const image = document.getElementById('hover-image-extension2');
const dropdown = document.getElementById('dropdown-content-extension2');

image.addEventListener('click', function () {
	if (dropdown.style.display === 'block') {
        dropdown.style.display = 'none';
    } else {
        dropdown.style.display = 'block';
    }
});