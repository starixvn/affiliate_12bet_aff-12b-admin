$('.thumb-box img').hover(
    function(){
        var attrImg = $(this).attr('id');
        $(this).attr('src', 'Styles/Images/'+ attrImg + '_hover.png');
        $(this).css('transition', 'background 0.5s ease');
    },
    function() {
         var attrImg = $(this).attr('id');
        $(this).attr('src', 'Styles/Images/'+ attrImg + '.png');
    }
);

//$(window).scroll(function() {    
//    var scroll = $(window).scrollTop();    
//    if (scroll > 90) {
//        $(".header-bottom").addClass("navbar-fixed-top");
//    }
//    else {
//         $(".header-bottom").removeClass("navbar-fixed-top");
//    }
        
//});

//$(function() {
//  $('.menu a[href*=#]:not([href=#])').click(function() {
//    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
//      var target = $(this.hash);
//      target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
//      if (target.length) {
//        $('html,body').animate({
//          scrollTop: target.offset().top - 90
//        }, 1000);
//        return false;
//      }
//    }
//  });
//});

(function( $ ) {

	//Function to animate slider captions 
	function doAnimations( elems ) {
		//Cache the animationend event in a variable
		var animEndEv = 'webkitAnimationEnd animationend';
		
		elems.each(function () {
			var $this = $(this),
				$animationType = $this.data('animation');
			$this.addClass($animationType).one(animEndEv, function () {
				$this.removeClass($animationType);
			});
		});
	}
	
	//Variables on page load 
	var $myCarousel = $('#carousel-example-generic'),
		$firstAnimatingElems = $myCarousel.find('.item:first').find("[data-animation ^= 'animated']");
		
	//Initialize carousel 
	$myCarousel.carousel();
	
	//Animate captions in first slide on page load 
	doAnimations($firstAnimatingElems);
	
	//Pause carousel  
	$myCarousel.carousel('pause');
	
	
	//Other slides to be animated on carousel slide event 
	$myCarousel.on('slide.bs.carousel', function (e) {
		var $animatingElems = $(e.relatedTarget).find("[data-animation ^= 'animated']");
		doAnimations($animatingElems);
	});  
	
})(jQuery);