/*layout page start */

 
var url = window.location.protocol + '//' + window.location.host;

function LayoutPageDocument() {
    $("#owl-demo").owlCarousel({
        navigation: true,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true,
        autoPlay: true

    });
};

$(function () {
    $('select.styled').customSelect();
});

function flagGet(flagURL) {
    $("#languageImg").attr('src', flagURL);
};
/* layout page end */

/* Epin page */
function EpinMask() {




    $("#EpinCode").mask("****-****-****-****");

    $("#EpinCode").on("blur", function () {
        var last = $(this).val().substr($(this).val().indexOf("-") + 1);

        if (last.length == 3) {
            var move = $(this).val().substr($(this).val().indexOf("-") - 1, 1);
            var lastfour = move + last;

            var first = $(this).val().substr(0, 9);

            $(this).val(first + '-' + lastfour);
        }
    });

};

function Hesapla(sayi) {

    var indirim = $("#indirim_" + sayi).html().trim().replace(',','.');
    var Addon = $("#Addon_" + sayi).html();
    var Quantity = parseInt($("#Input_" + sayi).val());
    if (isNaN(Quantity))
    {
        Quantity = 0;
    }
    var toplam = indirim * Quantity;
 
    var toplamHesapla = $("#araToplam_" + sayi).html(toplam.toFixed(2));  

    if (Quantity == "")
    {
        Quantity = 0;
    }
  
   
    GenelToplam();
    return false;

}

function GenelToplam()
{
    $("#genelToplam").html('');
    var genelToplam="0";
    var kdvToplam = "0";
    var toplamAddon = $("#toplamaddon").html();

    for (var i = 0; i <= toplamAddon-1; i++)
    {
        var item = $("#araToplam_" + i).html();
        var commis = $("#commis_" + i).html();
        kdvToplam = (parseFloat(kdvToplam + parseFloat(commis))) ;
        genelToplam = parseFloat(genelToplam + parseFloat(item));
  
    }
    $("#kdvToplam").html(kdvToplam / toplamAddon);

 
    $("#genelToplam").html(genelToplam.toFixed(2));

}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
};

function ListPost()
{
    orderDetail = [];

    var toplamAddon = $("#toplamaddon").html();
    for (var i = 0; i <= toplamAddon - 1; i++) {
        var Addon = $("#Addon_" + i).html();
        var Quantity = parseInt($("#Input_" + i).val());
        if (Quantity > 0) {
            item = {}
            item["AddonName"] = Addon;
            item["Quantity"] = Quantity;
            orderDetail.push(item);
        }
    }
 
    $.ajax({
        type: 'post',
        contentType: 'application/json',
        dataType: 'json',
        type: 'POST',
        url: url + "/Epin/OrderList" ,
        data: JSON.stringify(orderDetail),
        //data: orderList  ,
        success: function (data) {
            if (data == "Siparişiniz alınmıştır.")
            {
         
                $("#msgLabel").html(data);
                $("#SuccessDiv").show();
            }
            else
            {
                $("#msgLabelError").html(data);
                $("#ErrorDiv").show();
                
            }
         
        }
    });
   

};
 

/* Payment page start */

function paymentTab(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
};

function CheckCharacter() {
    var characterName = $('#characterName').val();


    $.ajax({
        type: "POST",
        url: url + "/Payment/CheckCharacter/" + characterName,
        success: function (result) {

            $("#NickName").text(result.characterName);
            $("#UserLevel").text(result.userLevel);
            $("#RankTxt").text(result.rank);
            $("#UserId").val(result.userId);

        },
        error: function (message) {

            //alert(JSON.parse(message.responseText).Message);
        }

    })

    return false;
};


 
/* Payment page end */

//Max Size for Upload
var uploadFields = document.querySelectorAll('input[type=file]');

uploadFields.forEach(function (uploadField) {
    let inputName = uploadField.getAttribute('name');

    uploadField.onchange = function () {
        if (this.files[0].size > 2097152) {
            alert(inputName + " must be maximum 2MB!");
            this.value = "";
        };
    };
});