﻿/*
PNotify 2.0.1 sciactive.com/pnotify/
(C) 2014 Hunter Perrin
license GPL/LGPL/MPL
*/
/*
 * ====== PNotify ======
 *
 * http://sciactive.com/pnotify/
 *
 * Copyright 2009-2014 Hunter Perrin
 *
 * Triple licensed under the GPL, LGPL, and MPL.
 * 	http://gnu.org/licenses/gpl.html
 * 	http://gnu.org/licenses/lgpl.html
 * 	http://mozilla.org/MPL/MPL-1.1.html
 */

// Uses AMD or browser globals for jQuery.
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as a module.
        define('pnotify', ['jquery'], factory);
    } else {
        // Browser globals
        factory(jQuery);
    }
}(function ($) {
    var default_stack = {
        dir1: "down",
        dir2: "left",
        push: "bottom",
        spacing1: 25,
        spacing2: 25,
        context: $("body")
    };
    var timer, // Position all timer.
		body,
		jwindow = $(window);
    // Set global variables.
    var do_when_ready = function () {
        body = $("body");
        PNotify.prototype.options.stack.context = body;
        jwindow = $(window);
        // Reposition the notices when the window resizes.
        jwindow.bind('resize', function () {
            if (timer)
                clearTimeout(timer);
            timer = setTimeout(function () { PNotify.positionAll(true) }, 10);
        });
    };
    PNotify = function (options) {
        this.parseOptions(options);
        this.init();
    };
    $.extend(PNotify.prototype, {
        // The current version of PNotify.
        version: "2.0.1",

        // === Options ===

        // Options defaults.
        options: {
            // The notice's title.
            title: false,
            // Whether to escape the content of the title. (Not allow HTML.)
            title_escape: false,
            // The notice's text.
            text: false,
            // Whether to escape the content of the text. (Not allow HTML.)
            text_escape: false,
            // What styling classes to use. (Can be either jqueryui or bootstrap.)
            styling: "bootstrap3",
            // Additional classes to be added to the notice. (For custom styling.)
            addclass: "",
            // Class to be added to the notice for corner styling.
            cornerclass: "",
            // Display the notice when it is created.
            auto_display: true,
            // Width of the notice.
            width: "300px",
            // Minimum height of the notice. It will expand to fit content.
            min_height: "16px",
            // Type of the notice. "notice", "info", "success", or "error".
            type: "notice",
            // Set icon to true to use the default icon for the selected
            // style/type, false for no icon, or a string for your own icon class.
            icon: true,
            // Opacity of the notice.
            opacity: 1,
            // The animation to use when displaying and hiding the notice. "none",
            // "show", "fade", and "slide" are built in to jQuery. Others require jQuery
            // UI. Use an object with effect_in and effect_out to use different effects.
            animation: "fade",
            // Speed at which the notice animates in and out. "slow", "def" or "normal",
            // "fast" or number of milliseconds.
            animate_speed: "slow",
            // Specify a specific duration of position animation
            position_animate_speed: 500,
            // Display a drop shadow.
            shadow: true,
            // After a delay, remove the notice.
            hide: true,
            // Delay in milliseconds before the notice is removed.
            delay: 8000,
            // Reset the hide timer if the mouse moves over the notice.
            mouse_reset: true,
            // Remove the notice's elements from the DOM after it is removed.
            remove: true,
            // Change new lines to br tags.
            insert_brs: true,
            // Whether to remove notices from the global array.
            destroy: true,
            // The stack on which the notices will be placed. Also controls the
            // direction the notices stack.
            stack: default_stack
        },

        // === Modules ===

        // This object holds all the PNotify modules. They are used to provide
        // additional functionality.
        modules: {},
        // This runs an event on all the modules.
        runModules: function (event, arg) {
            var curArg;
            for (var module in this.modules) {
                curArg = ((typeof arg === "object" && module in arg) ? arg[module] : arg);
                if (typeof this.modules[module][event] === 'function')
                    this.modules[module][event](this, typeof this.options[module] === 'object' ? this.options[module] : {}, curArg);
            }
        },

        // === Class Variables ===

        state: "initializing", // The state can be "initializing", "opening", "open", "closing", and "closed".
        timer: null, // Auto close timer.
        styles: null,
        elem: null,
        container: null,
        title_container: null,
        text_container: null,
        animating: false, // Stores what is currently being animated (in or out).
        timerHide: false, // Stores whether the notice was hidden by a timer.

        // === Events ===

        init: function () {
            var that = this;

            // First and foremost, we don't want our module objects all referencing the prototype.
            this.modules = {};
            $.extend(true, this.modules, PNotify.prototype.modules);

            // Get our styling object.
            if (typeof this.options.styling === "object") {
                this.styles = this.options.styling;
            } else {
                this.styles = PNotify.styling[this.options.styling];
            }

            // Create our widget.
            // Stop animation, reset the removal timer when the user mouses over.
            this.elem = $("<div />", {
                "class": "ui-pnotify " + this.options.addclass,
                "css": { "display": "none" },
                "mouseenter": function (e) {
                    if (that.options.mouse_reset && that.animating === "out") {
                        if (!that.timerHide)
                            return;
                        that.cancelRemove();
                    }
                    // Stop the close timer.
                    if (that.options.hide && that.options.mouse_reset) that.cancelRemove();
                },
                "mouseleave": function (e) {
                    // Start the close timer.
                    if (that.options.hide && that.options.mouse_reset) that.queueRemove();
                    PNotify.positionAll();
                }
            });
            // Create a container for the notice contents.
            this.container = $("<div />", { "class": this.styles.container + " ui-pnotify-container " + (this.options.type === "error" ? this.styles.error : (this.options.type === "info" ? this.styles.info : (this.options.type === "success" ? this.styles.success : this.styles.notice))) })
			.appendTo(this.elem);
            if (this.options.cornerclass !== "")
                this.container.removeClass("ui-corner-all").addClass(this.options.cornerclass);
            // Create a drop shadow.
            if (this.options.shadow)
                this.container.addClass("ui-pnotify-shadow");


            // Add the appropriate icon.
            if (this.options.icon !== false) {
                $("<div />", { "class": "ui-pnotify-icon" })
				.append($("<span />", { "class": this.options.icon === true ? (this.options.type === "error" ? this.styles.error_icon : (this.options.type === "info" ? this.styles.info_icon : (this.options.type === "success" ? this.styles.success_icon : this.styles.notice_icon))) : this.options.icon }))
				.prependTo(this.container);
            }

            // Add a title.
            this.title_container = $("<h4 />", {
                "class": "ui-pnotify-title"
            })
			.appendTo(this.container);
            if (this.options.title === false)
                this.title_container.hide();
            else if (this.options.title_escape)
                this.title_container.text(this.options.title);
            else
                this.title_container.html(this.options.title);

            // Add text.
            this.text_container = $("<div />", {
                "class": "ui-pnotify-text"
            })
			.appendTo(this.container);
            if (this.options.text === false)
                this.text_container.hide();
            else if (this.options.text_escape)
                this.text_container.text(this.options.text);
            else
                this.text_container.html(this.options.insert_brs ? String(this.options.text).replace(/\n/g, "<br />") : this.options.text);

            // Set width and min height.
            if (typeof this.options.width === "string")
                this.elem.css("width", this.options.width);
            if (typeof this.options.min_height === "string")
                this.container.css("min-height", this.options.min_height);


            // Add the notice to the notice array.
            if (this.options.stack.push === "top")
                PNotify.notices = $.merge([this], PNotify.notices);
            else
                PNotify.notices = $.merge(PNotify.notices, [this]);
            // Now position all the notices if they are to push to the top.
            if (this.options.stack.push === "top")
                this.queuePosition(false, 1);




            // Mark the stack so it won't animate the new notice.
            this.options.stack.animation = false;

            // Run the modules.
            this.runModules('init');

            // Display the notice.
            if (this.options.auto_display)
                this.open();
            return this;
        },

        // This function is for updating the notice.
        update: function (options) {
            // Save old options.
            var oldOpts = this.options;
            // Then update to the new options.
            this.parseOptions(oldOpts, options);
            // Update the corner class.
            if (this.options.cornerclass !== oldOpts.cornerclass)
                this.container.removeClass("ui-corner-all " + oldOpts.cornerclass).addClass(this.options.cornerclass);
            // Update the shadow.
            if (this.options.shadow !== oldOpts.shadow) {
                if (this.options.shadow)
                    this.container.addClass("ui-pnotify-shadow");
                else
                    this.container.removeClass("ui-pnotify-shadow");
            }
            // Update the additional classes.
            if (this.options.addclass === false)
                this.elem.removeClass(oldOpts.addclass);
            else if (this.options.addclass !== oldOpts.addclass)
                this.elem.removeClass(oldOpts.addclass).addClass(this.options.addclass);
            // Update the title.
            if (this.options.title === false)
                this.title_container.slideUp("fast");
            else if (this.options.title !== oldOpts.title) {
                if (this.options.title_escape)
                    this.title_container.text(this.options.title);
                else
                    this.title_container.html(this.options.title);
                if (oldOpts.title === false)
                    this.title_container.slideDown(200)
            }
            // Update the text.
            if (this.options.text === false) {
                this.text_container.slideUp("fast");
            } else if (this.options.text !== oldOpts.text) {
                if (this.options.text_escape)
                    this.text_container.text(this.options.text);
                else
                    this.text_container.html(this.options.insert_brs ? String(this.options.text).replace(/\n/g, "<br />") : this.options.text);
                if (oldOpts.text === false)
                    this.text_container.slideDown(200)
            }
            // Change the notice type.
            if (this.options.type !== oldOpts.type)
                this.container.removeClass(
					this.styles.error + " " + this.styles.notice + " " + this.styles.success + " " + this.styles.info
				).addClass(this.options.type === "error" ?
					this.styles.error :
					(this.options.type === "info" ?
						this.styles.info :
						(this.options.type === "success" ?
							this.styles.success :
							this.styles.notice
						)
					)
				);
            if (this.options.icon !== oldOpts.icon || (this.options.icon === true && this.options.type !== oldOpts.type)) {
                // Remove any old icon.
                this.container.find("div.ui-pnotify-icon").remove();
                if (this.options.icon !== false) {
                    // Build the new icon.
                    $("<div />", { "class": "ui-pnotify-icon" })
					.append($("<span />", { "class": this.options.icon === true ? (this.options.type === "error" ? this.styles.error_icon : (this.options.type === "info" ? this.styles.info_icon : (this.options.type === "success" ? this.styles.success_icon : this.styles.notice_icon))) : this.options.icon }))
					.prependTo(this.container);
                }
            }
            // Update the width.
            if (this.options.width !== oldOpts.width)
                this.elem.animate({ width: this.options.width });
            // Update the minimum height.
            if (this.options.min_height !== oldOpts.min_height)
                this.container.animate({ minHeight: this.options.min_height });
            // Update the opacity.
            if (this.options.opacity !== oldOpts.opacity)
                this.elem.fadeTo(this.options.animate_speed, this.options.opacity);
            // Update the timed hiding.
            if (!this.options.hide)
                this.cancelRemove();
            else if (!oldOpts.hide)
                this.queueRemove();
            this.queuePosition(true);

            // Run the modules.
            this.runModules('update', oldOpts);
            return this;
        },

        // Display the notice.
        open: function () {
            this.state = "opening";
            // Run the modules.
            this.runModules('beforeOpen');

            var that = this;
            // If the notice is not in the DOM, append it.
            if (!this.elem.parent().length)
                this.elem.appendTo(this.options.stack.context ? this.options.stack.context : body);
            // Try to put it in the right position.
            if (this.options.stack.push !== "top")
                this.position(true);
            // First show it, then set its opacity, then hide it.
            if (this.options.animation === "fade" || this.options.animation.effect_in === "fade") {
                // If it's fading in, it should start at 0.
                this.elem.show().fadeTo(0, 0).hide();
            } else {
                // Or else it should be set to the opacity.
                if (this.options.opacity !== 1)
                    this.elem.show().fadeTo(0, this.options.opacity).hide();
            }
            this.animateIn(function () {
                that.queuePosition(true);

                // Now set it to hide.
                if (that.options.hide)
                    that.queueRemove();

                that.state = "open";

                // Run the modules.
                that.runModules('afterOpen');
            });

            return this;
        },

        // Remove the notice.
        remove: function (timer_hide) {
            this.state = "closing";
            this.timerHide = !!timer_hide; // Make sure it's a boolean.
            // Run the modules.
            this.runModules('beforeClose');

            var that = this;
            if (this.timer) {
                window.clearTimeout(this.timer);
                this.timer = null;
            }
            this.animateOut(function () {
                that.state = "closed";
                // Run the modules.
                that.runModules('afterClose');
                that.queuePosition(true);
                // If we're supposed to remove the notice from the DOM, do it.
                if (that.options.remove)
                    that.elem.detach();
                // Run the modules.
                that.runModules('beforeDestroy');
                // Remove object from PNotify.notices to prevent memory leak (issue #49)
                // unless destroy is off
                if (that.options.destroy) {
                    if (PNotify.notices !== null) {
                        var idx = $.inArray(that, PNotify.notices);
                        if (idx !== -1) {
                            PNotify.notices.splice(idx, 1);
                        }
                    }
                }
                // Run the modules.
                that.runModules('afterDestroy');
            });

            return this;
        },

        // === Class Methods ===

        // Get the DOM element.
        get: function () { return this.elem; },

        // Put all the options in the right places.
        parseOptions: function (options, moreOptions) {
            this.options = $.extend(true, {}, PNotify.prototype.options);
            // This is the only thing that *should* be copied by reference.
            this.options.stack = PNotify.prototype.options.stack;
            var optArray = [options, moreOptions], curOpts;
            for (var curIndex in optArray) {
                curOpts = optArray[curIndex];
                if (typeof curOpts == "undefined")
                    break;
                if (typeof curOpts !== 'object') {
                    this.options.text = curOpts;
                } else {
                    for (var option in curOpts) {
                        if (this.modules[option]) {
                            // Avoid overwriting module defaults.
                            $.extend(true, this.options[option], curOpts[option]);
                        } else {
                            this.options[option] = curOpts[option];
                        }
                    }
                }
            }
        },

        // Animate the notice in.
        animateIn: function (callback) {
            // Declare that the notice is animating in. (Or has completed animating in.)
            this.animating = "in";
            var animation;
            if (typeof this.options.animation.effect_in !== "undefined")
                animation = this.options.animation.effect_in;
            else
                animation = this.options.animation;
            if (animation === "none") {
                this.elem.show();
                callback();
            } else if (animation === "show")
                this.elem.show(this.options.animate_speed, callback);
            else if (animation === "fade")
                this.elem.show().fadeTo(this.options.animate_speed, this.options.opacity, callback);
            else if (animation === "slide")
                this.elem.slideDown(this.options.animate_speed, callback);
            else if (typeof animation === "function")
                animation("in", callback, this.elem);
            else
                this.elem.show(animation, (typeof this.options.animation.options_in === "object" ? this.options.animation.options_in : {}), this.options.animate_speed, callback);
            if (this.elem.parent().hasClass('ui-effects-wrapper'))
                this.elem.parent().css({ "position": "fixed", "overflow": "visible" });
            if (animation !== "slide")
                this.elem.css("overflow", "visible");
            this.container.css("overflow", "hidden");
        },

        // Animate the notice out.
        animateOut: function (callback) {
            // Declare that the notice is animating out. (Or has completed animating out.)
            this.animating = "out";
            var animation;
            if (typeof this.options.animation.effect_out !== "undefined")
                animation = this.options.animation.effect_out;
            else
                animation = this.options.animation;
            if (animation === "none") {
                this.elem.hide();
                callback();
            } else if (animation === "show")
                this.elem.hide(this.options.animate_speed, callback);
            else if (animation === "fade")
                this.elem.fadeOut(this.options.animate_speed, callback);
            else if (animation === "slide")
                this.elem.slideUp(this.options.animate_speed, callback);
            else if (typeof animation === "function")
                animation("out", callback, this.elem);
            else
                this.elem.hide(animation, (typeof this.options.animation.options_out === "object" ? this.options.animation.options_out : {}), this.options.animate_speed, callback);
            if (this.elem.parent().hasClass('ui-effects-wrapper'))
                this.elem.parent().css({ "position": "fixed", "overflow": "visible" });
            if (animation !== "slide")
                this.elem.css("overflow", "visible");
            this.container.css("overflow", "hidden");
        },

        // Position the notice. dont_skip_hidden causes the notice to
        // position even if it's not visible.
        position: function (dontSkipHidden) {
            // Get the notice's stack.
            var s = this.options.stack,
				e = this.elem;
            if (e.parent().hasClass('ui-effects-wrapper'))
                e = this.elem.css({ "left": "0", "top": "0", "right": "0", "bottom": "0" }).parent();
            if (typeof s.context === "undefined")
                s.context = body;
            if (!s) return;
            if (typeof s.nextpos1 !== "number")
                s.nextpos1 = s.firstpos1;
            if (typeof s.nextpos2 !== "number")
                s.nextpos2 = s.firstpos2;
            if (typeof s.addpos2 !== "number")
                s.addpos2 = 0;
            var hidden = e.css("display") === "none";
            // Skip this notice if it's not shown.
            if (!hidden || dontSkipHidden) {
                var curpos1, curpos2;
                // Store what will need to be animated.
                var animate = {};
                // Calculate the current pos1 value.
                var csspos1;
                switch (s.dir1) {
                    case "down":
                        csspos1 = "top";
                        break;
                    case "up":
                        csspos1 = "bottom";
                        break;
                    case "left":
                        csspos1 = "right";
                        break;
                    case "right":
                        csspos1 = "left";
                        break;
                }
                curpos1 = parseInt(e.css(csspos1).replace(/(?:\..*|[^0-9.])/g, ''));
                if (isNaN(curpos1))
                    curpos1 = 0;
                // Remember the first pos1, so the first visible notice goes there.
                if (typeof s.firstpos1 === "undefined" && !hidden) {
                    s.firstpos1 = curpos1;
                    s.nextpos1 = s.firstpos1;
                }
                // Calculate the current pos2 value.
                var csspos2;
                switch (s.dir2) {
                    case "down":
                        csspos2 = "top";
                        break;
                    case "up":
                        csspos2 = "bottom";
                        break;
                    case "left":
                        csspos2 = "right";
                        break;
                    case "right":
                        csspos2 = "left";
                        break;
                }
                curpos2 = parseInt(e.css(csspos2).replace(/(?:\..*|[^0-9.])/g, ''));
                if (isNaN(curpos2))
                    curpos2 = 0;
                // Remember the first pos2, so the first visible notice goes there.
                if (typeof s.firstpos2 === "undefined" && !hidden) {
                    s.firstpos2 = curpos2;
                    s.nextpos2 = s.firstpos2;
                }
                // Check that it's not beyond the viewport edge.
                if ((s.dir1 === "down" && s.nextpos1 + e.height() > (s.context.is(body) ? jwindow.height() : s.context.prop('scrollHeight'))) ||
					(s.dir1 === "up" && s.nextpos1 + e.height() > (s.context.is(body) ? jwindow.height() : s.context.prop('scrollHeight'))) ||
					(s.dir1 === "left" && s.nextpos1 + e.width() > (s.context.is(body) ? jwindow.width() : s.context.prop('scrollWidth'))) ||
					(s.dir1 === "right" && s.nextpos1 + e.width() > (s.context.is(body) ? jwindow.width() : s.context.prop('scrollWidth')))) {
                    // If it is, it needs to go back to the first pos1, and over on pos2.
                    s.nextpos1 = s.firstpos1;
                    s.nextpos2 += s.addpos2 + (typeof s.spacing2 === "undefined" ? 25 : s.spacing2);
                    s.addpos2 = 0;
                }
                // Animate if we're moving on dir2.
                if (s.animation && s.nextpos2 < curpos2) {
                    switch (s.dir2) {
                        case "down":
                            animate.top = s.nextpos2 + "px";
                            break;
                        case "up":
                            animate.bottom = s.nextpos2 + "px";
                            break;
                        case "left":
                            animate.right = s.nextpos2 + "px";
                            break;
                        case "right":
                            animate.left = s.nextpos2 + "px";
                            break;
                    }
                } else {
                    if (typeof s.nextpos2 === "number")
                        e.css(csspos2, s.nextpos2 + "px");
                }
                // Keep track of the widest/tallest notice in the column/row, so we can push the next column/row.
                switch (s.dir2) {
                    case "down":
                    case "up":
                        if (e.outerHeight(true) > s.addpos2)
                            s.addpos2 = e.height();
                        break;
                    case "left":
                    case "right":
                        if (e.outerWidth(true) > s.addpos2)
                            s.addpos2 = e.width();
                        break;
                }
                // Move the notice on dir1.
                if (typeof s.nextpos1 === "number") {
                    // Animate if we're moving toward the first pos.
                    if (s.animation && (curpos1 > s.nextpos1 || animate.top || animate.bottom || animate.right || animate.left)) {
                        switch (s.dir1) {
                            case "down":
                                animate.top = s.nextpos1 + "px";
                                break;
                            case "up":
                                animate.bottom = s.nextpos1 + "px";
                                break;
                            case "left":
                                animate.right = s.nextpos1 + "px";
                                break;
                            case "right":
                                animate.left = s.nextpos1 + "px";
                                break;
                        }
                    } else
                        e.css(csspos1, s.nextpos1 + "px");
                }
                // Run the animation.
                if (animate.top || animate.bottom || animate.right || animate.left)
                    e.animate(animate, { duration: this.options.position_animate_speed, queue: false });
                // Calculate the next dir1 position.
                switch (s.dir1) {
                    case "down":
                    case "up":
                        s.nextpos1 += e.height() + (typeof s.spacing1 === "undefined" ? 25 : s.spacing1);
                        break;
                    case "left":
                    case "right":
                        s.nextpos1 += e.width() + (typeof s.spacing1 === "undefined" ? 25 : s.spacing1);
                        break;
                }
            }
            return this;
        },
        // Queue the position all function so it doesn't run repeatedly and
        // use up resources.
        queuePosition: function (animate, milliseconds) {
            if (timer)
                clearTimeout(timer);
            if (!milliseconds)
                milliseconds = 10;
            timer = setTimeout(function () { PNotify.positionAll(animate) }, milliseconds);
            return this;
        },


        // Cancel any pending removal timer.
        cancelRemove: function () {
            if (this.timer)
                window.clearTimeout(this.timer);
            if (this.state === "closing") {
                // If it's animating out, animate back in really quickly.
                this.elem.stop(true);
                this.state = "open";
                this.animating = "in";
                this.elem.css("height", "auto").animate({ "width": this.options.width, "opacity": this.options.opacity }, "fast");
            }
            return this;
        },
        // Queue a removal timer.
        queueRemove: function () {
            var that = this;
            // Cancel any current removal timer.
            this.cancelRemove();
            this.timer = window.setTimeout(function () {
                that.remove(true);
            }, (isNaN(this.options.delay) ? 0 : this.options.delay));
            return this;
        }
    });
    // These functions affect all notices.
    $.extend(PNotify, {
        // This holds all the notices.
        notices: [],
        removeAll: function () {
            $.each(PNotify.notices, function () {
                if (this.remove)
                    this.remove();
            });
        },
        positionAll: function (animate) {
            // This timer is used for queueing this function so it doesn't run
            // repeatedly.
            if (timer)
                clearTimeout(timer);
            timer = null;
            // Reset the next position data.
            $.each(PNotify.notices, function () {
                var s = this.options.stack;
                if (!s) return;
                s.nextpos1 = s.firstpos1;
                s.nextpos2 = s.firstpos2;
                s.addpos2 = 0;
                s.animation = animate;
            });
            $.each(PNotify.notices, function () {
                this.position();
            });
        },
        styling: {
            jqueryui: {
                container: "ui-widget ui-widget-content ui-corner-all",
                notice: "ui-state-highlight",
                // (The actual jQUI notice icon looks terrible.)
                notice_icon: "ui-icon ui-icon-info",
                info: "",
                info_icon: "ui-icon ui-icon-info",
                success: "ui-state-default",
                success_icon: "ui-icon ui-icon-circle-check",
                error: "ui-state-error",
                error_icon: "ui-icon ui-icon-alert"
            },
            bootstrap2: {
                container: "alert",
                notice: "",
                notice_icon: "icon-exclamation-sign",
                info: "alert-info",
                info_icon: "icon-info-sign",
                success: "alert-success",
                success_icon: "icon-ok-sign",
                error: "alert-error",
                error_icon: "icon-warning-sign"
            },
            bootstrap3: {
                container: "alert",
                notice: "alert-warning",
                notice_icon: "glyphicon glyphicon-exclamation-sign",
                info: "alert-info",
                info_icon: "glyphicon glyphicon-info-sign",
                success: "alert-success",
                success_icon: "glyphicon glyphicon-ok-sign",
                error: "alert-danger",
                error_icon: "glyphicon glyphicon-warning-sign"
            }
        }
    });
    /*
	 * uses icons from http://fontawesome.io/
	 * version 4.0.3
	 */
    PNotify.styling.fontawesome = $.extend({}, PNotify.styling.bootstrap3);
    $.extend(PNotify.styling.fontawesome, {
        notice_icon: "fa fa-info",
        info_icon: "fa fa-cog fa-spin",
        success_icon: "fa fa-check",
        error_icon: "fa fa-warning"
    });

    if (document.body)
        do_when_ready();
    else
        $(do_when_ready);
    return PNotify;
}));
// Callbacks
// Uses AMD or browser globals for jQuery.
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as a module.
        define('pnotify.callbacks', ['jquery', 'pnotify'], factory);
    } else {
        // Browser globals
        factory(jQuery, PNotify);
    }
}(function ($, PNotify) {
    var _init = PNotify.prototype.init,
		_open = PNotify.prototype.open,
		_remove = PNotify.prototype.remove;
    PNotify.prototype.init = function () {
        if (this.options.before_init) {
            this.options.before_init(this.options);
        }
        _init.apply(this, arguments);
        if (this.options.after_init) {
            this.options.after_init(this);
        }
    };
    PNotify.prototype.open = function () {
        var ret;
        if (this.options.before_open) {
            ret = this.options.before_open(this);
        }
        if (ret !== false) {
            _open.apply(this, arguments);
            if (this.options.after_open) {
                this.options.after_open(this);
            }
        }
    };
    PNotify.prototype.remove = function (timer_hide) {
        var ret;
        if (this.options.before_close) {
            ret = this.options.before_close(this, timer_hide);
        }
        if (ret !== false) {
            _remove.apply(this, arguments);
            if (this.options.after_close) {
                this.options.after_close(this, timer_hide);
            }
        }
    };
}));
// Reference
// This file is for referencing while you are making a notify module.
// Uses AMD or browser globals for jQuery.
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as a module.
        define('pnotify.reference', ['jquery', 'pnotify'], factory);
    } else {
        // Browser globals
        factory(jQuery, PNotify);
    }
}(function ($, PNotify) {
    // This if the default values of your options.
    PNotify.prototype.options.reference = {
        // Provide a thing for stuff. Turned off by default.
        putThing: false,
        // If you are displaying any text, you should use a labels options to
        // support internationalization.
        labels: {
            text: "Spin Around"
        }
    };
    PNotify.prototype.modules.reference = {
        // You can put variables here that are specific to a notice instance.
        thingElem: null,

        // This function is called when the notice is being created, after the
        // core has done all of its work.
        init: function (notice /* the notice object */, options /* this module's options */) {
            var that = this; // This line will allow you to access instance variables
            // like "this.thingElem" from within closures.

            // Note that options only contains the options specific to our modules.
            // To access global options, we would use notice.options.

            // We want to check to make sure the notice should include our thing.
            if (!options.putThing)
                return;

            // We're going to create a button that will be appended to the notice.
            // It will be disabled by default, so we can enable it on mouseover.
            // You should try to keep elements inside the notice container.
            this.thingElem = $('<button style="float:right;" class="btn btn-default" type="button" disabled><i class="' + notice.styles.athing + '" />&nbsp;' + options.labels.text + '</button>').appendTo(notice.container);
            // Since our button is floated, we have to add a clearing div.
            notice.container.append('<div style="clear: right; line-height: 0;" />')

            // Now we're going to enable the button on mouseenter.
            notice.elem.on({
                "mouseenter": function (e) {
                    // Enable the button.
                    // Notice that we have to use "that" to access thingElem, because
                    // we are in a different scope inside this function.
                    that.thingElem.prop("disabled", false);
                },
                "mouseleave": function (e) {
                    // Disable the button.
                    that.thingElem.prop("disabled", true);
                }
            });

            // Now we're going to make our button do something.
            this.thingElem.on("click", function () {
                // Spin the notice around.
                var cur_angle = 0;
                var timer = setInterval(function () {
                    cur_angle += 10;
                    if (cur_angle == 360) {
                        cur_angle = 0;
                        clearInterval(timer);
                    }
                    notice.elem.css({
                        '-moz-transform': ('rotate(' + cur_angle + 'deg)'),
                        '-webkit-transform': ('rotate(' + cur_angle + 'deg)'),
                        '-o-transform': ('rotate(' + cur_angle + 'deg)'),
                        '-ms-transform': ('rotate(' + cur_angle + 'deg)'),
                        'filter': ('progid:DXImageTransform.Microsoft.BasicImage(rotation=' + (cur_angle / 360 * 4) + ')')
                    });
                }, 20);
            });
        },

        // This is called when the notice is updating its options.
        update: function (notice, options /* the new options for our module */, oldOpts /* the old options for our module */) {
            // We need to remove the button if it's now disabled, and show it again if it's enabled.
            if (options.putThing && this.thingElem)
                this.thingElem.show();
            else if (!options.putThing && this.thingElem)
                this.thingElem.hide();
            // You may notice that if the user creates a notice without our button,
            // then updates it to enable our button, they will be out of luck.
            // Whatever, I don't want to write that much code.

            // Now we update the icon, which may have changed.
            // Note that as of right now, PNotify doesn't support updating styling.
            if (this.thingElem)
                this.thingElem.find('i').attr("class", notice.styles.athing);
        },
        // I have nothing to put in these, just showing you that they exist. You
        // won't need to include them if you aren't using them.
        beforeOpen: function (notice, options) {
            // Called before the notice is opened.
        },
        afterOpen: function (notice, options) {
            // Called after the notice is opened.
        },
        beforeClose: function (notice, options) {
            // Called before the notice is closed.
        },
        afterClose: function (notice, options) {
            // Called after the notice is closed.
        },
        beforeDestroy: function (notice, options) {
            // Called before the notice is destroyed.
        },
        afterDestroy: function (notice, options) {
            // Called after the notice is destroyed.
        }
    };
    // This is where you would add any styling options you are using in your code.
    $.extend(PNotify.styling.jqueryui, {
        athing: "ui-icon ui-icon-refresh"
    });
    $.extend(PNotify.styling.bootstrap2, {
        athing: "icon-refresh"
    });
    $.extend(PNotify.styling.bootstrap3, {
        athing: "glyphicon glyphicon-refresh"
    });
    $.extend(PNotify.styling.fontawesome, {
        athing: "fa fa-refresh"
    });
}));
