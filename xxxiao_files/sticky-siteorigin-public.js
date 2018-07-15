(function( $ ) {
	'use strict';

	/**
	 * All of the code for your public-facing JavaScript source
	 * should reside in this file.
	 *
	 * Note: It has been assumed you will write jQuery code here, so the
	 * $ function reference has been prepared for usage within the scope
	 * of this function.
	 *
	 * This enables you to define handlers, for when the DOM is ready:
	 *
	 * $(function() {
	 *
	 * });
	 *
	 * When the window is loaded:
	 *
	 * $( window ).load(function() {
	 *
	 * });
	 *
	 * ...and/or other possibilities.
	 *
	 * Ideally, it is not considered best practise to attach more than a
	 * single DOM-ready or window-load handler for a particular page.
	 * Although scripts in the WordPress core, Plugins and Themes may be
	 * practising this, we should strive to set a better example in our own work.
	 */

	 function sticky_siteorigin_fixed(){
	     if ($(window).width() > 780) {
	            

	            $('.sticky-siteorigin').each(function(){
	            	var offset_px = parseInt($(this).attr('data-sticky-offset'), 10),
	            	use_spacer = $(this).attr('data-sticky-spacer');

	            	$(this).closest('.panel-grid-cell').css({'align-self':'stretch'});

	                 $(this).closest('.so-panel').css({'z-index':'99999','align-self':'flex-start'}).stick_in_parent({
	                     offset_top:offset_px,
	                     recalc_every:1,
	                     spacer:use_spacer
	                 });

	            })
	         
	     } else {
             	$('.sticky-siteorigin').trigger("sticky_kit:detach");
	     }
	 }


	 $(window).load(function(){

	 	sticky_siteorigin_fixed();
	 });

	 $(window).resize(function(){

	 	sticky_siteorigin_fixed();
	 });

})( jQuery );
