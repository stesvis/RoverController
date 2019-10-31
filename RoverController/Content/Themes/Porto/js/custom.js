/* Add here all your JS customizations */

$(document).ready(function () {

    var isOnIOS = navigator.userAgent.match(/iPad/i) || navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i);
    //var eventName = isOnIOS ? "pagehide" : "beforeunload";

    //window.addEventListener(eventName, showLoading);

    //$(window).bind('beforeunload', showLoading);
    window.addEventListener('beforeunload', showLoading);
    //window.onbeforeunload = showLoading;

    $('#sidebar-left').on('click', '.nav-link', function () {
        if (isOnIOS) {
            showLoading();
        }
    });

    $('button[type=submit]').on('click', function () {
        var form = $(this).closest('form');
        console.log(form);

        if (form.valid() && isOnIOS) {
            showLoading();
        }
    });

    $(document).on('click', '.redirect', function () {
        console.log('redirect');
        if (isOnIOS) {
            showLoading();
        }
    });

    $('.breadcrumbs').on('click', 'a', function () {
        if (isOnIOS) {
            showLoading();
        }
    });

    //console.log('GET sidebar-status = ' + localStorage.getItem('sidebar-status'));

    if (localStorage.getItem('sidebar-status') === 'sidebar-left-collapsed') {
        $('html').addClass('sidebar-left-collapsed');
    }

    $(".dropdown-menu a").click(function () {
        $(this).closest(".dropdown-menu").prev().dropdown("toggle");
    });

    //$(document).on('click', '.js-navSearchButton', function (e) {
    //    alert('you clicked me');
    //});

    /* Modal Dismiss */
    $(document).on('click', '.modal-dismiss', function (e) {
        e.preventDefault();
        $.magnificPopup.close();
    });

    $('.modal-dismiss').on('click', function (e) {
        e.preventDefault();
        $.magnificPopup.close();
    });

    $.magnificPopup.instance._onFocusIn = function (e) {
        // Do nothing if target element is select2 input
        if ($(e.target).hasClass('select2-search__field')) {
            return true;
        }
        // Else call parent method
        $.magnificPopup.proto._onFocusIn.call(this, e);
    };

    // Validate a form in a Magnific Popup
    //$(document).on('click', '.modal-confirm', function (e) {
    //    e.preventDefault();

    //    var form = $('.modal-confirm').closest('.card-footer').siblings('.card-body').find('form');

    //    if (form) {
    //        $(form).submit();

    //        var validator = $(form).validate();
    //        validator.showErrors();

    //        if ($(form).valid()) {
    //            $.magnificPopup.close();
    //        }
    //        else {
    //            e.stopPropagation();
    //        }
    //    }
    //});

    var select2_open;
    // open select2 dropdown on focus
    $(document).on('focus', '.select2-selection--single', function (e) {
        var select2_open = $(this).parent().parent().siblings('select');
        select2_open.select2('open');
    });


    // fix for ie11
    if (/rv:11.0/i.test(navigator.userAgent)) {
        $(document).on('blur', '.select2-search__field', function (e) {
            select2_open.select2('close');
        });
    }

    // keeps the sidebar collapsed
    $(document).on('click', '.sidebar-toggle', function (e) {
        //console.log('GET sidebar-status = ' + localStorage.getItem('sidebar-status'));
        if ($('html').hasClass('sidebar-left-collapsed')) {
            localStorage.setItem('sidebar-status', 'sidebar-left-collapsed');
            //console.log('SET sidebar-status = ' + localStorage.getItem('sidebar-status'));
        }
        else {
            localStorage.setItem('sidebar-status', '');
            //console.log('SET sidebar-status = ' + localStorage.getItem('sidebar-status'));
        }
    });

    $(document).on('click', '.js-save', function (e) {
        $('#SaveMode').val(0);
    });

    $(document).on('click', '.js-save-exit', function (e) {
        $('#SaveMode').val(1);
    });

    $(document).on('click', '.js-cancel', function (e) {
        history.back();
    });


    //--------------------------- Preferences ---------------------------//
    $('#userbox').on('click', '.js-preferences', function (e) {

        e.preventDefault();
        e.stopPropagation();

        $.magnificPopup.open({
            type: 'ajax',
            modal: true,

            fixedContentPos: false,
            fixedBgPos: true,

            overflowY: 'auto',

            preloader: false,

            midClick: true,
            removalDelay: 300,
            mainClass: 'my-mfp-zoom-in',

            closeOnContentClick: false,
            closeOnBgClick: true,
            items: {
                src: $(this).attr('href'),
            },
            callbacks: {

                ajaxContentAdded: function () {

                    if ($.isFunction($.fn['select2'])) {

                        $(function () {
                            $('[data-plugin-selectTwo]').each(function () {
                                var $this = $(this),
                                    opts = {};

                                var pluginOptions = $this.data('plugin-options');
                                if (pluginOptions)
                                    opts = pluginOptions;

                                $this.themePluginSelect2(opts);
                            });
                        });
                    }
                }
            }
        });
        return false;
    });

    $.extend(theme.PluginTimePicker.defaults, {
        disableMousewheel: true,
        icons: {
            up: 'fas fa-chevron-up',
            down: 'fas fa-chevron-down'
        },
        defaultTime: false
    });

});

function showLoading() {
    $("#loading").fadeIn();

    var opts = {
        lines: 12, // The number of lines to draw
        length: 7, // The length of each line
        width: 4, // The line thickness
        radius: 10, // The radius of the inner circle
        color: '#000', // #rgb or #rrggbb
        speed: 1, // Rounds per second
        trail: 60, // Afterglow percentage
        shadow: false, // Whether to render a shadow
        hwaccel: false // Whether to use hardware acceleration
    };
    var target = document.getElementById('loading');
    var spinner = new Spinner(opts).spin(target);
}

function hideLoading() {
    $("#loading").fadeOut();
}

function getErrorMessage(error) {
    var exceptionMessage = '';
    if (error.responseJSON.exceptionMessage != undefined) {
        exceptionMessage = error.responseJSON.exceptionMessage;
    }

    return exceptionMessage;
}
