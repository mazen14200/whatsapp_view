document.addEventListener("DOMContentLoaded", function () {
  document.body.addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
      document.getElementById("contract-extension-form").onsubmit = function (event) {
        event.preventDefault();
        console.log("Search Query:", document.getElementById("search").value);
      };
    }
  });
});
//////////////////////////////////////////////////////////////////////////////////////
document.addEventListener("DOMContentLoaded", function () {
  var tableRows = document.getElementById("extensionTable").getElementsByClassName("name");
  for (var i = 0; i < tableRows.length; i++) {
    tableRows[i].addEventListener("click", function () {
      window.location.href = "extension2.html";
    });
  }
});

var rows = document.getElementById("extensionTable").getElementsByTagName("tr");
for (var i = 0; i < rows.length; i++) {
  rows[i].onclick = function () {
    var cells = this.getElementsByTagName("td");
    
      console.log(cells[5].innerHTML);
    
  };
}
