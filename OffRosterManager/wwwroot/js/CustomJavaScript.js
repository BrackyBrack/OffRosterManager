function search() {
    // Declare variables
    var filter, input;
    input = document.getElementById("searchText")
    filter = input.value.toUpperCase();
    searchTable(filter, "awaitingTable", "awaitingTableCount");
    searchTable(filter, "activeTable", "activeTableCount");
    searchTable(filter, "openTable", "openTableCount");
    searchTable(filter, "futureTable", "futureTableCount");
    searchTable(filter, "historicTable", "historicTableCount");
}

function searchTable(searchItem, tableId, countId) {
    var table, tr, td, totalResults, i, txtValue
    totalResults = 0;
    table = document.getElementById(tableId);
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(searchItem) > -1) {
                tr[i].style.display = "";
                totalResults++;
            } else {
                tr[i].style.display = "none";
            }
        }
    }

    document.getElementById(countId).innerHTML = totalResults;
}