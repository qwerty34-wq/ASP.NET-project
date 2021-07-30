$(function(){

    $(".filterElem").change(function (e) {

        let name = e.target.name
        let value = e.target.value

        let query = `${name}=${value}`

        var obj = {name: value}

        $.ajax({
            type: 'POST',
            url: '/Home/Filter',
            data: JSON.stringify(obj)
        }).done(function (response) {

            console.log("OK")
        })

    })

})