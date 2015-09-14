/*
* Common functions js
*/

$(function () {
    //capire come ottenere l'user id senza autorizzazione all'app
    var APIURL = "/api";
    $.ajaxSetup({ cache: true });
        window.fbAsyncInit = function () {
            FB.init({
                appId: '570139676365041',
                xfbml: true,
                version: 'v2.4',
                status: true
            });
        FB.Event.subscribe("comment.create", function (e) {
            var userMail = "";
            var result = {};
            var a = FB.getAccessToken();
            FB.api(
                "/" + e.commentID + "/comments",
                function(response) {
                console.log(response)
                    if (response && !response.error) {
                        
                        result.idcomment = e.commentID,
                        result.userid = response.from.id,
                        result.name = response.from.name,
                        result.comment = response.message,
                        result.pageurl = e.href;
                        result.idpost = idpost;
                        FB.api(
                        "/me",
                        function (response) {
                            if (response && !response.error) {
                                result.usermail = response.email;

                                $.ajax({
                                    url: APIURL + "/post/comment",
                                    type: "POST",
                                    dataType: "json",
                                    async: false,
                                    contentType: "application/json; charset=utf-8",
                                    data: JSON.stringify(result)
                                });
                            }
                        }
                    );
                    }
                }
            );
            

            

        })       
        };

    $('[data-toggle="popover"]').popover(); //Initi popover tags



});