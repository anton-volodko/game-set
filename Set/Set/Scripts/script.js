$(function($){
    $('button.start-game').bind('click', 
    function () {
        var page = $(this).attr('data-page');
        if (page != undefined) window.location = page;
    });

    $.postJSON = function(url, data, callback) {
        ///	<summary>
        ///     &#10;Loads JSON data using an HTTP GET request.
        ///	</summary>
        ///	<param name="url" type="String">The URL of the JSON data to load.</param>
        ///	<param name="data" optional="true" type="Map">Key/value pairs that will be sent to the server.</param>
        ///	<param name="callback" optional="true" type="Function">The function called when the AJAX request is complete if the data is loaded successfully.  It should map function(data, textStatus) such that this maps the options for this AJAX request.</param>
        ///	<returns type="XMLHttpRequest" />

        return jQuery.post(url, data, callback, "json");
    };
    
}($));