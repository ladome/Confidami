/*
* Common functions js
*/
$(function () {
    //capire come ottenere l'user id senza autorizzazione all'app
    $.ajaxSetup({ cache: true });
    window.fbAsyncInit = function () {
        FB.init({
            appId: '1024000484311306',
            xfbml: true,
            version: 'v2.4',
            oauth: true,
            cookie: true
        });
        FB.Event.subscribe("comment.create", function (e) {
            FB.getLoginStatus(function (response) {
                if (response.status !== 'connected') {
                    console.log('non connesso');
                    FB.login(function (response) {
                        // user is logged in and granted permissions.
                        console.log('authresponse', response.authResponse);
                        if (response.authResponse) {
                            CreateComment(e.commentID, e.href);
                        }
                        else { // User cancelled login or did not fully authorize.
                            alert('Problema di autorizzazione');

                        }
                    }, { scope: 'email' });
                }
                CreateComment(e.commentID, e.href);
                });
            });
        };

    $('[data-toggle="popover"]').popover(); //Initi popover tags
});

function CreateComment(commentid,href) {
    FB.api(
    "/" + commentid,
    function (response) {
        console.log('creazione commento', response);
        var result = {};
        if (response && !response.error) {
            //result.idcomment = e.commentID,
            result.idcomment = commentid,
            result.userid = response.from.id,
            result.name = response.from.name,
            result.comment = response.message,
            //result.pageurl = e.href;
            result.pageurl = href;
            result.idpost = idpost;
            FB.api(
                "/me", { fields: 'email' },
                function (response) {
                    if (response && !response.error) {
                        result.usermail = response.email;

                        $.ajax({
                            url: APIURL + "/post/comment",
                            type: "POST",
                            dataType: "json",
                            async: true,
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(result)
                        });
                    }
                }
            );
        }
        console.log('all right');
    }
);
}

