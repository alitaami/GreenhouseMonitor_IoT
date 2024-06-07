  document.addEventListener('DOMContentLoaded', function () {
    // Prepare the data for the charts
    var sensorData = JSON.parse(document.getElementById('sensorData').textContent);

    var labels = sensorData.map(item => {
        var date = new Date(item.Timestamp);
        // Convert Gregorian date to Shamsi date
        var pDate = new persianDate(date).format('YYYY/MM/DD HH:mm:ss');
        return pDate;
    });

    var temperatures = sensorData.map(item => item.Temperature);
    var humidities = sensorData.map(item => item.Humidity);

    // Temperature Chart
    var tempCtx = document.getElementById('temperatureChart').getContext('2d');
    new Chart(tempCtx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'دما (سلسیوس)',
                data: temperatures,
                borderColor: 'rgba(255, 99, 132, 1)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                fill: true
            }]
        },
        options: {
            responsive: true,
            scales: {
                x: { display: true, title: { display: true, text: 'زمان' } },
                y: { display: true, title: { display: true, text: 'دما (سلسیوس)' } }
            }
        }
    });

    // Humidity Chart
    var humidityCtx = document.getElementById('humidityChart').getContext('2d');
    new Chart(humidityCtx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'رطوبت (درصد)',
                data: humidities,
                borderColor: 'rgba(54, 162, 235, 1)',
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                fill: true
            }]
        },
        options: {
            responsive: true,
            scales: {
                x: { display: true, title: { display: true, text: 'زمان' } },
                y: { display: true, title: { display: true, text: 'رطوبت (درصد)' } }
            }
        }
    });

      // Export to Excel button click event
      document.getElementById('exportToExcelBtn').addEventListener('click', exportToExcel);
  });

// Export to Excel function
function exportToExcel() {
    // Get the table data
    var table = document.querySelector('table');
    var rows = table.querySelectorAll('tr');

    // Prepare the Excel data
    var data = [];
    for (var i = 0; i < rows.length; i++) {
        var row = [];
        var cols = rows[i].querySelectorAll('td, th');
        for (var j = 0; j < cols.length; j++) {
            row.push(cols[j].innerText);
        }
        data.push(row);
    }

    // Create a new Excel workbook
    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(data);
    XLSX.utils.book_append_sheet(wb, ws, "SensorData");

    // Save the Excel file
    XLSX.writeFile(wb, "SensorData.xlsx");
}