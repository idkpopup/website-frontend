﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function registerUser(clickSource) {
    var subscribeToMailingList = false;
    if (document.getElementById('mailist').checked === true) {
        subscribeToMailingList = true;
    } else {
        if (document.getElementById('subscribeEmail').value != "") {
            subscribeToMailingList = true;
        }
    }

    var contact = {
        "FirstName": document.getElementById('firstName').value,
        "LastName": document.getElementById('lastName').value,
        "Email": document.getElementById('email').value,
        "Phone": document.getElementById('phone').value,
        "Website": document.getElementById('website').value,
        "Company": document.getElementById('company').value,
        "Message": document.getElementById('message').value,
        "MailingList": subscribeToMailingList
    };

    contact.TimeZone = new Date().getTimezoneOffset();
    
    $.ajax({
            url: 'api/register',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(contact),
            dataType: 'json'
        })
        .done(function (result) {
            //TODO: check for error

            //write contact to cookies
            setCookie("contact", contact.Email);
            
            if (clickSource === 'contactUs') {
                //diable form, hide button, show thank you
                document.getElementById('firstName').disabled = true;
                document.getElementById('lastName').disabled = true;	
                document.getElementById('email').disabled = true;	
                document.getElementById('phone').disabled = true;	
                document.getElementById('website').disabled = true;	
                document.getElementById('company').disabled = true;
                document.getElementById('message').disabled = true;
                document.getElementById('mailing').style.display = "none";
                document.getElementById('contactUs').style.display = "none";
                document.getElementById('thankYou').style.display = "block";
            }
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        setCookie("failed reason:" + JSON.stringify(jqXHR) + "\n" + JSON.stringify(textStatus) + "\n" + JSON.stringify(errorThrown));
    });
};

if (document.getElementById('contactUs') !== null) {
    document.getElementById('contactUs').onclick = function() {
        registerUser('contactUs');
    }
}

if (document.getElementById('subscribe') !== null) {
    document.getElementById('subscribe').onclick =  function() {
       registerUser('subscribe');
    }
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + JSON.stringify(cvalue) + ";" + expires + ";path=/";
};

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
        c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
        }
    }
    return "";
};

