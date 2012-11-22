var set = set || {};
set.board = (function ($) {
    var my = { };
    var playersSource = $("#players-info").attr('data-source');
    my.updatePlayers = function () {
        $.getJSON(playersSource, null, function(players) {
            $("#players-info").empty();
            $.tmpl(tmpl.player, players).appendTo("#players-info");
        }).fail(
            function (jqXHR, textStatus, err) {
                alert('Connection error');
            });
    };

    my.updatePlayers();

    var detailsSource = $("#game-details").attr('data-source');
    my.updateDetails = function() {
        $.postJSON(detailsSource, null, function(details) {
            $("#game-details").empty();
            $.tmpl(tmpl.game_details, details).appendTo("#game-details");
        }).fail(
            function(jqXHR, textStatus, err) {
                alert('Connection error');
            });
    };
    my.updateDetails();

    var boardCardsSource = $("#cards-panel").attr('data-source');
    my.updateCardsPanel = function () {
        $.postJSON(boardCardsSource, null, function (cards) {
            var cp = $("#cards-panel");
            // set rotation and choose current style depended on cards count
            var rotation = 90;
            cp.removeClass("twenty-one-cards eighteen-cards fifteen-cards twelve-cards");
            if (cards.length > 18) {
                cp.addClass('twenty-one-cards');
                rotation = 0;
            } else if (cards.length > 15) cp.addClass('eighteen-cards');
            else if (cards.length > 12) cp.addClass('fifteen-cards');
            else if (cards.length > 9) cp.addClass('twelve-cards');
            // update controls
            cp.empty();
            $.tmpl(tmpl.card, cards, {Rotation: rotation}).appendTo(cp);
        }).fail(
            function (jqXHR, textStatus, err) {
                alert('Connection error');
            });
    };
    my.updateCardsPanel();

    var addCardsButton = $("button#add-cards");
    var addCardsActionUrl = addCardsButton.attr('data-action');
    var addCards = function () {
        $.post(addCardsActionUrl, null, function () {
            my.updateDetails();
            my.updateCardsPanel();
        }).fail(function(jqXHR, textStatus, err) {
            alert('Can\'t add cards');
        });
    };
    addCardsButton.bind('click', addCards);

    var raiseHandButton = $("button#raise-hand");
    var raiseHandActionUrl = raiseHandButton.attr('data-action');
    var raiseHand = function () {
        // disable buttons so none can do any action
        addCardsButton.hide();
        raiseHandButton.hide();
        // make cards selectable
        set.board.cardsPanel.startSelection(function (selectedCards) {
            my.checkSet(selectedCards);
            addCardsButton.show();
            raiseHandButton.show();
        });
        $.post(raiseHandActionUrl, null, function() {
            my.updateDetails();
        }).fail(function(jqXHR, textStatus, err) {
            alert('Can\'t raise hand');
            addCardsButton.show();
            raiseHandButton.show();
        });
    };

    raiseHandButton.bind('click', raiseHand);

    var clearSelection = function() {
        set.board.cardsPanel.clearSelection();
    };

    function toAspArray(arr, arrName) {
        var args = {};
        $.each(arr, function (ind, val) {
            args[arrName + "[" + ind + "]"] = val;
        });
        return args;
    }

    my.checkSet = function (selectedCards) {
        var actionUrl = $("div#board").attr('data-action-check');
        var params = toAspArray(selectedCards, "set");
        $.post(actionUrl, params, function (isCorrect) {
            my.updateDetails();
            my.updatePlayers();
            if (isCorrect) {                
                my.updateCardsPanel();
            } else clearSelection();
        }).fail(function (jqXHR, textStatus, err) {
            alert('Can\'t check set');
            clearSelection();
        });
    };
    return my;
}($));