//////////////////////////charts////////////////////////////////
//var available_cars = ["2005", "2012", "2001", "2005", "2012"];
//var not_available_cars = ["2005", "2001", "2001"];
//var rented_cars = ["2012", "2005" ,"2003",'2023'];

//var barChart1 = document.getElementById("horizontal_bar").getContext("2d");
//var myChart1 = new Chart(barChart1, {
//  type: "bar",
//  data: {
//    labels: ['متاحة', 'مؤجرة', 'غير متاحة'],
//    datasets: [
//      {
//        data: [available_cars.length, rented_cars.length, not_available_cars.length],
//        backgroundColor: [
//          "rgba(255, 150, 38, 1)",
//          "rgba(151, 71, 255, 1)",
//          "rgba(255, 38, 38, 1)",
//        ],
       
//      }
//    ]
//  },
//  options: {
//    indexAxis: 'y',
//    plugins: {
//      legend: {
//        display: false
//      },
//      tooltip: {
//        callbacks: {
//          label: function(context) {
//            var label =[];
//            label =getCountByVersion(context.dataIndex);
//            return label;
//          }
//        }
//      }
//    },
//    scales: {
//      x: {
//        grid: {
//          display: false // Remove vertical lines
//        },
//        ticks: {
//          font: {
//            size: 10 // Change the font size of the tick labels
//          }
//        }
//      },
//      y: {
//        grid: {
//          display: false // Remove horizontal lines
//        },
//        beginAtZero: true,
//      }
//    },
//    layout: {
//      padding: {
//        left: 10,
//        right: 10,
//        top: 10,
//        bottom: 10
//      }
//    },
//    responsive: true,
//    maintainAspectRatio: false,
//    barPercentage: 0.6, // Adjust the width of the bars
//    categoryPercentage: 0.8 // Adjust the width of the bars
//  }
//});
//function getCountByVersion(dataIndex) {
//  var versions = {};
//  var cars;
  
//  switch (dataIndex) {
//    case 0:
//      cars = available_cars;
//      break;
//    case 1:
//      cars = rented_cars;
//      break;
//    case 2:
//      cars = not_available_cars;
//      break;
//    default:
//      cars = [];
//  }
  
//  cars.forEach(function(version) {
//    if (!versions[version]) {
//      versions[version] = 1;
//    } else {
//      versions[version]++;
//    }
//  });
  
//  var countByVersion = [];
//  Object.keys(versions).forEach(function(version) {
//    countByVersion.push(versions[version] + ':' + version+"\n\r");
//  });
  
//  return countByVersion;
//}
//pieChart
//const pieChartEl = document.getElementById('pieChart');
//const pieChartData = {
//  labels: ['العقود المنتهية', ' تنتهي اليوم', ' تنتهي غدا', 'تنتهي لاحقا'],
//  datasets: [{
//    data: [342, 313, 245, 210],
//    backgroundColor: ['rgba(242, 143, 36, 1)', '#9966FF', 'rgba(242, 36, 36, 1)', 'rgba(1, 5, 83, 1)']
//  }]
//};

//const pieChart = new Chart(pieChartEl, {
//  type: 'doughnut',
//  data: pieChartData,
//  options: {
//    responsive: true,
//    maintainAspectRatio: false,
//    plugins: {
//      tooltip: {
//        callbacks: {
//          label: function (context) {
//            return context.dataset.data[context.dataIndex];
//          }
//        }
//      },
//      legend: {
//        display: false
//      }
//    }
//  }
//});

//const legendContainer = document.querySelector('.chart-legend');
//const legendItems = pieChartData.labels.map((label, index) => {
//  const dataset = pieChartData.datasets[0];
//  const backgroundColor = dataset.backgroundColor[index];
//  const legendItem = document.createElement('div');
//  legendItem.classList.add('legend-item');
//  legendItem.innerHTML = `${label}<span style="background-color:${backgroundColor}"></span>`;
//  return legendItem;
//});

//legendItems.forEach(item => {
//  legendContainer.appendChild(item);
//});

//// Bar Chart 2
//var barChart2 = document.getElementById("barChart2").getContext("2d");
//var myChart = new Chart(barChart2, {
//  type: "bar",
//  data: {
//    labels: ['نقدًا', 'مدى', 'فيزا', 'اكسبرس', 'ماستر ', 'تحويل'],
//    datasets: [
//      {
//        data: [100, 60, 50, 45, 40, 30],
//        backgroundColor: [
//          "rgba(255, 99, 132, 1)",
//          "rgba(54, 162, 235, 1)",
//          "rgba(255, 206, 86, 1)",
//          "rgba(75, 192, 192, 1)",
//          "rgba(153, 102, 255, 1)",
//          "rgba(255, 159, 64, 1)"
//        ],
       
//      }
//    ]
//  },
//  options: {
//    plugins: {
      
//      legend: {
//        display: false
//      }

//    },
//    scales: {
//      x: {
//        grid: {
//          display: false // Remove vertical lines
//        }
//      },
//      y: {
//        grid: {
//          display: false // Remove horizontal lines
//        },
//        beginAtZero: true,
//        ticks: {
//          font: {
//            size: 11 // Change the font size of the tick labels
//          }
//        }
//      }

//    }
//  }
//});

//var chrt = document.getElementById("chartId").getContext("2d");
//var chartId = new Chart(chrt, {
//  type: "bar",
//  data: {
//    labels: ['نقدًا', 'مدى', 'فيزا', 'اكسبرس', 'ماستر ', 'تحويل'],
//    datasets: [
//      {
//        data: [100, 50, 45, 60, 40, 30],
//        backgroundColor: [
//          "rgba(255, 99, 132, 1)",
//          "rgba(54, 162, 235, 1)",
//          "rgba(255, 206, 86, 1)",
//          "rgba(75, 192, 192, 1)",
//          "rgba(153, 102, 255, 1)",
//          "rgba(255, 159, 64, 1)"
//        ],
       

//      }
//    ]
//  },
//  options: {
//    plugins: {
//      legend: {
//        display: false
//      }
//    },
//    scales: {
//      x: {
//        grid: {
//          display: false // Remove vertical lines
//        }
//      },
//      y: {
//        grid: {
//          display: false // Remove horizontal lines
//        },
//        beginAtZero: true,
//        ticks: {
//          font: {
//            size: 11 // Change the font size of the tick labels
//          }
//        }
//      }

//    }
//  }
//});

/////////////////////////notification icons/////////////////////////////////

const notificationImage = document.getElementById('notificationImage');
let isImageChanged = false;

notificationImage.addEventListener('dblclick', function () {
  if (isImageChanged) {
    notificationImage.src = 'img/notofication.png';
    isImageChanged = false;
    counter.style.display = 'inline-block'; // Display the counter
  } else {
    notificationImage.src = 'img/Group 3.png';
    isImageChanged = true;
    counter.style.display = 'none';
  }
});
/////////////////////////////////////////////////////////////

function setActiveButton(buttonIndex) {
  // Get all the buttons
  var buttons = document.querySelectorAll('.btn-secondary1');

  // Remove the active class from all buttons
  for (var i = 0; i < buttons.length; i++) {
    buttons[i].classList.remove('active1');
  }

  // Set the active class for the clicked button
  buttons[buttonIndex].classList.add('active1');
}
/////////////funcation to sunbmit brunch name without button/////////////
$(document).ready(function () {
  $("#brunch_name").change(function () {
    $("#formID").submit();
  });
});


////////////js function to view the charts///////////

const employeeCustody = document.getElementById('employee-custody');

// Function to start or stop the neonShadow effect
function toggleNeonShadow() {
  const brunchButton = document.getElementById("pulse-button-brunch");
  const brunchButton_img = document.getElementById("brunch_button-img");

  if (employeeCustody.innerText >= 100000) {
    brunchButton.classList.add('neonShadow');
    brunchButton_img.classList.add('anm');
  } else {
    brunchButton.classList.remove('neonShadow');
    brunchButton_img.classList.remove('anm');
  }

  const formattedEmployeeCustody = parseInt(employeeCustody.innerText.replace(/,/g, '')).toLocaleString();
  employeeCustody.innerText = formattedEmployeeCustody;
  
}

// Call the toggleNeonShadow function initially
toggleNeonShadow();
// Function to format a number with thousands separators
function formatNumberWithThousandsSeparator(elementId) {
  const element = document.getElementById(elementId);
  const value = element.innerText;
  const formattedValue = parseInt(value.replace(/,/g, '')).toLocaleString();
  element.innerText = formattedValue;
}



formatNumberWithThousandsSeparator("brunch-custody");
formatNumberWithThousandsSeparator("Executed-contracts");
formatNumberWithThousandsSeparator("Closed-contracts");
formatNumberWithThousandsSeparator("Available-balance-brunch");
formatNumberWithThousandsSeparator("used-balance-brunch");
formatNumberWithThousandsSeparator("Available-balance-employee");
formatNumberWithThousandsSeparator("used-balance-employee");


