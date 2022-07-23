$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
    $("#header").hide();
    $("#sitecss").attr("disabled", "true");
});