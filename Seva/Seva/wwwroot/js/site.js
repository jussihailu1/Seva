// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function AddImageInput() {
    var input = "<br/><input name='images' type='file' oninput='AddImageInput()' class='form-control-file' />";
    $("#imageContainer").append(input);
}

//Index page
//------------------------------------------------------------------------------------------------

var servicesToShow;
var shownServicesCount;

function ToggleImageIndicators(v) {
    var carouselIndicators = document.getElementsByClassName("carousel-indicators");
    for (var i = 0; i < carouselIndicators.length; i++) {
        carouselIndicators[i].hidden = !v;
    }
}

//Index page filtering
//------------------------------------------------------------------------------------------------

$('.filter').on('change',
    function () {
        FilterItems();
    });

$('.filter').on('keyup',
    function () {
        FilterItems();
    });

function FilterItems() {
    //servicesToShow = [];
    var allItems = document.getElementsByClassName("service");
    for (var i = 0; i < allItems.length; i++) {
        var item = allItems[i];
        item.hidden = true;

        var itemsCategory = item.classList[3].substring("category".length);
        var itemsName = item.classList[4].substring("name".length);
        var itemsPrice = item.classList[5];

        if (IsCategory(itemsCategory) && IsName(itemsName) && IsPrice(itemsPrice)) {
            //servicesToShow.push(item);
            item.hidden = false;
        }
    }
    //SetGrid();
    //HtmlSetResult();
}

function IsCategory(category) {
    var selectedCategory = document.getElementById('categoryFilter').value;
    var searchedCategoryTrimmed = "";
    for (var i = 0; i < selectedCategory.length; i++) {
        var char = selectedCategory[i];
        if (char != ' ') {
            searchedCategoryTrimmed += char;
        }
    }
    return selectedCategory == "Select category" || category == searchedCategoryTrimmed;
}

function IsName(serviceName) {
    var searchedName = document.getElementById('searchBox').value;
    var searchedNameTrimmed = "";
    for (var i = 0; i < searchedName.length; i++) {
        var char = searchedName[i];
        if (char != ' ') {
            searchedNameTrimmed += char;
        }
    }
    document.getElementById("searchBox").value = searchedName.toLowerCase();
    return searchedName == "" || serviceName.toLowerCase().indexOf(searchedNameTrimmed) > -1;
}

function IsPrice(price) {
    price = price.substring(0, price.indexOf(','));

    var selectedPriceRange = document.getElementById('priceFilter').value;
    var selectedMinPrice = selectedPriceRange.substring(0, selectedPriceRange.indexOf('-'));
    var selectedMaxPrice = selectedPriceRange.substring(selectedPriceRange.indexOf('-') + 1);

    if (selectedMaxPrice == "Max") { selectedMaxPrice = 999999999; }

    //Nog niet helemaal betrouwbaar
    return selectedPriceRange == "Select a price range" || price > selectedMinPrice && price < selectedMaxPrice;
}

//function IsRating(rating) {
//    var minRating = document.getElementById('averageRatingMin').value;
//    var maxRating = document.getElementById('averageRatingMax').value;

//    //if (maxRating < minRating) {
//    //    maxRating = minRating + 1;
//    //}
//    //if (minRating > maxRating) {
//    //    minRating = maxRating - 1;
//    //}
//    //if (minRating < 0) {
//    //    document.getElementById('averageRatingMin').innerHTML = 0;
//    //}
//    //if (maxRating > 10) {
//    //    document.getElementById('averageRatingMax').innerHTML = 10;
//    //}

//    console.log(minRating + "-" + maxRating);

//    //TODO (js): Dit werkt nog niet helemaal.
//    return rating >= minRating && rating <= maxRating;
//}

//function HtmlSetResult() {
//    if (IsHomePage()) {
//        document.getElementById('resultCount').innerHTML = servicesToShow.length;
//    }
//}


//Pagination:
//----------------------------------------------------------------------

//function SetGrid() {
//    for (var i = 0; i < servicesToShow.length; i++) {
//        var service = servicesToShow[i];
//        service.hidden = true;

//        if (i < shownServicesCount) {
//            service.hidden = false;
//        }
//    }
//}

//$(document).ready(function () {
//    servicesToShow = document.getElementsByClassName('service');
//    if (IsHomePage()) {
//        shownServicesCount = document.getElementById('results').value;
//    }
//    HtmlSetResult();
//    SetGrid();

//    $('#results').on('change',
//        function () {
//            shownServicesCount = document.getElementById('results').value;

//            SetGrid();
//        });
//});

//function IsHomePage() {
//    return document.getElementById('results') != null;
//}