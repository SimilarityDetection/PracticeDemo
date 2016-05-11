function triggerBuriedPoint(obj) {
    var $this = $(obj);
    var data = {
        site: $this.data('site'),
        point: $this.data('point')
    };

    $.ajax({
        url: 'http://one.a.com/api/BuriedPoint?site=' + data.site + '&point=' + data.point,
        dataType: "jsonp"
    });
}