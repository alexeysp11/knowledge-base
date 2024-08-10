// File for invoking algorithms 

document.addEventListener("DOMContentLoaded", function() {
    // this function runs when the DOM is ready, i.e. when the document has been parsed
    
    document.getElementById("main-header").textContent = "Solving leetcode problems".toUpperCase();
    document.getElementById("leetcode-problem-name").textContent = ShuffleArray.getLeetcodeProblemName();
});

var buttonClick = document.addEventListener('click', function(event) {
    var a = new ShuffleArray;
    if (event.target.nodeName == "BUTTON" && event.target.id == "get-result-btn") {
        document.getElementById("leetcode-problem-result").textContent = a.execute();
    }
});
