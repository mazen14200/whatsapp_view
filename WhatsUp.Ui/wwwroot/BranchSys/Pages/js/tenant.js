document.addEventListener("DOMContentLoaded", function () {
    document.body.addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            document.getElementById("tenant-form").onsubmit = function (event) {
                event.preventDefault();
                console.log("Submitted Data:");
                console.log("Search Query:", document.getElementById("search").value);
                console.log("Selected Option:", document.querySelector('input[name="Options"]:checked').value);
               
            };
        }
    });


});
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

document.addEventListener("DOMContentLoaded", function() {
  var tableRows = document.getElementById("myTable").getElementsByTagName("td");
  for (var i = 0; i < tableRows.length; i++) {
    tableRows[i].addEventListener("click", function() {
      window.location.href = "tenant-data.html"; 
    });
  }
});

// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var rows = document.getElementById("myTable").getElementsByTagName("tr");
for (var i = 0; i < rows.length; i++) {
  rows[i].onclick = function () {
    var cells = this.getElementsByTagName("td");
    
      console.log(cells[7].innerHTML);
    
  };
}
