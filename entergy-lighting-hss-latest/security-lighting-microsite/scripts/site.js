var regions = [
  'no-region',
  'mississippi',
  'louisiana',
  'texas',
  'arkansas',
  'new-orleans'
];

function selectFixture(t) {
    var $div = t.parent();

    $.get(t.attr('href') + '/summary', function (partial) {
        $('#fixture-summary-container').html(partial);

        $div.siblings().removeClass('selected-fixture');

        $div.addClass('selected-fixture');
    });
}
function recalcAspectRatios() {
    $(".aspect-ratio").each(function () {
        var ratio = $(this).data('aspect-ratio');
        var thisWidth = parseInt($(this).css('width'));
        var thisHeight = thisWidth * ratio;
        $(this).css('height', thisHeight);
    });
}



var navHeight;
var windowHeight;
var recalcNav = function () {
    navHeight = parseInt($('#nav-menu').css('height')) + parseInt($('.navbar-header').css('height'));
    windowHeight = parseInt($(window).height());
    if (navHeight > windowHeight) {
        $('body').addClass('ab-nav');
    } else {
        $('body').removeClass('ab-nav');
    }
};

recalcNav();
recalcAspectRatios();

if (!sessionStorage.getItem('toggle')) {
    sessionStorage.setItem('toggle', 'commercial');
}

$(window).resize(function () {
    recalcNav();
    recalcAspectRatios();
});

$(window).scroll(function () {
    recalcNav();
});

$('.flexslider').flexslider();

lightbox.option({
    'resizeDuration': 200,
    'wrapAround': true
});


// Fixture selector
$('.fixture-selector').on('click', 'a', function (evt) {
    evt.preventDefault();

    if (parseInt($(window).width()) < 768) {
        animateScrollTo($('#available-fixtures'));
    }

    selectFixture($(this));

});


if ($('body').attr('id') == 'home-fixture') {
    var fixtype = $('header').attr('data-fixture-type');
    if (sessionStorage.getItem('toggle') === 'residential') {
        $('#available-fixtures').removeClass('commercial-fixtures residential-fixtures').addClass('residential-fixtures');
    }
    else {
        $('#available-fixtures').removeClass('commercial-fixtures residential-fixtures').addClass('commercial-fixtures');
    }
    selectFixture($('fixture-type-' + fixtype + ' a'));
}


var animateScrollTo = function ($element) {
    var scrollTarget = $element.offset().top;

    var duration = (Math.abs($(window).scrollTop() - scrollTarget)) / $('body').height() * 3000;

    $('html, body').animate({
        scrollTop: scrollTarget - $('.navbar').height()
    }, duration, 'easeInOutExpo');

    window.location.hash = $element.selector;
};


recalcFixtureAvailability = function () {
    $('.fixture-selector div').removeClass('com-res-fix res-fix com-fix');
    $('.fixture-selector div').addClass('com-res-fix');

    var comFixtures = ['.fixture-type-shoebox',
						'.arkansas .fixture-type-traditionaire',
						'.louisiana .fixture-type-traditionaire',
						'.new-orleans .fixture-type-traditionaire',
						'.texas .fixture-type-traditionaire',
						'.arkansas .fixture-type-acorn',
						'.louisiana .fixture-type-acorn',
						'.new-orleans .fixture-type-acorn',
						'.texas .fixture-type-acorn'

    ];
    var hasLED = false;
    comFixtures.forEach(function (value, index, ar) {
        $(value).addClass('com-fix').removeClass('com-res-fix');
        if ($(value).children('led-badge').length > 0) {
            hasLED = true;
        }
    });
};

recalcFixtureAvailability();

$('.footer a.available-fixtures-pagelink').on('click', function (evt) {
    evt.preventDefault();
    $('#available-fixtures').removeClass('commercial-fixtures residential-fixtures')
	                      .addClass($(this).data('filter'));

    if ($(this).data('filter') == 'commercial-fixtures') {
        selectFixture($('#available-fixtures .fixture-selector div.com-fix:first a'));
    } else {
        selectFixture($('#available-fixtures .fixture-selector div.com-res-fix:first a'));
    }
});


$('#nav-menu a.internal, .footer a.available-fixtures-pagelink').on('click', function (evt) {
    evt.preventDefault();

    if ($('body').hasClass('no-region')) {
        $('.available-fixtures-pagelink').attr('href', '#region-selection');
    }
    else {
        $('.available-fixtures-pagelink').attr('href', '#available-fixtures');
    }


    var $target = $(this.href.match(/#.*/)[0]);
    
    animateScrollTo($target);
});

$('a[data-custom-toggle="collapse"]').click(function (e) {
    e.preventDefault();
    $($(this).data('parent')).find('.collapse').removeClass('in');
    $($(this).attr('href')).addClass('in');
});

$('#available-fixtures .fixture-filter .toggle-switch').on('touchstart click', function (evt) {
    evt.preventDefault();
    $('#available-fixtures').toggleClass('commercial-fixtures').toggleClass('residential-fixtures');

    if ($('#available-fixtures').hasClass('commercial-fixtures')) {
        sessionStorage.setItem("toggle", "commercial");
        selectFixture($('#available-fixtures .fixture-selector div.com-fix:first a'));
    } else {
        sessionStorage.setItem("toggle", "residential");
        selectFixture($('#available-fixtures .fixture-selector div.com-res-fix:first a'));
    }


});

$('#available-fixtures .fixture-filter a.not-toggle').on('click', function (evt) {
    evt.preventDefault();

    $('#available-fixtures').removeClass('commercial-fixtures residential-fixtures')
                            .addClass($(this).data('filter'));

    if ($(this).data('filter') == 'commercial-fixtures') {
        sessionStorage.setItem("toggle", "commercial");
    } else {
        sessionStorage.setItem("toggle", "residential");
        selectFixture($('#available-fixtures .fixture-selector div.com-res-fix:first a'));
    }
});

$('.thumbnails img').on('click', function () {
    $('.installation-viewer img:first').attr('src', $(this).attr('src'));
});

var monitor_scroll = function (evt) {
    var firstVisibleSection = $('header, section:visible').filter(':in-viewport(78)').first();

    $('#current-section-name').text(firstVisibleSection.data('nav-text'));
}

monitor_scroll();

$(window).on('scroll', $.throttle(250, monitor_scroll));


if ($('#available-fixtures').hasClass('commercial-fixtures')) {
} else {
    selectFixture($('#available-fixtures .fixture-selector div.com-res-fix:first a'));
}

window.entergyContactUs = function (state) {
    $('#FixtureRecommenderResult').val(JSON.stringify(state));
    $('#contact-show').hide();
    $('.contact-form').slideDown(500, 'easeInOutSine');
    $('html, body').animate({scrollTop: $('#contact').offset().top}, 1000);
};

// Contact form collapse/expand
$('#contact-show').on('click', function () {
    $(this).hide();

    $('.contact-form').slideDown(500, 'easeInOutSine');
});

$('.contact-form .close-button').click(function (evt) {
    evt.preventDefault();
    $('.contact-form').slideUp(500, 'easeInOutSine', function () { $('#contact-show').fadeIn(); });
});

$('.contact-link').on('click', function (evt) {
    evt.preventDefault();

    //Reset the visibiltiy of the contact form
    resetContact();

    animateScrollTo($('section.contact'));
    $('.contact-form').slideDown(500, 'easeInOutSine');
});

$('.nav-contact-us, #faq-contact-us').on('click', function () {
    $('#contact-show').click();

    //Reset the visibiltiy of the contact form
    resetContact();

    animateScrollTo($('section.contact'));
});

function resetContact() {
    $('#contact-show').hide();
    if ($('.contact-success').is(":visible")) {
        $('.contact-success').hide();
    }
    if ($('.contact-failure').is(":visible")) {
        $('.contact-failure').hide();
    }
    $('#contact-intro').show();
    $('.contact-form').each(function () {
        this.reset();
    });
}

$(".contact-form").on('submit', function (evt) {
    evt.preventDefault();

    $(this).parsley().validate();

    if ($(this).parsley().isValid()) {
        var model = {};
        $('.contact-form input, .contact-form select').map(function () {
            model[this.id] = $(this).val();
        });
        $.ajax($('.contact-form').attr('action'), {
            data: model,
            type: 'post'
        })
        .done(function (response) {
            console.log("response = ", response);
            if (response == "success") {
                $('#contact-intro').hide();
                $('.contact-form').slideUp(500, 'easeInOutSine', function () { $(".contact-success").show(); });
            } else {
                $(".contact-failure").slideDown(500, 'easeInOutSine');
            }
        })
        .fail(function (response) {
            $(".contact-failure").slideDown(500, 'easeInOutSine');
        });
    }
});
