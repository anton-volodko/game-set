var tmpl = tmpl || { };
tmpl.player = (function ($)
{
    var template = $.template(
        '<div class="player-info{{if State == \'HavingFall\'}} having-fall{{/if}}">${User.Name}</div>'
    );
    return template;
}($));