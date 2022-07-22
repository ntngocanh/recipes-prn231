function getReactionsCount() {
    $.ajax({
        url: "https://localhost:5001/api/Reactions/getByRecipe/" + $("#recipe-id").text(),
        method: 'get',
        contentType: "application/json",
        success: function (result, status, xhr) {
            var countHeart = 0;
            var countClap = 0;
            var countYum = 0;
            $.each(result, function (index, value) {
                if (value["reactionType"] == 0)
                    countHeart++;
                if (value["reactionType"] == 1)
                    countYum++;
                if (value["reactionType"] == 2)
                    countClap++;
            });
            $("#yummy-react-count").html(countYum);
            $("#heart-react-count").html(countHeart);
            $("#clap-react-count").html(countClap);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}

function GetCurrentUserReact(userid, recipeid) {
    $.ajax({
        url: "https://localhost:5001/api/Reactions/getByUserAndRecipe/" + recipeid + "/" + userid,
        method: 'get',
        contentType: "application/json",
        statusCode: {
            404: function () {
                console.log("not found");
            }
        },
        success: function (result, status, xhr) {
            if (result) {
                if (result["reactionType"] == 0) {
                    $("#heart-react-btn").addClass("react-btn-active");
                }
                if (result["reactionType"] == 1) {
                    $("#yummy-react-btn").addClass("react-btn-active");
                }
                if (result["reactionType"] == 2) {
                    $("#clap-react-btn").addClass("react-btn-active");
                }
            }
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}



$(document).ready(function () {
    
    getReactionsCount();

});