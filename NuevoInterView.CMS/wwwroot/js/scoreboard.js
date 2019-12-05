var fileName = $('body').attr('class') + '.png';

//Edit Any Text
$('body').on('click', "span.editable", function () {
    var label = $(this), labelText = label.text();
    var textbox = $('<input type="text" class="textbox" value="' + labelText + '" style="width:' + label.width() + 'px" />');
    var parentEl = label.parent('.time');

    label.replaceWith(textbox);
    textbox.focus();

    textbox.blur(function () {
        textbox.replaceWith(label);
    })
        .keydown(function (evt) {
            if (evt.keyCode == 13) {
                var newText = $(this).val();
                parentEl.removeClass('result');
                if (newText.indexOf('-') > -1) {
                    parentEl.addClass('result');
                }
                label.text(newText);
                textbox.replaceWith(label);
            }
        });
}); 


//Set Team Icon
$('body').on('change', '.icon', function (e) {
    //console.log(e.target.nextSibling);
    var output = $(this).siblings('img');
    var reader = new FileReader();
    reader.onload = function () {

        output.attr('src', reader.result);
    }
    reader.readAsDataURL(e.target.files[0]);
});

//Screen Shot Function
function screenShot(picSize) {
    
    $('.wrapper').addClass(picSize);
    html2canvas(document.querySelector('.wrapper')).then(function (canvas) {
        $('#download').attr('href', canvas.toDataURL('image/png'));
        $('#download').attr('download', picSize + '-' + fileName);
        $('#download')[0].click();
        $('.wrapper').removeClass(picSize);
        
    });
}

//Increase Match
$('#increase').on('click', function () {
    var matchLength = $('.match').length + 1;
    if (matchLength > 5) {
        $('.wrapper').addClass('more-five');
    }

    var match = '<div class="match"><div class="team home" ><span class="team-icon"><label><input type="file" class="icon" /><img src="/images/scoreboard/team-icon.png" class="img-fluid" /></label></span><span class="team-name editable">Takım</span></div ><div class="time"><span class="editable">00:00</span></div><div class="team away"><span class="team-icon"><label><input type="file" class="icon" /><img src="/images/scoreboard/team-icon.png" class="img-fluid" /></label></span><span class="team-name editable">Takım</span></div></div >';

    $('div.matches').append(match);
});

//Reduce Match
$('#reduce').on('click', function () {
    var matchLength = $('.match').length - 1;
    if (matchLength < 6) {
        $('.wrapper').removeClass('more-five');
    }
    $('div.matches .match').last().remove();
});

//Wide Screen Shot
$('#wideScreenShot').on('click', function () {
    screenShot('wide');
});

//Slim Screen Shot
$('#slimScreenShot').on('click', function () {
    screenShot('slim');
});

//Screen Shot
$('body').on('keydown', function (e) {
    var fileName = $('body').attr('class') + '.png';
    if (e.keyCode == 33) {
        $('.wrapper').addClass('wide');
        html2canvas(document.querySelector('.wrapper')).then(function (canvas) {
            $('#download').attr('href', canvas.toDataURL('image/png'));
            $('#download').attr('download', fileName);
            $('#download')[0].click();
            $('.wrapper').removeClass('wide');
        });
    } else if (e.keyCode == 34) {
        $('.wrapper').addClass('slim');
        html2canvas(document.querySelector('.wrapper')).then(function (canvas) {
            $('#download').attr('href', canvas.toDataURL('image/png'));
            $('#download').attr('download', "slim-" + fileName);
            $('#download')[0].click();
            $('.wrapper').removeClass('slim');
        });
    } else if (e.keyCode == 40) {
        var matchLength = $('.match').length + 1;
        if (matchLength > 5) {
            $('.wrapper').addClass('more-five');
        }

        var match = '<div class="match"><div class="team home" ><span class="team-icon"><label><input type="file" class="icon" /><img src="/images/scoreboard/team-icon.png" class="img-fluid" /></label></span><span class="team-name editable">Takım</span></div ><div class="time"><span class="editable">00:00</span></div><div class="team away"><span class="team-icon"><label><input type="file" class="icon" /><img src="/images/scoreboard/team-icon.png" class="img-fluid" /></label></span><span class="team-name editable">Takım</span></div></div >';

        $('div.matches').append(match);
    } else if (e.keyCode == 38) {
        var matchLength = $('.match').length - 1;
        if (matchLength < 6) {
            $('.wrapper').removeClass('more-five');
        }
        $('div.matches .match').last().remove();
    }
});