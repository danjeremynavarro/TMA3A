class SlideControls {
    static _intervalId;

    static _intervalDuration = 4000;

    static setMode() {
        switch ($("#image-mode").val()) {
            case "Slideshow":
                SlideControls.setSlideShow();
                break;
            case "Manual":
                SlideControls.setManualShow();
                break;
        }
    }
    static setRandomCarousel() {
        /**
         * Randomizer that ensures pictures are guaranteed to be shown at least once
         * given length(number of pictures).
         * https://stackoverflow.com/questions/2450954/how-to-randomize-shuffle-a-javascript-array
         * @param {any} array
         */
        function shuffleArray(array) {
            for (let i = array.length - 1; i > 0; i--) {
                const j = Math.floor(Math.random() * (i + 1));
                [array[i], array[j]] = [array[j], array[i]];
            }
        }

        clearInterval(SlideControls._intervalId);
        $("#sequential-mode-container").css("visibility", "hidden");
        const itemLength = $(".carousel-item").length;
        $("#slideshow-container").carousel("pause");

        let arrayRange = Array.from(Array(itemLength).keys());        
        shuffleArray(arrayRange);   

        SlideControls._intervalId = setInterval(function () {
            if (arrayRange.length === 0) {
                arrayRange = Array.from(Array(itemLength).keys());
                shuffleArray(arrayRange);   
            }
            $("#slideshow-container").carousel(arrayRange.pop());
            console.log(arrayRange.length);
            console.log(arrayRange);
        }, SlideControls._intervalDuration);
    }
    static setSequentialCarousel() {
        SlideControls.setSequenceMode();
        $("#sequential-mode-container").css("visibility", "visible");
    }
    static setModeSlideShow() {
        const mode = $("#show-mode").val();
        switch (mode) {
            case "Sequential":
                SlideControls.setSequentialCarousel();
                break;
            case "Random":
                SlideControls.setRandomCarousel();
                break;

        }
    }
    static setCarouselControl(visibility) {
        $(".carousel-control-prev").css("visibility", visibility);
        $(".carousel-control-next").css("visibility", visibility);
    }
    static startShow() {
        $("#slideshow-container").carousel("pause");
        $("#stop-show").show();
        $("#start-show").hide();
        $("#slideshow-mode").css("visibility", "visible");
        SlideControls.setModeSlideShow();
        SlideControls.setCarouselControl("hidden");
    }
    static stopShow() {
        clearInterval(SlideControls._intervalId);
        $("#slideshow-container").carousel("pause");
        $("#stop-show").hide();
        $("#start-show").show();
        $("#slideshow-mode").css("visibility", "hidden");
        $("#sequential-mode-container").css("visibility", "hidden");
        SlideControls.setCarouselControl("visible");
    }
    static setBackwardMode() {
        clearInterval(SlideControls._intervalId);
        $("#slideshow-container").carousel("pause");
        SlideControls._intervalId = setInterval(function () {
            console.log("Set Random");
            $("#slideshow-container").carousel("prev");
        }, SlideControls._intervalDuration);
    }
    static setSequenceMode() {
        clearInterval(SlideControls._intervalId);
        const mode = $("#sequential-mode").val();
        switch (mode) {
            case "Forward":
                console.log("forward");
                $("#slideshow-container").carousel("cycle");
                break;
            case "Backward":
                SlideControls.setBackwardMode();
                break;
        }
    }
}

$(document).ready(() => {
    var myCarousel = document.querySelector("#slideshow-container")
    var carousel = new bootstrap.Carousel(myCarousel, {
        interval: SlideControls._intervalDuration,
        pause: false
    })
    $("#image-mode").change(() => { SlideControls.setMode(); });
    $("#show-mode").change(() => { SlideControls.setModeSlideShow(); });
    $("#start-show").click(() => { SlideControls.startShow() });
    $("#stop-show").on("click", () => {
        SlideControls.stopShow()
    })
    $("#sequential-mode").on("change", () => {
        SlideControls.setSequenceMode();
    })
})