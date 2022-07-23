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
            $("#yummy-react-count1").html(countYum);
            $("#heart-react-count").html(countHeart);
            $("#heart-react-count1").html(countHeart);
            $("#clap-react-count").html(countClap);
            $("#clap-react-count1").html(countClap);

            var total = countHeart + countClap + countYum;
            $("#reaction-toggle").html(total + " người đã bày tỏ cảm xúc về công thức");

        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}

function fillReactList() {
    $.ajax({
        url: "https://localhost:5001/api/Reactions/getByRecipe/" + $("#recipe-id").text(),
        method: 'get',
        contentType: "application/json",
        success: function (result, status, xhr) {
            $.each(result, function (index, value) {
                var htmlContent = `<div class="row flex-row">
                                <div class="avatar avatar-sm rounded-circle">
                                        <img class="avatar-img" src="/images/`+ value["user"]["avatar"] + `" alt="">
                                    </div>
                                <div>` + value["user"]["name"] +`</div>
                                </div>`;
                var div = $("<div>").attr("class", "container").attr("style", "margin: 5px").html(htmlContent);
                if (value["reactionType"] == 0)
                    div.appendTo($("#heart-list"));
                if (value["reactionType"] == 1)
                    div.appendTo($("#yummy-list"));
                if (value["reactionType"] == 2)
                    div.appendTo($("#clap-list"));
            });

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
    fillReactList();
});