set.board.cardsPanel = (function (board) {
    var my = {};

    var selectedCardsCount = 0;
    var selectCard = function (card) {
        if (!$(card).hasClass('selected')) {
            $(card).addClass('selected');
            selectedCardsCount++;
        }
    };
    
    my.startSelection = function (selectionCompletedCallback) {
        $('#cards-panel .card').bind('click', function () {
            selectCard($(this));
            if (selectedCardsCount == 3) {
                var selectedCards = $('#cards-panel .card.selected');
                // reset 
                selectedCardsCount = 0;
                selectedCards.removeClass('selected');
                $('#cards-panel .card').unbind('click');
                selectionCompletedCallback($.map(selectedCards, function(value) {
                    return {
                        Color: $(value).attr('data-color'),
                        Filling: $(value).attr('data-filling'),
                        Shape: $(value).attr('data-shape'),
                        ShapeCount: $(value).attr('data-count'),
                    };
                }));
            }
        });
    };

    my.removeSelectedCards = function() {
        $.remove('#cards-panel .card.selected');
    };

    my.clearSelection = function() {
        $('#cards-panel .card.selected').removeClass("selected");
    };
    
    return my;
}(set.board));