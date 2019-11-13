/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.min.js" />
angular.module('App.Utils', []).run(function ($rootScope, Utils) {
    $rootScope.getRootUrl = function () {
        return Utils.getRootUrl();
    }
}).factory('Utils', function ($http, $rootScope) {
    var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
    return {
        getRootUrl: function () {
            var url = $('#hdUrlPrefix').val();
            return url;
        },
        //InfoNotify: function () {
        //    $('button,input[type="button]"').not('.close').attr('disabled', 'disabled');
        //    $rootScope.notice = new PNotify({
        //        text: 'Processing action, please wait...',
        //        addclass: "stack-bottomleft",
        //        type: 'info',
        //        stack: stack-bottomleft,
        //        hide: false,
        //        styling: 'fontawesome'
        //    });
        //},
        finalResultNotify: function (obj, container) {
            $.unblockUI();
            $('button,input[type="button]"').removeAttr('disabled');
            App.alert({
                place: 'append', // append or prepent in container 
                type: obj.ResponseCode == 0 ? 'success' : 'error', // alert's type 
                message: obj.ResponseDesc, // alert's message
                close: true, // make alert closable 
                reset: true, // close all previouse alerts first 
                focus: true, // auto scroll to the alert after shown 
                //closeInSeconds: 30 // auto close after defined seconds
                //icon: 'fa fa-check-circle' // put icon class before the message
                container: container
            });
        },
        CloseNotify: function () {
            App.closeAlert();
        },
        PNotify: function (obj, releaseDisabled) {
            App.alert({
                place: 'append', // append or prepent in container 
                type: obj.flag == 0 ? 'success' : 'error', // alert's type 
                message: obj.strResponse, // alert's message
                close: true, // make alert closable 
                reset: true, // close all previouse alerts first 
                focus: true, // auto scroll to the alert after shown 
                //closeInSeconds: 5 // auto close after defined seconds
                //icon: 'fa fa-check-circle' // put icon class before the message
            });
        },
        ShowNotify: function (message, flag) {
            App.alert({
                place: 'append', // append or prepent in container 
                type: flag, // alert's type 
                message: message, // alert's message
                close: true, // make alert closable 
                reset: true, // close all previouse alerts first 
                focus: true, // auto scroll to the alert after shown 
                //closeInSeconds: 5 // auto close after defined seconds
                //icon: 'fa fa-check-circle' // put icon class before the message
            });
        },
        makeObjectNull: function (obj, overrides) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (overrides[key]) {
                        obj[key] = overrides[key];
                    } else {
                        obj[key] = null;
                    }
                }
            }
            for (var key in overrides) {
                if (!obj.hasOwnProperty(key)) {
                    obj[key] = overrides[key];
                }
            }
            return obj;
        },
        getSelectedRow: function (dt) {
            var obj, self = this;
            dt.find('tr').each(function (index, $row) {
                if ($(this).hasClass('active')) {
                    obj = dt.fnGetData($row);

                }
            });
            if (obj)
                return obj;
            self.PNotify({ flag: 1, Descp: "0 row selected" });
            return null;
        },
        getSelectedRows: function (dt) {
            var obj, self = this;
            var rows = [];
            dt.find('tr').each(function (index, $row) {
                if ($(this).hasClass('active')) {
                    rows.push(dt.fnGetData($row));
                }
            });
            if (rows.length)
                return rows;
            self.PNotify({ flag: 1, Descp: "0 row selected" });
            return null;
        },
        updateDataTable: function (dt, newOptions) {
            var objSettings = dt.fnSettings();
            angular.extend(objSettings, newOptions);
        },
        getEventMap: function () {
            return [
             { id: 1, type: "PeriodType", placeholder: "", val: null, class: 'col-sm-6', dataType: 'x' },
             { id: 2, type: "PeriodInterval", placeholder: "Interval", val: null, class: 'col-sm-6', dataType: 'x' },
             { id: 4, type: "MinIntVal", placeholder: "Min Value", val: null, class: 'col-sm-6', dataType: 'int' },
            { id: 8, type: "MaxIntVal", placeholder: "Max Value", val: null, class: 'col-sm-6', dataType: 'int' },
            { id: 16, type: "MinMoneyVal", placeholder: "Min Money", val: null, class: 'col-sm-6', dataType: 'money' },
            { id: 32, type: "MaxMoneyVal", placeholder: "Max Money", val: null, class: 'col-sm-6', dataType: 'money' },
            { id: 64, type: "MinDateVal", placeholder: "Min Date", val: null, class: 'col-sm-6', dataType: 'datePicker' },
            { id: 128, type: "MaxDateVal", placeholder: "Max Date", val: null, class: 'col-sm-6', dataType: 'datePicker' },
            { id: 256, type: "MinTimeVal", placeholder: "Min Time", val: null, class: 'col-sm-6', dataType: 'timePicker' },
            { id: 512, type: "MaxTimeVal", placeholder: "Max Time", val: null, class: 'col-sm-6', dataType: 'timePicker' },
            { id: 1024, type: "VarCharVal", placeholder: "Char Value", val: null, class: 'col-sm-12', dataType: 'string' }
            ]
        },
        getDaysByMonth: function (month) {
            var days = new Date(1904, month, 0).getDate();
            var result = new Array;
            for (var i = 1; i <= days; i++) {
                result.push({ Text: i, Value: i });
            }
            return result;
        }
        //getMonths: function () {
        //    return [
        //        { Value: 1, Text: 'Jan' },
        //        { Value: 2, Text: 'Feb' },
        //        { Value: 3, Text: 'Mar' },
        //        { Value: 4, Text: 'Apr' },
        //        { Value: 5, Text: 'May' },
        //        { Value: 6, Text: 'Jun' },
        //        { Value: 7, Text: 'Jul' },
        //        { Value: 8, Text: 'Aug' },
        //        { Value: 9, Text: 'Sep' },
        //        { Value: 10, Text: 'Oct' },
        //        { Value: 11, Text: 'Nov' },
        //        { Value: 12, Text: 'Dec' }
        //    ];
        //}
    }
})
    .directive('routingMenu', function ($compile, $timeout, $anchorScroll) {
        return {
            restrict: 'A',
            link: function (scope, $element, attrs) {
                scope.$on('$stateChanged', function (data, val) {
                    $(".leftMenu li").removeClass("active");
                    $(".pagekey_" + val).addClass('active');
                    var ulDiv = $(".pagekey_" + val).parent();
                    if ($(ulDiv).hasClass('sub-menu')) {
                        var livMainMenu = $(ulDiv).parent();
                        livMainMenu.addClass('active');
                    }
                    //var urlHash = window.location.hash;
                    //$('.leftMenu li a').each(function () {
                    //    var $this = $(this);
                    //    // if the current path is like this link, make it active
                    //    var attrValue = $this.attr('href');
                    //    if (attrValue && attrValue.endsWith(urlHash)) {
                    //        var liDiv = $($this).parent();
                    //        liDiv.addClass('active');

                    //        var ulDiv = $(liDiv).parent();
                    //        if ($(ulDiv).hasClass('sub-menu')) {
                    //            var livMainMenu = $(ulDiv).parent();
                    //            livMainMenu.addClass('active');
                    //        }
                    //    }
                    //})

                })
                $link = $element.find('.action > ul > li > a');
                $element.find('ul.sub-menu li a').on('click', function () {
                    var self = $(this).parent();
                    $element.find('ul li').each(function () {
                        $(this).removeClass('active');
                    });
                    self.addClass('active');

                    var liParent = $(self).parent().parent();
                    liParent.addClass('active');

                    $anchorScroll();
                });

                $element.find('li.no-submenu a').on('click', function () {
                    var self = $(this).parent();
                    $element.find('ul li').each(function () {
                        $(this).removeClass('active');
                        $(this).removeClass('open');
                    });
                    self.addClass('active');

                    $anchorScroll();
                });
            }
        }
    })

    .directive('routingTopMenu', function ($compile, $timeout, $anchorScroll) {
        return {
            restrict: 'A',
            link: function (scope, $element, attrs) {

                //scope.$on('stateChanged', function (data, val) {
                //    $(".pagekey").removeClass("active");
                //    $(".pagekey_" + val).addClass('active');
                //})

                //$link = $element.find('.action > ul > li > a');

                $element.find('ul li a').on('click', function () {
                    var self = $(this).parent();
                    $element.find('ul li').each(function () {
                        $(this).removeClass('active');
                    });
                    self.addClass('active');

                    var liParent = $(self).parent().parent();
                    liParent.addClass('active');

                    $('.leftMenu').find('ul li').each(function () {
                        $(this).removeClass('open');
                        $(this).removeClass('active');
                    });

                    $anchorScroll();
                });
            }
        }
    })

    .directive('onlyDigits', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9]/g, '');

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return parseInt(digits, 10);
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    })
    .directive('onlyTime', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/^(?:2[0-3]|[01]?[0-9]):[0-5][0-9]:[0-5][0-9]$/, '');  //!@#$%^&*()_+\-=\[\]{};'"\\|,.<>\/?

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return digits
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    })
    .directive('appModal', function ($compile, $timeout) {
        return {
            scope: { trigger: '=trigger' },
            restrict: 'AE',
            link: function (scope, $element, attrs) {
                $element.on('hidden.bs.modal', function (e) {
                    scope.trigger = false;
                })
                scope.$watch('trigger', function (newVal) {
                    if (newVal) {
                        $element.modal({ 'show': true, keyboard: true, backdrop: 'static' });
                    } else {
                        $element.modal('hide');
                    }
                });
                //$element.draggable({ handle: ".modal-head" });
            }
        }
    })
    .directive('confirmAction', function ($compile, $timeout) {
        return {
            scope: { trigger: '=trigger' },
            restrict: 'AE',
            link: function (scope, $element, attrs) {
                $element.on('hidden.bs.modal', function (e) {
                    scope.trigger = false;
                })
                scope.$watch('trigger', function (newVal) {
                    if (newVal) {
                        $element.modal({ 'show': true, keyboard: true, backdrop: 'static' });
                    } else {
                        $element.modal('hide');
                    }
                });
                $element.draggable({ handle: ".modal-head" });
                $element.find('btnConfirm').on('click', function () {
                    scope.$eval(attrs.execute);
                })
            }
        }
    })

    .directive('validationForm', function ($compile, $timeout, $rootScope) {
        return {
            restrict: 'EA',
            link: function (scope, $element, attrs) {
                var $form = $element.closest('form');
                $element.on('click', function (e) {
                    e.preventDefault();
                    $.validator.unobtrusive.parse($form);

                    var periodValid = true;
                    $('.form-group').removeClass('has-error1');// define for period date only

                    var formItems = $form[0];
                    jQuery.each(formItems, function (index, item) {
                        var period_to = $(item).attr('period_to');
                        if (period_to) {
                            var toDate = $('#' + period_to).val();
                            var fromDate = $(item).val();

                            if ($(item).is(':visible') && $(item).attr('is_required') === 'true') {
                                if (!fromDate || !toDate) {
                                    // Error - required
                                    periodValid = false;
                                    var formGrp = $(item).closest('.form-group');
                                    $(formGrp).addClass('has-error1');
                                    $(formGrp).find(".message-custom").text('From Date and To Date field are required.');
                                }
                            }

                            if (fromDate && toDate) {
                                var fromDateVal = moment(fromDate);
                                var toDateVal = moment(toDate);
                                if (fromDateVal > toDateVal) {
                                    // Error - from date < to date
                                    periodValid = false;
                                    var formGrp = $(item).closest('.form-group');
                                    $(formGrp).addClass('has-error1');
                                    $(formGrp).find(".message-custom").text('To date must be greater than From date.')
                                    //$('span[data-valmsg-for="' + $(item).attr('name') + '"]').text('To date must be greater than From date.')
                                }
                            }
                        }
                    }); 
                    

                    if ($form.valid() && periodValid) {
                        scope.$eval(attrs.customsubmit);
                        scope.$apply();
                    } else{
                        App.alert({
                            place: 'append', // append or prepent in container 
                            type: 'error', // alert's type 
                            message: 'Validation errors, please verify your inputs', // alert's message
                            close: true, // make alert closable 
                            reset: true, // close all previouse alerts first 
                            focus: true, // auto scroll to the alert after shown 
                            //closeInSeconds: 10 // auto close after defined seconds
                            //icon: 'fa fa-check-circle' // put icon class before the message
                        });
                    }
                });
            }
        }
    })
    .filter('doubleQuote', function () {
        return function (input) {
            input = '"' + input.replace(/^"*|"*$/, '') + '"';
            return input;
        }
    })
    .filter("jsonDate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(m[1]));
            else return null;
        };
    })
    .directive('dtable', function ($compile, $timeout, $rootScope, $window) {
        return {
            restrict: 'AE',
            scope: { options: '=' },
            link: function (scope, $element, attrs) {
                $.fn.dataTable.ext.legacy.ajax = true;
                /* Set the defaults for DataTables initialisation */
                $.extend(!0, $.fn.dataTable.defaults, {
                    sDom: "<'row'<'col-xs-6'l><'col-xs-6'f>r>t<'row'<'col-xs-6'i><'col-xs-6'p>>",
                    oLanguage: {
                        sLengthMenu: "_MENU_ records per page"
                    }
                });
                $.extend($.fn.dataTableExt.oStdClasses, {
                    sWrapper: "dataTables_wrapper form-inline",
                    sFilterInput: "form-control input-sm",
                    sLengthSelect: "form-control input-sm"
                });
                if ($.fn.dataTable.Api) {
                    $.fn.dataTable.defaults.renderer = "bootstrap";
                    $.fn.dataTable.ext.renderer.pageButton.bootstrap = function (e, t, n, r, i, s) {
                        var o = new $.fn.dataTable.Api(e),
                            u = e.oClasses,
                            a = e.oLanguage.oPaginate,
                            f, l, c = function (t, r) {
                                var h, p, d, v, m = function (e) {
                                    e.preventDefault();
                                    e.data.action !== "ellipsis" && o.page(e.data.action).draw(!1)
                                };
                                for (h = 0, p = r.length; h < p; h++) {
                                    v = r[h];
                                    if ($.isArray(v)) c(t, v);
                                    else {
                                        f = "";
                                        l = "";
                                        switch (v) {
                                            case "ellipsis":
                                                f = "&hellip;";
                                                l = "disabled";
                                                break;
                                            case "first":
                                                f = a.sFirst;
                                                l = v + (i > 0 ? "" : " disabled");
                                                break;
                                            case "previous":
                                                f = a.sPrevious;
                                                l = v + (i > 0 ? "" : " disabled");
                                                break;
                                            case "next":
                                                f = a.sNext;
                                                l = v + (i < s - 1 ? "" : " disabled");
                                                break;
                                            case "last":
                                                f = a.sLast;
                                                l = v + (i < s - 1 ? "" : " disabled");
                                                break;
                                            default:
                                                f = v + 1;
                                                l = i === v ? "active" : ""
                                        }
                                        if (f) {
                                            d = $("<li>", {
                                                "class": u.sPageButton + " " + l,
                                                "aria-controls": e.sTableId,
                                                tabindex: e.iTabIndex,
                                                id: n === 0 && typeof v == "string" ? e.sTableId + "_" + v : null
                                            }).append($("<a>", {
                                                href: "#"
                                            }).html(f)).appendTo(t);
                                            e.oApi._fnBindAction(d, {
                                                action: v
                                            }, m)
                                        }
                                    }
                                }
                            };
                        c($(t).empty().html('<ul class="pagination"/>').children("ul"), r)
                    }
                } else {
                    $.fn.dataTable.defaults.sPaginationType = "bootstrap";
                    $.fn.dataTableExt.oApi.fnPagingInfo = function (e) {
                        return {
                            iStart: e._iDisplayStart,
                            iEnd: e.fnDisplayEnd(),
                            iLength: e._iDisplayLength,
                            iTotal: e.fnRecordsTotal(),
                            iFilteredTotal: e.fnRecordsDisplay(),
                            iPage: e._iDisplayLength === -1 ? 0 : Math.ceil(e._iDisplayStart / e._iDisplayLength),
                            iTotalPages: e._iDisplayLength === -1 ? 0 : Math.ceil(e.fnRecordsDisplay() / e._iDisplayLength)
                        }
                    };
                    $.extend($.fn.dataTableExt.oPagination, {
                        bootstrap: {
                            fnInit: function (e, t, n) {
                                var r = e.oLanguage.oPaginate,
                                    i = function (t) {
                                        t.preventDefault();
                                        e.oApi._fnPageChange(e, t.data.action) && n(e)
                                    };
                                $(t).append('<ul class="pagination"><li class="prev disabled"><a href="#">&larr; ' + r.sPrevious + "</a></li>" + '<li class="next disabled"><a href="#">' + r.sNext + " &rarr; </a></li>" + "</ul>");
                                var s = $("a", t);
                                $(s[0]).bind("click.DT", {
                                    action: "previous"
                                }, i);
                                $(s[1]).bind("click.DT", {
                                    action: "next"
                                }, i)
                            },
                            fnUpdate: function (e, t) {
                                var n = 5,
                                    r = e.oInstance.fnPagingInfo(),
                                    i = e.aanFeatures.p,
                                    s, o, u, a, f, l, c = Math.floor(n / 2);
                                if (r.iTotalPages < n) {
                                    f = 1;
                                    l = r.iTotalPages
                                } else if (r.iPage <= c) {
                                    f = 1;
                                    l = n
                                } else if (r.iPage >= r.iTotalPages - c) {
                                    f = r.iTotalPages - n + 1;
                                    l = r.iTotalPages
                                } else {
                                    f = r.iPage - c + 1;
                                    l = f + n - 1
                                }
                                for (s = 0, o = i.length; s < o; s++) {
                                    $("li:gt(0)", i[s]).filter(":not(:last)").remove();
                                    for (u = f; u <= l; u++) {
                                        a = u == r.iPage + 1 ? 'class="active"' : "";
                                        $("<li " + a + '><a href="#">' + u + "</a></li>").insertBefore($("li:last", i[s])[0]).bind("click", function (n) {
                                            n.preventDefault();
                                            e._iDisplayStart = (parseInt($("a", this).text(), 10) - 1) * r.iLength;
                                            t(e)
                                        })
                                    }
                                    r.iPage === 0 ? $("li:first", i[s]).addClass("disabled") : $("li:first", i[s]).removeClass("disabled");
                                    r.iPage === r.iTotalPages - 1 || r.iTotalPages === 0 ? $("li:last", i[s]).addClass("disabled") : $("li:last", i[s]).removeClass("disabled")
                                }
                            }

                        }
                    })
                }
                if ($.fn.DataTable.TableTools) {
                    $.extend(!0, $.fn.DataTable.TableTools.classes, {
                        container: "DTTT btn-group",
                        buttons: {
                            normal: "btn btn-default",
                            disabled: "disabled"
                        },
                        collection: {
                            container: "DTTT_dropdown dropdown-menu",
                            buttons: {
                                normal: "",
                                disabled: "disabled"
                            }
                        },
                        print: {
                            info: "DTTT_print_info modal"
                        },
                        select: {
                            row: "active"
                        }
                    });
                    $.extend(!0, $.fn.DataTable.TableTools.DEFAULTS.oTags, {
                        collection: {
                            container: "ul",
                            button: "li",
                            liner: "a"
                        }
                    })
                };

                function mixIt(Scopeoptions) {

                    var defaults = {
                        "info": true,
                        "lengthChange": true,
                        "scrollX": true,
                        paging: true,
                        "searching": true,
                        searchable: true,
                        pageLength: 10,
                        "dom": 'C<"clear">lfrtip',
                        checkBox: false,
                        oLanguage: {
                            sEmptyTable: '<i style="font-size:140px;color:#eeeeee" class="fa fa-ban"></i>'
                        }
                    };
                    var options = {};
                    $.extend(options, $.extend({}, defaults, Scopeoptions));
                    scope.options.id = options.id;
                    options.fnRowCallback = function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        var $nRow = $(nRow);
                        var self = this;

                        $nRow.on('click', function () {
                            //if (options.edit) {
                            //    $nRow.closest('tbody').find('tr').removeClass('active');
                            //    $('#' + options.id + '-options').show();
                            //    if (options.checkBox) {
                            //        $nRow.closest('tbody').find('tr').each(function (index, $elm) {
                            //            var x = $($elm).find('td:first-child').find('input').prop('checked', false);
                            //        });
                            //    }
                            //    $nRow.removeClass('active').addClass('active');
                            //}
                            //if (options.rowClick)
                            //    options.rowClick.call(this, aData, nRow);

                            if (Scopeoptions.edit)
                                scope.$emit(Scopeoptions.edit.func, aData);
                        });

                        $nRow.on('dblclick', function () {
                            if (Scopeoptions.edit)
                                scope.$emit(Scopeoptions.edit.func, aData);
                        });

                        //if (Scopeoptions.edit) {
                        //    var $tbl = $rootScope.tables[options.id];
                        //    $tbl.find('tbody').addClass('tb-pointer');
                        //}

                        if (options.rowCallback) {
                            options.rowCallback.call(self, aData, nRow);
                        }
                        if (options.childTable) {
                            $nRow.find('td:first').on('click', function () {
                                $nRow.closest('tbody').find('tr').not($nRow).each(function () {
                                    var $row = $(this);
                                    if ($row.hasClass('parent')) {
                                        $row.removeClass('parent');
                                        $row.next().remove();
                                        $row.closest('tbody').find('tr.dynamic-created').remove();
                                    }
                                });
                                var $tbl = $rootScope.tables[options.id];
                                var row = $nRow.next().find('.childtable').length;
                                if (!row) {
                                    $nRow.addClass('parent');
                                    var $tbl = $(options.childTable.format(aData, options));//aaData options
                                    if (options.childTable.edit) {
                                        $tbl.find('tbody tr').on('click', function () {
                                            var self = this, $row = $(self), cells = [];
                                            $tbl.find('tr td').each(function () {
                                                $(this).removeClass('active');
                                            });
                                            $row.find('td').each(function (index, item) {
                                                $(this).removeClass().addClass('active');
                                            });
                                            $row.find('td').each(function (index, item) {
                                                cells.push(item.innerText);
                                            });
                                            options.childTable.edit.func.call(self, cells);
                                        });
                                    }
                                    $nRow.after($tbl);
                                } else {
                                    $nRow.removeClass('parent');
                                    var __ex = false;
                                    if ($nRow.closest('tbody').find('tr.dynamic-created').length)
                                        $nRow.closest('tbody').find('tr.dynamic-created').remove();
                                }
                            });
                        }
                    };
                    options.fnCreatedRow = function (nRow, aData, iDataIndex) {
                        if (options.checkBox) {
                            var $input = $('<input type="checkbox"/>');
                            $input.on('click', function (event) {
                                var checked = $input.prop('checked');
                                $(nRow).closest('tbody').find('tr').each(function (index, $elm) {
                                    //  var x = $($elm).find('td:first-child').find('input').prop('checked', false);
                                });
                                $input.prop('checked', checked);
                                if (checked) {
                                    $('#' + options.id + '-options').show();
                                    // $(nRow).closest('tbody').find('tr').removeClass('active');
                                    event.stopPropagation();
                                    $(nRow).removeClass('active').addClass('active');
                                } else {
                                    $(nRow).removeClass('active');
                                    event.stopPropagation();
                                }
                            });
                            $('td:first', nRow).html($input);
                        }
                        if (options.createdRow) {
                            options.createdRow.call(this, nRow, aData, iDataIndex);
                        }
                    }
                    options.fnInitComplete = function (oSettings, json) {
                        if (options.checkBox) {
                            var $table = $rootScope.tables[options.id];
                            var $input = $table.closest('.dataTables_scroll').find('.dataTables_scrollHead').find('input');
                            $input.on('click', function (event) {
                                if ($input.is(':checked')) {
                                    $table.find('tbody tr').each(function () {
                                        $(this).find('input').trigger('click');
                                        if (!$(this).find('input').is(':checked'))
                                            $(this).find('input').trigger('click');
                                    })
                                }
                                else {
                                    $table.find('tbody tr').each(function () {
                                        $(this).find('input').trigger('click');
                                        if ($(this).find('input').is(':checked'))
                                            $(this).find('input').trigger('click');
                                    })
                                }
                            });
                        }
                    }
                    return options;
                }
                scope.$watch('options', function (newVal) {
                    if (newVal) {
                        $timeout(function () {
                            var options = mixIt(scope.options);
                            $rootScope.tables[options.id] = $element.on('xhr.dt', function (e, settings, json) {
                                if (options.childTable) {
                                    options.childTable.fngroupOp.call(this, e, settings, json, options);
                                }
                                if (options.xhrDone) {
                                    options.xhrDone.call(this, e, settings, json);
                                }
                            }).dataTable(options);
                        });
                    }
                }, true);
                scope.$on('updateDataTable', function (data, val) {
                    if (scope.options && val.options.id == scope.options.id) {
                        var tbl = $rootScope.tables[val.options.id];
                        if (tbl) {
                            tbl.fnDestroy();
                        }
                        var options = mixIt(val.options);
                        $rootScope.tables[options.id] = $element.on('xhr.dt', function (e, settings, json) {
                            if (options.childTable) {
                                options.childTable.fngroupOp.call(this, e, settings, json);
                            }
                            if (options.xhrDone) {
                                options.xhrDone.call(this, e, settings, json);
                            }
                        }).dataTable(options);
                        data.preventDefault();
                    }
                });

                var resizeTimer;
                angular.element($window).bind('resize', function () {
                    if (scope.options && scope.options.id) {
                        clearTimeout(resizeTimer);
                        resizeTimer = setTimeout(function () {
                            console.log('resize');
                            $rootScope.tables[scope.options.id].fnAdjustColumnSizing();
                        }, 500);
                    }
                });
            }
        }
    })
    .directive('amount', ['$filter', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) return;

                ctrl.$parsers.unshift(function (viewValue) {

                    elem.priceFormat({
                        prefix: '',
                        centsSeparator: '.',
                        thousandsSeparator: ','
                    });

                    var removeCommaResult = elem[0].value.replace(/,/g, '');
                    return removeCommaResult;

                });
            }
        };
        //return {
        //    require: '?ngModel',
        //    link: function (scope, elem, attrs, ctrl) {
        //        if (!ctrl) return;
        //        //ctrl.$formatters.unshift(function (a) {
        //        //    return $filter(attrs.format)(ctrl.$modelValue)
        //        //});
        //        ctrl.$parsers.unshift(function (viewValue) {
        //            elem.priceFormat({
        //                prefix: '',
        //                centsSeparator: '.',
        //                thousandsSeparator: ','
        //            });
        //            return elem[0].value;
        //        });
        //    }
        //};
    }])
    .directive('treeView', function ($compile, $rootScope) {
        return {
            restrict: 'E',
            scope: {
                localNodes: '=model',
                //localClick: '&click'
            },
            link: function (scope, tElement, tAttrs, transclude) {

                var maxLevels = (angular.isUndefined(tAttrs.maxlevels)) ? 10 : tAttrs.maxlevels;
                var hasCheckBox = (angular.isUndefined(tAttrs.checkbox)) ? false : true;

                scope.showItems = [];

                scope.showHide = function (ulId) {
                    var hideThis = document.getElementById(ulId);
                    var showHide = angular.element(hideThis).attr('class');
                    angular.element(hideThis).attr('class', (showHide === 'show' ? 'hide' : 'show'));

                    var _icon = angular.element(event.target).closest('i');
                    if (showHide.indexOf('show') !== -1) {
                        $(_icon).removeClass('fa-minus');
                    } else {
                        $(_icon).addClass('fa-minus');
                    }
                }

                scope.showIcon = function (node) {
                    if (!angular.isUndefined(node.SubPages) && node.SubPages.length > 0) return true;
                }

                scope.checkIfChildren = function (node) {
                    if (!angular.isUndefined(node.SubPages)) return true;
                }

                /////////////////////////////////////////////////
                /// SELECT ALL CHILDRENS
                // as seen at: http://jsfiddle.net/incutonez/D8vhb/5/
                function tickChildrenNode(item) {
                    if (item.SubPages != null && item.SubPages.length > 0) {
                        for (var i in item.SubPages) {
                            item.SubPages[i].GroupStatus = item.GroupStatus;
                            if (item.SubPages[i].SubPages) {
                                tickChildrenNode(item.SubPages[i]);
                            }
                        }
                    }
                }

                function tickParentNode(_rootTree, _tree, node, loop) {
                    if (node.GroupStatus !== true) {
                        return;
                    }

                    var _checkTree = [];

                    if (loop === 0) {
                        _checkTree = _rootTree;
                    } else {
                        _checkTree = _tree;
                    }

                    for (var i in _checkTree) {
                        var item = _checkTree[i];

                        if (item.SubPages != null && item.SubPages.length > 0) {
                            for (var j in item.SubPages) {
                                if (item.SubPages[j].PageId === node.PageId) {
                                    item.GroupStatus = node.GroupStatus;
                                    tickParentNode(_rootTree, _tree, item, 0);
                                    //$("#node_" + item.PageId + "").prop('checked', true);
                                    return;
                                }
                            }
                            if (item.SubPages != null && item.SubPages.length > 0) {
                                tickParentNode(_rootTree, item.SubPages, node, 1);
                            }
                        }
                    }
                }

                scope.checkChange = function (node) {
                    tickChildrenNode(node);
                    tickParentNode($rootScope.TreeMatrix, $rootScope.TreeMatrix, node, 0);
                    //if (ticked) {
                    //    console.log('TICKED: ' + ticked.Descp)
                    //}
                    //if (node.SubPages && node.SubPages.length > 0) {
                    //    childCheckChange(node);
                    //} else {
                    //    var children = $rootScope.sourceTree;
                    //    for (var i = 0; i < children.length; i++) {

                    //        if (children[i].SubPages) {
                    //            for (var j = 0; j < children[i].SubPages.length; j++) {
                    //                if (node.PageId == children[i].SubPages[j].PageId)
                    //                {
                    //                    //$("#node_" + children[i].PageId + "").prop('checked', true);
                    //                    children[i].GroupStatus = true;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
                /////////////////////////////////////////////////

                function renderTreeView(collection, level, max) {                             
                    var text = '';

                    if (level === 1) {
                        text += '<li class="main-li" ng-repeat="n in ' + collection + '" >';
                    } else {
                        text += '<li ng-repeat="n in ' + collection + '" >';
                    }

                    if (level > 1) {
                        text += '<span ng-show=showIcon(n) class="show-hide" ng-click=showHide(n.PageId)><i class="fa fa-plus"></i></span>';
                        text += '<span ng-show=!showIcon(n) style="padding-right: 18px"></span>';

                        if (hasCheckBox) {
                            text += '<input id="node_{{n.PageId}}" class="tree-checkbox" type=checkbox ng-model=n.GroupStatus ng-change=checkChange(n)>';
                        }
                    }


                    //text += '<span class="edit" ng-click=localClick({node:n})><i></i></span>'

                    text += '<label>{{n.Descp}}</label>';

                    if (level < max) {
                        if (level == 1) {
                            text += '<ul id="{{n.PageId}}" class="show" ng-if=checkIfChildren(n)>' + renderTreeView('n.SubPages', level + 1, max) + '</ul></li>';
                        } else {
                            text += '<ul id="{{n.PageId}}" class="hide" ng-if=checkIfChildren(n)>' + renderTreeView('n.SubPages', level + 1, max) + '</ul></li>';
                        }
                    } else {
                        text += '</li>';
                    }

                    return text;
                }// end renderTreeView();

                try {
                    var text = '<ul class="tree-view-wrapper">';
                    text += renderTreeView('localNodes', 1, maxLevels);
                    text += '</ul>';
                    tElement.html(text);
                    $compile(tElement.contents())(scope);
                }
                catch (err) {
                    tElement.html('<b>ERROR!!!</b> - ' + err);
                    $compile(tElement.contents())(scope);
                }
            }
        };
    })
    .config(function ($provide, $httpProvider) {
        var rootUrl = $('#hdUrlPrefix').val();

        $provide.factory('CustomHttpInterceptor', function ($q, $rootScope, $window) {
            return {
                timer: null,
                request: function (config) {
                    if (config.method == 'POST' || config.method == 'PUT') {
                        this.timer = setTimeout(function () {
                            if ($rootScope.notice) {
                                $rootScope.notice.update({
                                    type: 'info',
                                    text: "<p>Server is taking longer than usual to <strong>respond</strong>. You can ignore this message and continue to wait for the response or refresh this page again.</p><p class='text-center'><button onclick='location.reload()' class='btn btn-white btn-sm'><i class='fa fa-refresh'></i>&nbsp;Refresh this page</button></p>",
                                    title: 'Uh oh',
                                    isLongRunning: true
                                });
                            }
                        }, 60000);
                    }
                    return config || $q.when(config);
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    return response || $q.when(response);
                },
                responseError: function (rejection) {
                    var error;
                    type = 'error';
                    var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
                    switch (rejection.status) {
                        case 500:
                            error = 'Internal server error';
                            break;
                        case 404:
                            error = 'location not found';
                            break;
                        case 403:
                            $window.location = rootUrl + '/InternalError/Error403';
                            break;
                        case 402:
                            error = 'bad gateway';
                            break;
                        case 0:
                            error = ""
                            type = "info";
                            break;
                        default:
                            error = "Internal server error";
                            break;

                    }
                    $('button').removeAttr('disabled');

                    $.unblockUI();
                    App.alert({
                        place: 'append', // append or prepent in container 
                        type: type, // alert's type 
                        message: error, // alert's message
                        close: true, // make alert closable 
                        reset: true, // close all previouse alerts first 
                        focus: true, // auto scroll to the alert after shown 
                        //closeInSeconds: 30 // auto close after defined seconds
                        //icon: 'fa fa-check-circle' // put icon class before the message
                        //container: container
                    });
                    return $q.reject(rejection);
                }
            };
        });
        $httpProvider.interceptors.push('CustomHttpInterceptor');
    })
    .directive('dataTableInputX', function ($compile) {
        return {
            restrict: 'AE',
            scope: { options: '@' },
            link: function (scope, $element, attrs) {
                scope.options.fnCreatedRow = function (nRow, aData, iDataIndex) {
                    // Bold the grade for all 'A' grade browsers
                    var $input = $('<input type="checkbox"/>');
                    $input.on('click', function (event) {
                        alert();
                        var checked = $input.prop('checked');
                        $(nRow).closest('tbody').find('tr').each(function (index, $elm) {
                            var x = $($elm).find('td:first-child').find('input').prop('checked', false);
                        });
                        $input.prop('checked', checked);
                        if (checked) {
                            $(nRow).closest('tbody').find('tr').removeClass('active');
                            event.stopPropagation();
                            $(nRow).removeClass('active').addClass('active');
                        } else {
                            $(nRow).removeClass('active');
                            event.stopPropagation();
                        }
                    });
                    $('td:first', nRow).html($input);
                }
                $element.dataTable($scope.options);
            }
        }

    })
    .directive("searchForm", function ($compile) {
        return {
            restrict: 'AE',
            scope: {},
            link: function (scope, $element, attrs) {

                $.widget("custom.catcomplete", $.ui.autocomplete, {
                    _renderMenu: function (ul, items) {
                        var self = this,
                            currentCategory = "";
                        $.each(items, function (index, item) {
                            self._renderItemData(ul, item);
                        });
                    }
                });
                var CurrentValue;
                var autoCompleteModel = function () {
                    this.minLength = 4;
                    this.currentPrefix = null;
                    this.determinePost = function (term) {
                        var self = this;
                        if (term.length < 3)
                            return false;
                        var prefix = term.substring(0, 3).toLowerCase();
                        var qstr = term.substr(3, term.length - 1);
                        for (i = 0; i < this.prefixes.length; i++) {
                            if (this.prefixes[i].shortCode == prefix) {
                                if (qstr.length >= this.prefixes[i].minChars) {
                                    self.currentPrefix = prefix;
                                    return true;
                                } else {
                                    return false;
                                }
                            }
                        }
                    }
                    this.prefixes = [
                      { shortCode: "cac", minChars: 1 }, { shortCode: "crd", minChars: 1 }, { shortCode: "nam", minChars: 1 }, { shortCode: "nic", minChars: 1 }, { shortCode: "oic", minChars: 1 },
                        { shortCode: "pas", minChars: 1 }, { shortCode: "pye", minChars: 1 }, { shortCode: "tax", minChars: 1 }, { shortCode: "apl", minChars: 1 }, { shortCode: "apc", minChars: 1 },
                    { shortCode: "apr", minChars: 1 }, { shortCode: "co1", minChars: 1 }, { shortCode: "co2", minChars: 1 }, { shortCode: "cor", minChars: 1 }, { shortCode: "vrn", minChars: 1 }
                    , { shortCode: "psn", minChars: 1 }, { shortCode: "mid", minChars: 1 }, { shortCode: "mac", minChars: 1 }, { shortCode: "bsn", minChars: 1 }, { shortCode: "sub", minChars: 1 },
                    { shortCode: "mdt", minChars: 1 }, { shortCode: "sid", minChars: 1 }, { shortCode: "apn", minChars: 1 }, { shortCode: "crn", minChars: 1 }],
                    this.source = function (request, response) {
                        var x = $('#hdUrlPrefix').val();
                        if (!objAutoComplete.determinePost(request.term))
                            return;
                        $.ajax({
                            url: $('#hdUrlPrefix').val() + '/Repository/_Querymeta',
                            dataType: "json",
                            cache: false,
                            data: {
                                name_startsWith: request.term
                            },
                            success: function (data) {
                                response($.map(data.theResult, function (item) {
                                    return {
                                        value: item.Link,
                                        desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest, prefix: request.term }
                                    }
                                }));
                            }
                        });
                    },
                    this.open = function () {
                        $('.ui-autocomplete').css('z-index', '3000');
                        $('.ui-autocomplete').css('width', '450');
                        $('.ui-autocomplete').css('max-height', '500');
                        return false;
                    },
                    this.focus = function (event, ui) {
                        //   $(this).val(objAutoComplete.currentPrefix + ui.item.desc.match);
                        $('.autocomplete-match-desc').css('color', '#797979');
                        $('.ui-menu-item').find('.ui-state-focus').find('.autocomplete-match-desc').css('color', 'white');
                        return false;
                    },
                    this.select = function (event, ui) {
                        var prefix = $('#hdUrlPrefix').val();
                        $(this).val(ui.item.desc.match);

                        scope.$emit('SearchItemSelected', ui);

                        return false;
                    },
                    this.renderItemData = function (ul, item) {
                        //if (item.desc == "Show More") {
                        //    return $("<li></li>").data("item.autocomplete", item).append(
                        //        '<a style="padding-left:70px;background-color:#DFDFDF"><span>Show More results for  <strong>"' + CurrentValue + '"</strong></span></a>')
                        //        .appendTo(ul);owa.edenowa
                        //}
                        //else {
                        return $("<li></li>").data("item.autocomplete", item).append(
                                            '<a style="border-bottom:1px solid #f3f3f3"><span class="autocomplete-match">' + item.desc.match + '</span> <span class="autocomplete-match-desc">' + item.desc.more + '</span></a>')
                                            .appendTo(ul);
                        //}

                        //$('.ui-autocomplete').css('z-index', '3000');
                    };

                }
                var objAutoComplete = new autoCompleteModel();

                $element.find('input').val('').catcomplete({
                    minLength: objAutoComplete.minLength,
                    source: objAutoComplete.source,
                    open: objAutoComplete.open,
                    focus: objAutoComplete.focus,
                    autoFocus: true,
                    select: objAutoComplete.select
                }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;

                $element.find('.selector li a').on('click', function () {
                    $element.find('input').val($(this).find('span').text()).focus();
                });



            }
        }

    })
    .directive("autocompleteForm", function ($compile) {
        return {
            restrict: 'AE',
            scope: {},
            link: function (scope, $element, attrs) {
                alert(attrs.autocompleteForm);
                $.widget("custom.catcomplete", $.ui.autocomplete, {
                    _renderMenu: function (ul, items) {
                        var self = this,
                            currentCategory = "";
                        $.each(items, function (index, item) {
                            self._renderItemData(ul, item);
                        });
                    }
                });
                var CurrentValue;
                var autoCompleteModel = function () {
                    this.minLength = attrs.minLength;
                    this.currentPrefix = null;

                    this.source = function (request, response) {
                        var x = $('#hdUrlPrefix').val();
                        $.ajax({
                            url: $('#hdUrlPrefix').val() + attrs.autocompleteForm,
                            dataType: "json",
                            cache: false,
                            data: {
                                name_startsWith: request.term
                            },
                            success: function (data) {
                                response($.map(data.theResult, function (item) {
                                    return {
                                        value: item.Text,
                                        desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest, prefix: request.term }
                                    }
                                }));
                            }
                        });
                    },
                    this.open = function () {
                        $('.ui-autocomplete').css('z-index', '3000');
                        $('.ui-autocomplete').css('width', '450');
                        $('.ui-autocomplete').css('max-height', '500');
                        return false;
                    },
                    this.focus = function (event, ui) {
                        //   $(this).val(objAutoComplete.currentPrefix + ui.item.desc.match);
                        $('.autocomplete-match-desc').css('color', '#797979');
                        $('.ui-menu-item').find('.ui-state-focus').find('.autocomplete-match-desc').css('color', 'white');
                        return false;
                    },
                    this.select = function (event, ui) {
                        var prefix = $('#hdUrlPrefix').val();
                        $(this).val(ui.item.desc.match);

                        scope.$emit('SearchItemSelected', ui);

                        return false;
                    },
                    this.renderItemData = function (ul, item) {
                        //if (item.desc == "Show More") {
                        //    return $("<li></li>").data("item.autocomplete", item).append(
                        //        '<a style="padding-left:70px;background-color:#DFDFDF"><span>Show More results for  <strong>"' + CurrentValue + '"</strong></span></a>')
                        //        .appendTo(ul);owa.edenowa
                        //}
                        //else {
                        return $("<li></li>").data("item.autocomplete", item).append(
                                            '<a style="border-bottom:1px solid #f3f3f3"><span class="autocomplete-match">' + item.desc.match + '</span> <span class="autocomplete-match-desc">' + item.desc.more + '</span></a>')
                                            .appendTo(ul);
                        //}

                        //$('.ui-autocomplete').css('z-index', '3000');
                    };

                }
                var objAutoComplete = new autoCompleteModel();

                $element.catcomplete({
                    minLength: objAutoComplete.minLength,
                    source: objAutoComplete.source,
                    open: objAutoComplete.open,
                    focus: objAutoComplete.focus,
                    autoFocus: true,
                    select: objAutoComplete.select
                }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;

                $element.find('.selector li a').on('click', function () {
                    $element.find('input').val($(this).find('span').text()).focus();
                });



            }
        }

    })

    .directive("autocompleteAccno", function ($compile) {
        return {
            restrict: 'AE',
            scope: {},
            link: function (scope, $element, attrs) {

                $.widget("custom.catcomplete", $.ui.autocomplete, {
                    _renderMenu: function (ul, items) {
                        var self = this,
                            currentCategory = "";
                        $.each(items, function (index, item) {
                            self._renderItemData(ul, item);
                        });
                    }
                });
                var CurrentValue;
                var autoCompleteModel = function () {
                    // this.minLength = 4;
                    this.currentPrefix = null;
                    this.determinePost = function (term) {
                        var self = this;
                        //if (term.length < 3)
                        //    return false;
                        var prefix = 'cac';// term.substring(0, 3).toLowerCase();
                        var qstr = term;//.substr(3, term.length - 1);
                        for (i = 0; i < this.prefixes.length; i++) {
                            if (this.prefixes[i].shortCode == prefix) {
                                if (qstr.length >= this.prefixes[i].minChars) {
                                    self.currentPrefix = prefix;
                                    return true;
                                } else {
                                    return false;
                                }
                            }
                        }
                    }
                    this.prefixes = [
                        { shortCode: "cac", minChars: 1 }, { shortCode: "crd", minChars: 1 }, { shortCode: "nam", minChars: 1 }, { shortCode: "nic", minChars: 1 }, { shortCode: "oic", minChars: 1 },
                        { shortCode: "pas", minChars: 1 }, { shortCode: "pye", minChars: 1 }, { shortCode: "tax", minChars: 1 }, { shortCode: "apl", minChars: 1 }, { shortCode: "apc", minChars: 1 },
                    { shortCode: "apr", minChars: 1 }, { shortCode: "co1", minChars: 1 }, { shortCode: "co2", minChars: 1 }, { shortCode: "cor", minChars: 1 }, { shortCode: "vrn", minChars: 1 }
                    , { shortCode: "psn", minChars: 1 }, { shortCode: "mid", minChars: 1 }, { shortCode: "mac", minChars: 1 }, { shortCode: "bsn", minChars: 1 }, { shortCode: "sub", minChars: 1 },
                    { shortCode: "mdt", minChars: 1 }, { shortCode: "sid", minChars: 1 }, { shortCode: "apn", minChars: 1 }, { shortCode: "crn", minChars: 1 }],
                    this.source = function (request, response) {
                        var x = $('#hdUrlPrefix').val();
                        if (!objAutoComplete.determinePost(request.term))
                            return;
                        $.ajax({
                            url: $('#hdUrlPrefix').val() + '/Repository/_Querymeta',
                            dataType: "json",
                            cache: false,
                            data: {
                                name_startsWith: 'cac' + request.term
                            },
                            success: function (data) {
                                response($.map(data.theResult, function (item) {
                                    return {
                                        value: item.Link,
                                        desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest, prefix: request.term }
                                    }
                                }));
                            }
                        });
                    },
                    this.open = function () {
                        $('.ui-autocomplete').css('z-index', '3000');
                        $('.ui-autocomplete').css('width', '450');
                        $('.ui-autocomplete').css('max-height', '500');
                        return false;
                    },
                    this.focus = function (event, ui) {
                        //   $(this).val(objAutoComplete.currentPrefix + ui.item.desc.match);
                        $('.autocomplete-match-desc').css('color', '#797979');
                        $('.ui-menu-item').find('.ui-state-focus').find('.autocomplete-match-desc').css('color', 'white');
                        return false;
                    },
                    this.select = function (event, ui) {
                        var prefix = $('#hdUrlPrefix').val();
                        $(this).val(ui.item.desc.match);

                        scope.$emit('SearchItemSelected', ui);

                        return false;
                    },
                    this.renderItemData = function (ul, item) {
                        //if (item.desc == "Show More") {
                        //    return $("<li></li>").data("item.autocomplete", item).append(
                        //        '<a style="padding-left:70px;background-color:#DFDFDF"><span>Show More results for  <strong>"' + CurrentValue + '"</strong></span></a>')
                        //        .appendTo(ul);owa.edenowa
                        //}
                        //else {
                        return $("<li></li>").data("item.autocomplete", item).append(
                                            '<a style="border-bottom:1px solid #f3f3f3"><span class="autocomplete-match">' + item.desc.match + '</span> <span class="autocomplete-match-desc">' + item.desc.more + '</span></a>')
                                            .appendTo(ul);
                        //}

                        //$('.ui-autocomplete').css('z-index', '3000');
                    };

                }
                var objAutoComplete = new autoCompleteModel();

                $element.find('input').val('').catcomplete({
                    minLength: objAutoComplete.minLength,
                    source: objAutoComplete.source,
                    open: objAutoComplete.open,
                    focus: objAutoComplete.focus,
                    autoFocus: true,
                    select: objAutoComplete.select
                }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;

                $element.find('.selector li a').on('click', function () {
                    $element.find('input').val($(this).find('span').text()).focus();
                });



            }
        }

    })

    //Usage:Add attributes: ng-confirm-message="Are you sure"? ng-confirm-click="takeAction()" function
    .directive('ngConfirmClick', [function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.bind('click', function () {
                    var message = attrs.ngConfirmMessage;
                    if (message && confirm(message)) {
                        scope.$apply(attrs.ngConfirmClick);
                    }
                });
            }
        }
    }])
    .directive('multiselectDropdown', function () {
        return {
            link: function (scope, element, attrs) {
                $.blockUI();
                element.multiselect({
                    buttonWidth: '100%',
                    dataWidth: '100%'
                    //buttonClass: 'btn',
                    //buttonWidth: 'auto',
                    //buttonContainer: '<div class="btn-group" />',
                    //maxHeight: false,
                    //buttonText: function (options) {
                    //    if (options.length == 0) {
                    //        return 'None selected <b class="caret"></b>';
                    //    }
                    //    else if (options.length > 3) {
                    //        return options.length + ' selected  <b class="caret"></b>';
                    //    }
                    //    else {
                    //        var selected = '';
                    //        options.each(function () {
                    //            selected += $(this).text() + ', ';
                    //        });
                    //        return selected.substr(0, selected.length - 2) + ' <b class="caret"></b>';
                    //    }
                    //}
                });
                $.unblockUI();
                // Watch for any changes to the length of our select element
                scope.$watch(function () {
                    return element[0].length;
                }, function () {
                    $.blockUI();
                    element.multiselect('rebuild');
                    $.unblockUI();
                });

                // Watch for any changes from outside the directive and refresh
                scope.$watch(attrs.ngModel, function () {
                    $.blockUI();
                    element.multiselect('refresh');
                    $.unblockUI();
                });
            }
        };
    })
;


