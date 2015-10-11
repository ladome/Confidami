
$(function () {
    $.fn.raty.defaults.path = '/content/raty-2.7.0/images';
    if (($('.star').attr('data-score') > 0)) {
        $.fn.raty.defaults.readOnly = true;
    }
    $('.star').raty({
        score: function() {
            return $(this).attr('data-score');
        },
        click: function (score, evt) {
            if (evt.currentTarget.className === "raty-cancel") {
                alert('cancel pressed');
            }
            else
            {
                var data = {};
                data.idPost = idpost;
                data.vote = score;
                $.ajax({
                    url: APIURL + "/post/vote",
                    type: "POST",
                    dataType: "json",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(data),
                    success: function (result) {
                        alert(result);
                        $('.star').raty('readOnly', true);
                    },
                    error: function (result) {
                        alert(result);
                    }
                });
                
            }

        },
        hints: ['Non rilevante', 'Interessante', 'Incazzato', 'Molto grave', 'Da segnalare alle autorità'],
    });
})