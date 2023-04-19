$(document).ready(function () {


    //delete
    $(document).on("click", ".delete-icons", function (ev) {

        ev.preventDefault();

        let dataId = $(this).attr("data-id");


        

        $.ajax({

            url: `cart/deleteproductformbasket?id=${dataId}`,
            type: "Post",
            success: function (res) {


              

            }


        })

        var totalComon = $(".grand-total span").html()

        var productTotal = $(this).parent().parent().prev().html()

        var res = parseFloat(totalComon) - parseFloat(productTotal)

        $(".grand-total span").html(res)


        $(this).parent().parent().parent().remove()


        
       
      


    })



    $(document).on("submit", "#add-basket-form", function (ev) {
        ev.preventDefault();

       


        let productId = $(this).prev().val();

        let dataId = { id: productId }


        $.ajax({

            url: "home/addbasket",
            type: "Post",
            data: dataId,
            success: function (res) {

               $(".cart-count").text(res) //layout da countu aritir


                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Your work has been saved',
                    showConfirmButton: false,
                    timer: 1500
                })
            }


         })


    })




    ////Add basket  ozum yazdiqim 

    //$(document).on("click", ".add-to-cart", function (ev) {
    //    ev.preventDefault();

    //    let dataId = $(this).attr("data-id");

       
    //    $.ajax({

    //        url: `home/addbasket?id=${dataId}`,
    //        type: "Post",
    //        success: function (res) {



    //        }


    //     })
    //})

  


    //Serach

    $(document).on("keyup", "#input-serach", function () {

        $("#serachList li").slice(1).remove();

        let value = $("#input-serach").val();

        $.ajax({
            url: "/shop/serach?item="+value,
            method: "Get",
            success: function(res){

                $("#serachList").append(res)
            }

        })

    })





    $(document).on("click", ".load-more", function () {

        let parent = $(".parent-products");

        let skipCount = $(parent).children().length;

        let dataCount = $(parent).attr("data-count");

        $.ajax({
            url: `shop/loadmore?skip=${skipCount}`,
            type: "Get",
            success: function (res) {

                $(parent).append(res);

                skipCount = $(parent).children().length;

                if (skipCount >= dataCount) {
                    $(".load-more").addClass("d-none");
                    $(".show-less").removeClass("d-none");
                }
            }

        })
    })




    $(document).on("click", ".show-less", function () {



        let skipCount = 0;


        $.ajax({
            url: `shop/loadmore?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(".parent-products").html("");
                $(".parent-products").append(res);

                $(".load-more").removeClass("d-none");
                $(".show-less").addClass("d-none");
            }

        })
    })



    // HEADER

    $(document).on('click', '#search', function () {
        $(this).next().toggle();
    })

    $(document).on('click', '#mobile-navbar-close', function () {
        $(this).parent().removeClass("active");

    })
    $(document).on('click', '#mobile-navbar-show', function () {
        $('.mobile-navbar').addClass("active");

    })

    $(document).on('click', '.mobile-navbar ul li a', function () {
        if ($(this).children('i').hasClass('fa-caret-right')) {
            $(this).children('i').removeClass('fa-caret-right').addClass('fa-sort-down')
        }
        else {
            $(this).children('i').removeClass('fa-sort-down').addClass('fa-caret-right')
        }
        $(this).parent().next().slideToggle();
    })

    // SLIDER

    $(document).ready(function(){
        $(".slider").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });

    // PRODUCT

    $(document).on('click', '.categories', function(e)
    {
        e.preventDefault();
        $(this).next().next().slideToggle();
    })

    $(document).on('click', '.category li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');
        
        products.each(function () {
            if(category == $(this).attr('data-id'))
            {
                $(this).parent().fadeIn();
            }
            else
            {
                $(this).parent().hide();
            }
        })
        if(category == 'all')
        {
            products.parent().fadeIn();
        }
    })

    // ACCORDION 

    $(document).on('click', '.question', function()
    {   
       $(this).siblings('.question').children('i').removeClass('fa-minus').addClass('fa-plus');
       $(this).siblings('.answer').not($(this).next()).slideUp();
       $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
       $(this).next().slideToggle();
       $(this).siblings('.active').removeClass('active');
       $(this).toggleClass('active');
    })

    // TAB

    $(document).on('click', 'ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().next().children('p.active').removeClass('active');

        $(this).parent().next().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    $(document).on('click', '.tab4 ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().parent().next().children().children('p.active').removeClass('active');

        $(this).parent().parent().next().children().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    // INSTAGRAM

    $(document).ready(function(){
        $(".instagram").owlCarousel(
            {
                items: 4,
                loop: true,
                autoplay: true,
                responsive:{
                    0:{
                        items:1
                    },
                    576:{
                        items:2
                    },
                    768:{
                        items:3
                    },
                    992:{
                        items:4
                    }
                }
            }
        );
      });

      $(document).ready(function(){
        $(".say").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });
})