var tmpl = tmpl || { };
tmpl.game_details = (function ($)
{
    var template = $.template(
        '<div class="cards-stack">${cardsLeft} Cards Left</div>'
    );
    return template;
}($));