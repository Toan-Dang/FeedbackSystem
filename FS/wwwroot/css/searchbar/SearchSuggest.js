function getXmlHttpRequestObject() {
    if (window.XMLHttpRequest) {
        return new XMLHttpRequest();
        //For all the new browsers
    }
    else if (window.ActiveXObject) {
        //for IE5,IE6
        return new ActiveXObject("Microsoft.XMLHTTP");
    }
    else {
        alert("Time to upgrade your browser?");
    }
}
//Our XmlHttpRequest object to get the auto suggestvar
searchReq = getXmlHttpRequestObject();

function searchSuggest(e) {
    var key = window.event ? e.keyCode : e.which;

    if (key == 40 || key == 38) {
        scrolldiv(key);
    }
    else {
        if (searchReq.readyState == 4 || searchReq.readyState == 0) {
            var str = escape(document.getElementById('txtSearch').value);
            strOriginal = str;
            searchReq.open("GET", 'Result.aspx?search=' + str, true);
            searchReq.onreadystatechange = handleSearchSuggest;
            searchReq.send(null);
        }
    }
}