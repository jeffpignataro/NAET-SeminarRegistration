//$(function () {
//    var total;
//    var checked = $('input:checkbox').click(function (e) {
//        calculateSum();
//    });

//    function calculateSum() {
//        var $checked = $(':checkbox:checked');
//        total = 0.0;
//        $checked.each(function () {
//            total += parseFloat($(this).next().text());
//        });
//        $('#tot').text('Your Total Cost is: $' + total.toFixed(2).toString());
//    }
//});