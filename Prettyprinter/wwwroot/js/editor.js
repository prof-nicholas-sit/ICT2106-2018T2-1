$(function () {
    var textarea = document.getElementById("tooltest");

    $("#bold").click(function () {
        textarea.value += "**Bold Text Here**";
    });
    $("#italic").click(function () {
        textarea.value += "*Italic Text Here*";
    });
    $("#underline").click(function () {
        textarea.value += "__Underline Text Here__";
    });
    $("#strikethrough").click(function () {
        textarea.value += "~~Strikethrough Text Here~~";
    });
    $("#redtext").click(function () {
        textarea.value += "@@FF0000 Text Color @@";
    });
    $("#highlight").click(function () {
        textarea.value += "==Highlighted Text Here==";
    });
    $("#alignleft").click(function () {
        textarea.value += "*<-Align Left</-*";
    });
    $("#aligncenter").click(function () {
        textarea.value += "<->Align Center<->";
    });
    $("#alignright").click(function () {
        textarea.value += "->Align Right-/>";
    });
    $("#justify").click(function () {
        textarea.value += "<-->Justify<--/>";
    });
    $("#unorderedlist").click(function () {
        textarea.value += "+Item1+\n+Item2+\n";
    });
    $("#orderedlist").click(function () {
        textarea.value += "%1. Item1%\n%2. Item2%\n";
    });
    $("#hyperlink").click(function () {
        textarea.value += "[Label](http://www.url.com)";
    });

    //Emojis
    $("#smileEmo").click(function () {
        textarea.value += ":smile:";
    });
    $("#winkEmo").click(function () {
        textarea.value += ":wink:";
    });
    $("#joyEmo").click(function () {
        textarea.value += ":joy:";
    });
    $("#fearfulEmo").click(function () {
        textarea.value += ":fearful:";
    });
    $("#grinEmo").click(function () {
        textarea.value += ":grin:";
    });
    $("#angryEmo").click(function () {
        textarea.value += ":angry:";
    });
});

function prevDocs() {
    var prevDocsText = document.getElementById('prevDocsText');
    var textarea = document.getElementById("tooltest");
    var url = '@Url.Action("Preview", "DocumentController")';
    $.ajax({
        url: url,
        data: { content: textarea.value }
    }).done(function () {
        alert("olmlasda");
    })
    if (prevDocsText.className == 'hide') {  //check if classname is hide 
        prevDocsText.style.display = "block";
        prevDocsText.readOnly = true;
        prevDocsText.className = 'show';
    } else {
        prevDocsText.style.display = "none";
        prevDocsText.className = 'hide';
    }
}

function markdownSyntax() {
    var markdownSyntax = document.getElementById('markdownsyntax');

    if (markdownSyntax.className == 'hide') {  //check if classname is hide 
        markdownSyntax.style.display = "block";
        markdownSyntax.className = 'show';
    } else {
        markdownSyntax.style.display = "none";
        markdownSyntax.className = 'hide';
    }
    return false;
}