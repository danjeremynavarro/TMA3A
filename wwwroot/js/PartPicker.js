if (!window.tma3a || typeof (window.tma3a) === "undefined") {
    console.log("added window.tma3a namespace")
    window.tma3a = {};
}

window.tma3a.displayInfo = (element) => {
    const id = element.id;
    const productSelected = element.value;

    let allInfoCard = $(`[data-for=${id}]`);
    allInfoCard.each((index,infoEl) => {
        console.log($(infoEl));
        if ($(infoEl).attr("data-value") == productSelected) {
            $(infoEl).show();
        } else {
            $(infoEl).hide();
        }
    })
}

$(document).ready(() => {
    $("select").each((index, element) => {
        window.tma3a.displayInfo(element);
    })
    $("select").on("change", (el) => {
        console.log(el);
        window.tma3a.displayInfo(el.currentTarget);
    })
});