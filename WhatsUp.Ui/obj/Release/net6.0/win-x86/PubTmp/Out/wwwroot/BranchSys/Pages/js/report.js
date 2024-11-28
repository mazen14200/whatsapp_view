const form = document.getElementById('report-form');

form.addEventListener('submit', function(event) {
  event.preventDefault();

  const formData = new FormData(form);
  for (let pair of formData.entries()) {
    console.log(pair[0] + ': ' + pair[1]);
  }

 
});
////////////////////////////////////////////////////////////////////////////
var start = document.getElementById('start-date');
var end = document.getElementById('end-date');

start.addEventListener('change', function() {
    if (start.value)
        end.min = start.value;
}, false);
end.addEventListener('change', function() {
    if (end.value)
        start.max = end.value;
}, false);
/////////////////////////////////////////////////////////////////////////////////
var rows = document.getElementById("reportTable").getElementsByTagName("tr");
for (var i = 0; i < rows.length; i++) {
  rows[i].onclick = function (event) {
    if (event.target.classList.contains("contract_number")) {
      console.log(event.target.textContent);
    }
  };
}
