/*
* Common functions js
*/

$(function () {

    $.ajaxSetup({ cache: true });
    $.getScript('//connect.facebook.net/it_IT/sdk.js', function () {
        FB.init({
            appId: '125684191103707',
            version: 'v2.3',
            xfbml: true
        });
        
        
    });

    $('[data-toggle="popover"]').popover(); //Initi popover tags



});