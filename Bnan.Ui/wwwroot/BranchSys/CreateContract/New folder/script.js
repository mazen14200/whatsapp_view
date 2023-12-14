var canvas = document.getElementById('canvas');
var signaturePad = new SignaturePad(canvas, {
    backgroundColor: 'rgba(255, 255, 255, 0)',
    penColor: 'black',
    throttle: 16, // For performance
});

document.getElementById('clear').addEventListener('click', function (event) {
    signaturePad.clear();
});

document.getElementById('save').addEventListener('click', function (event) {
    if (signaturePad.isEmpty()) {
        alert("Please provide a signature first.");
    } else {
        var dataURL = signaturePad.toDataURL();
        var img = document.createElement('img');
        img.src = dataURL;

        // You can download image by right-click menu.
        var link = document.createElement('a');
        link.download = 'signature.png';
        link.href = dataURL;
        link.click();

        // Or you can set image source to any <img> tag.
        // img.src = signaturePad.toDataURL();
    }
});